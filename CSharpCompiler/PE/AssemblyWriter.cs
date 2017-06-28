using CSharpCompiler.Compilation;
using CSharpCompiler.PE.Metadata;
using CSharpCompiler.PE.Sections;
using CSharpCompiler.PE.Sections.Relocations;
using CSharpCompiler.PE.Sections.Resources;
using CSharpCompiler.PE.Sections.Text;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System.IO;

using static CSharpCompiler.PE.Constants;

namespace CSharpCompiler.PE
{
    public sealed class AssemblyWriter : PEWriter
    {
        private CompilationOptions _options;
        private TextSection _text;
        private RsrcSection _rsrc;
        private RelocSection _reloc;

        public AssemblyWriter(AssemblyDefinition assemblyDef, Stream output)
            : this(assemblyDef, output, new CompilationOptions())
        { }

        public AssemblyWriter(AssemblyDefinition assemblyDef, Stream output, CompilationOptions options)
            : base(output)
        {
            _options = options;
            _text = GetTextSection(assemblyDef);
            _rsrc = GetRsrcSection();
            _reloc = GetRelocSection();
        }

        public void WriteAssembly()
        {
            WriteDOSHeader();
            WritePEFileHeader();
            WriteSectionHeaders();
            WriteSections();
        }

        private TextSection GetTextSection(AssemblyDefinition assemblyDef)
        {
            var resources = new ByteBuffer();
            var data = new ByteBuffer();

            var metadata = MetadataBuilder.Build(assemblyDef);
            var map = GetTextMap(metadata, resources, data);

            return new TextSection()
            {
                Header = GetTextHeader(map),
                CLRHeader = GetCLRHeader(assemblyDef, map, metadata),
                Map = map,
                MetadataHeader = GetMetadataHeader(),
                Metadata = metadata,
                ILCode = metadata.ILCode,
                Resources = resources,
                Data = data
            };
        }

        private TextMap GetTextMap(MetadataBuildResult metadata, ByteBuffer resources, ByteBuffer data)
        {
            TextMap map = new TextMap();
            map.Add(TextSegment.ImportAddressTable, SIZE_OF_IAT);
            map.Add(TextSegment.CLRHeader, (uint)ByteBuffer.SizeOf<CLRHeader>(), 8);
            map.Add(TextSegment.ILCode, (uint)metadata.ILCode.Length, 4);
            map.Add(TextSegment.Resources, (uint)resources.Length, 8);
            map.Add(TextSegment.Data, (uint)data.Length, 4);
            map.Add(TextSegment.StrongNameSignature, 0, 4);
            map.Add(TextSegment.MetadataHeader, GetMetadataHeaderLength());
            map.Add(TextSegment.TableHeap, (uint)metadata.Heaps.Tables.Length, 4);
            map.Add(TextSegment.StringHeap, (uint)metadata.Heaps.Strings.Length, 4);
            map.Add(TextSegment.UserStringHeap, (uint)metadata.Heaps.UserStrings.Length, 4);
            map.Add(TextSegment.GuidHeap, (uint)metadata.Heaps.Guids.Length, 4);
            map.Add(TextSegment.BlobHeap, (uint)metadata.Heaps.Blobs.Length, 4);
            map.Add(TextSegment.DebugDirectory, 0, 4);

            uint importDirRVA = map.GetVirtualAddressAfter(TextSegment.DebugDirectory);
            uint importHintRVA = BitArithmetic.Align(importDirRVA + 48u, 16u);
            uint importDirLength = (importHintRVA - importDirRVA) + 27u;
            uint startupStubRVA = 2u + BitArithmetic.Align(importDirRVA + importDirLength, 4u);

            map.Add(TextSegment.ImportDirectory, new DataDirectory(importDirRVA, importDirLength));
            map.Add(TextSegment.ImportHintNameTable, new DataDirectory(importHintRVA, 0));
            map.Add(TextSegment.StartupStub, new DataDirectory(startupStubRVA, 6));

            return map;
        }

        private uint GetMetadataHeaderLength()
        {
            const uint metadataHeaderLength = 32;
            const uint tableStreamLength = 12;
            const uint stringStreamLength = 20;
            const uint userStringStreamLength = 12;
            const uint guidStreamLength = 16;
            const uint blobStreamLength = 16;

            return metadataHeaderLength
                + tableStreamLength
                + stringStreamLength
                + userStringStreamLength
                + guidStreamLength
                + blobStreamLength;
        }

        private SectionHeader GetTextHeader(TextMap map)
        {
            return new SectionHeader()
            {
                Name = ".text",
                VirtualSize = map.GetSize(),
                VirtualAddress = TEXT_SECTION_BASE,
                SizeOfRawData = BitArithmetic.Align(map.GetSize(), FILE_ALIGMENT),
                PointerToRawData = GetPEHeaderSize(),
                Characteristics = SectionCharacteristic.MEM_READ
                    | SectionCharacteristic.MEM_EXECUTE
                    | SectionCharacteristic.CNT_CODE
            };
        }

        private CLRHeader GetCLRHeader(AssemblyDefinition assemblyDef, TextMap map, MetadataBuildResult metadata)
        {
            return new CLRHeader()
            {
                cb = map[TextSegment.CLRHeader].Size,
                MajorRuntimeVersion = 0x0002,
                MinorRuntimeVersion = 0x0005,
                Metadata = new DataDirectory(
                    map[TextSegment.MetadataHeader].RVA,
                    map.GetOffset(TextSegment.MetadataHeader, TextSegment.DebugDirectory)
                ),
                PEKind = PEFileKinds.ILOnly,
                EntryPointToken = metadata.EntryPointToken
            };
        }

        private MetadataHeader GetMetadataHeader()
        {
            return new MetadataHeader
            {
                Signature = NET_METADATA_SIGNATURE,
                MajorVersion = 0x1,
                MinorVersion = 0x1,
                Reserved = 0x0,
                VersionLength = 0xC,
                VersionString = "v4.0.30319",
                Flags = 0x00,
                NumberOfStreams = 0x05
            };
        }

        private RsrcSection GetRsrcSection()
        {
            var buffer = new Win32ResourceBuffer();
            return new RsrcSection()
            {
                Buffer = buffer,
                Header = GetRsrcHeader(buffer)
            };
        }

        private SectionHeader GetRsrcHeader(Win32ResourceBuffer buffer)
        {
            uint rva = GetNextVirtualAddress(_text);
            return new SectionHeader()
            {
                Name = ".rsrc",
                VirtualSize = (uint)buffer.Length,
                VirtualAddress = rva,
                SizeOfRawData = BitArithmetic.Align((uint)buffer.Length, FILE_ALIGMENT),
                PointerToRawData = _text.Header.PointerToRawData + _text.Header.SizeOfRawData,
                Characteristics = SectionCharacteristic.MEM_READ
                    | SectionCharacteristic.CNT_INITIALIZED_DATA
            };
        }

        private RelocSection GetRelocSection()
        {
            RelocationBuffer buffer = new RelocationBuffer(_text);
            return new RelocSection()
            {
                Buffer = buffer,
                Header = GetRelocHeader(buffer)
            };
        }

        private SectionHeader GetRelocHeader(RelocationBuffer buffer)
        {
            uint rva = GetNextVirtualAddress(_rsrc);
            return new SectionHeader()
            {
                Name = ".reloc",
                VirtualSize = (uint)buffer.Length,
                VirtualAddress = rva,
                SizeOfRawData = BitArithmetic.Align((uint)buffer.Length, FILE_ALIGMENT),
                PointerToRawData = _rsrc.Header.PointerToRawData + _rsrc.Header.SizeOfRawData,
                Characteristics = SectionCharacteristic.MEM_READ
                    | SectionCharacteristic.CNT_INITIALIZED_DATA
                    | SectionCharacteristic.MEM_DISCARDABLE
            };
        }

        private DosHeader GetImageDosHeader()
        {
            return new DosHeader()
            {
                e_magic = DOS_SIGNATURE,
                e_cblp = 0x0090,
                e_cp = 0x0003,
                e_cparhdr = 0x0004,
                e_maxalloc = 0xffff,
                e_sp = 0x00b8,
                e_lfarlc = 0x0040,
                e_res = new ushort[SIZE_OF_DOS_RESERVED_WORDS],
                e_res2 = new ushort[SIZE_OF_DOS_RESERVED_WORDS2],
                e_lfanew = 0x00000080
            };
        }

        private NTHeaders32 GetNTHeaders32()
        {
            return new NTHeaders32()
            {
                Signature = NT_SIGNATURE,
                FileHeader = GetFileHeader(),
                OptionalHeader = GetOptionHeader32()
            };
        }

        private OptionHeader32 GetOptionHeader32()
        {
            return new OptionHeader32()
            {
                Magic = NT_OPTIONAL_HDR32_MAGIC,
                MajorLinkerVersion = 0x0b,
                MinorLinkerVersion = 0x00,
                SizeOfCode = _text.Header.SizeOfRawData,
                SizeOfInitializedData = GetInitializedDataSize(),
                AddressOfEntryPoint = _text.Map[TextSegment.StartupStub].RVA,
                BaseOfCode = _text.Header.VirtualAddress,
                BaseOfData = _rsrc.Header.VirtualAddress,
                ImageBase = IMAGE_BASE,
                SectionAlignment = SECTION_ALIGMENT,
                FileAlignment = FILE_ALIGMENT,
                MajorOperatingSystemVersion = 0x0004,
                MinorOperatingSystemVersion = 0x0000,
                MajorImageVersion = 0x0000,
                MinorImageVersion = 0x0000,
                MajorSubsystemVersion = 0x0004,
                MinorSubsystemVersion = 0x0000,
                SizeOfImage = GetPEImageSize(),
                SizeOfHeaders = GetPEHeaderSize(),
                Subsystem = SUBSYSTEM_WINDOWS_CUI,
                DllCharacteristics = DllCharacteristics.DYNAMIC_BASE
                    | DllCharacteristics.NX_COMPAT
                    | DllCharacteristics.NO_SEH
                    | DllCharacteristics.TERMINAL_SERVER_AWARE,
                SizeOfStackReserve = 0x00100000,
                SizeOfStackCommit = 0x00001000,
                SizeOfHeapReserve = 0x00100000,
                SizeOfHeapCommit = 0x00001000,
                NumberOfRvaAndSizes = NUMBER_OF_DIRECTORY_ENTRIES,
                DataDirectories = GetDataDirectories()
            };
        }

        private uint GetInitializedDataSize()
        {
            return _rsrc.Header.SizeOfRawData +
                _reloc.Header.SizeOfRawData;
        }

        private uint GetPEImageSize()
        {
            return GetNextVirtualAddress(_reloc);
        }

        private uint GetPEHeaderSize()
        {
            const uint size = SIZEOF_DOS_HEADER +
                SIZEOF_NT_OPTIONAL32_HEADER +
                (NUMBER_OF_SECTIONS * SIZEOF_SECTION_HEADER);

            return BitArithmetic.Align(size, FILE_ALIGMENT);
        }

        private DataDirectory[] GetDataDirectories()
        {
            DataDirectory[] dirs = new DataDirectory[NUMBER_OF_DIRECTORY_ENTRIES];
            dirs[DIRECTORY_ENTRY_IMPORT] = _text.Map[TextSegment.ImportDirectory];
            dirs[DIRECTORY_ENTRY_RESOURCE] = new DataDirectory(_rsrc.Header.VirtualAddress, _rsrc.Header.VirtualSize);
            dirs[DIRECTORY_ENTRY_BASERELOC] = new DataDirectory(_reloc.Header.VirtualAddress, _reloc.Header.VirtualSize);
            dirs[DIRECTORY_ENTRY_IAT] = _text.Map[TextSegment.ImportAddressTable];
            dirs[DIRECTORY_ENTRY_COM_DESCRIPTOR] = _text.Map[TextSegment.CLRHeader];

            return dirs;
        }

        private FileHeader GetFileHeader()
        {
            return new FileHeader()
            {
                Machine = FILE_MACHINE_I386,
                NumberOfSections = NUMBER_OF_SECTIONS,
                TimeDateStamp = _options.TimeStamp,
                SizeOfOptionalHeader = SIZEOF_NT_OPTIONAL32_HEADER,
                Characteristics = FileCharacteristics.EXECUTABLE_IMAGE
                    | FileCharacteristics.MACHINE_32BIT
            };
        }

        private uint GetNextVirtualAddress(ISection section)
        {
            return section.Header.VirtualAddress +
                BitArithmetic.Align(section.Header.VirtualSize, SECTION_ALIGMENT);
        }

        private void WriteDOSHeader()
        {
            WriteStruct(GetImageDosHeader());
            Write(DOS_HEADER_STUB);
        }

        private void WritePEFileHeader()
        {
            WriteStruct(GetNTHeaders32());
        }

        private void WriteSectionHeaders()
        {
            WriteStruct(_text.Header);
            WriteStruct(_rsrc.Header);
            WriteStruct(_reloc.Header);
        }

        private void WriteSections()
        {
            WriteTextSection();
            WriteRsrcSection();
            WriteRelocSection();
        }

        private void WriteTextSection()
        {
            MoveTo(TextSegment.ImportAddressTable);
            WriteStruct(_text.Map[TextSegment.ImportHintNameTable]);
            WriteStruct(_text.CLRHeader);

            MoveTo(TextSegment.ILCode);
            WriteBuffer(_text.ILCode);

            MoveTo(TextSegment.Resources);
            WriteBuffer(_text.Resources);

            MoveTo(TextSegment.Data);
            WriteBuffer(_text.Data);

            MoveTo(TextSegment.MetadataHeader);
            WriteMetadataHeader();
            WriteMetadata();

            MoveTo(TextSegment.ImportDirectory);
            WriteImportDirectory();

            MoveTo(TextSegment.StartupStub);
            WriteStartupStub();

            AlignSection(_text.Header);
        }

        private void WriteStartupStub()
        {
            WriteUInt16(0x25ff);
            WriteUInt32(IMAGE_BASE + _text.Map[TextSegment.ImportAddressTable].RVA);
        }

        private void WriteImportDirectory()
        {
            WriteUInt32(_text.Map[TextSegment.ImportDirectory].RVA + 40); // ImportLookupTable
            WriteUInt32(0); // DateTimeStamp
            WriteUInt32(0); // ForwarderChain
            WriteUInt32(_text.Map[TextSegment.ImportHintNameTable].RVA + 14);
            WriteUInt32(_text.Map[TextSegment.ImportAddressTable].RVA);
            Advance(20);
            WriteUInt32(_text.Map[TextSegment.ImportHintNameTable].RVA);

            MoveTo(TextSegment.ImportHintNameTable);
            WriteUInt16(0); // Hint
            WriteBytes(ByteBuffer.ToBytes("_CorExeMain"));
            WriteUInt8(0);
            WriteBytes(ByteBuffer.ToBytes("mscoree.dll"));
            WriteUInt16(0);
        }

        private void WriteMetadata()
        {
            WriteHeap(TextSegment.TableHeap, _text.Metadata.Heaps.Tables);
            WriteHeap(TextSegment.StringHeap, _text.Metadata.Heaps.Strings);
            WriteHeap(TextSegment.UserStringHeap, _text.Metadata.Heaps.UserStrings);
            WriteHeap(TextSegment.GuidHeap, _text.Metadata.Heaps.Guids);
            WriteHeap(TextSegment.BlobHeap, _text.Metadata.Heaps.Blobs);
        }

        private void WriteHeap(TextSegment heap, ByteBuffer buffer)
        {
            MoveTo(heap);
            WriteBuffer(buffer);
        }

        private void WriteMetadataHeader()
        {
            var header = _text.MetadataHeader;
            WriteUInt32(header.Signature);
            WriteUInt16(header.MajorVersion);
            WriteUInt16(header.MinorVersion);
            WriteUInt32(header.Reserved);
            WriteUInt32(header.VersionLength);
            WriteBytes(ByteBuffer.ToZeroEndBytes(header.VersionString));
            WriteUInt16(header.Flags);
            WriteUInt16(header.NumberOfStreams);

            WriteStreamHeader(TextSegment.TableHeap, "#~");
            WriteStreamHeader(TextSegment.StringHeap, "#Strings");
            WriteStreamHeader(TextSegment.UserStringHeap, "#US");
            WriteStreamHeader(TextSegment.GuidHeap, "#GUID");
            WriteStreamHeader(TextSegment.BlobHeap, "#Blob");
        }

        private void WriteStreamHeader(TextSegment heap, string name)
        {
            uint length = _text.Map.GetSize(heap);
            if (length == 0)
                return;

            uint offset = _text.Map.GetOffset(TextSegment.MetadataHeader, heap);
            WriteUInt32(offset);
            WriteUInt32(length);
            WriteBytes(ByteBuffer.ToZeroEndBytes(name));
        }

        private void WriteRsrcSection()
        {
            MoveTo(_rsrc.Header.PointerToRawData);
            WriteBuffer(_rsrc.Buffer);
            AlignSection(_rsrc.Header);
        }

        private void WriteRelocSection()
        {
            MoveTo(_reloc.Header.PointerToRawData);
            WriteBuffer(_reloc.Buffer);
            AlignSection(_reloc.Header);
        }

        private void AlignSection(SectionHeader header)
        {
            int written = (int)BaseStream.Position - (int)header.PointerToRawData;
            int rest = (int)header.SizeOfRawData - written;

            if (rest <= 0)
                return;

            Advance(rest - 1);
            WriteUInt8(0);
        }

        private void MoveTo(TextSegment segment)
        {
            SectionHeader header = _text.Header;
            uint rva = _text.Map[segment].RVA;
            uint raw = rva + header.PointerToRawData - header.VirtualAddress;

            MoveTo(raw);
        }
    }
}
