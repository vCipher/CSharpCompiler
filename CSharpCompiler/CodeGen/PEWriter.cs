using CSharpCompiler.CodeGen.Sections;
using CSharpCompiler.CodeGen.Sections.Relocations;
using CSharpCompiler.CodeGen.Sections.Resources;
using CSharpCompiler.CodeGen.Sections.Text;
using CSharpCompiler.CodeGen.Metadata;
using CSharpCompiler.Semantics.Metadata;
using System.IO;
using System;
using CSharpCompiler.Utility;

namespace CSharpCompiler.CodeGen
{
    public sealed class PEWriter : BinaryWriter
    {
        #region Win32 definitions for Windows NT
        private const ushort DOS_SIGNATURE = 0x5a4d;        // MZ
        private const uint NT_SIGNATURE = 0x00004550;	    // PE00
        
        private const ushort FILE_MACHINE_UNKNOWN = 0x0000;
        private const ushort FILE_MACHINE_I386 = 0x014c;
        
        private const ushort SIZEOF_FILE_HEADER = 20;
        private const ushort SIZEOF_DOS_HEADER = 128;
        private const ushort SIZEOF_ROM_OPTIONAL_HEADER = 56;
        private const ushort SIZEOF_STD_OPTIONAL_HEADER = 28;
        private const ushort SIZEOF_NT_OPTIONAL32_HEADER = 224;
        private const ushort SIZEOF_NT_OPTIONAL64_HEADER = 240;
        private const ushort SIZEOF_SHORT_NAME = 8;
        private const ushort SIZEOF_SECTION_HEADER = 40;
        private const ushort SIZEOF_SYMBOL = 18;
        private const ushort SIZEOF_AUX_SYMBOL = 18;
        private const ushort SIZEOF_RELOCATION = 10;
        private const ushort SIZEOF_BASE_RELOCATION = 8;
        private const ushort SIZEOF_LINENUMBER = 6;
        private const ushort SIZEOF_ARCHIVE_MEMBER_HDR = 60;

        private const ushort NT_OPTIONAL_HDR32_MAGIC = 0x10b;
        private const ushort NT_OPTIONAL_HDR64_MAGIC = 0x20b;

        private const ushort FILE_EXPORT_DIRECTORY = 0;
        private const ushort FILE_IMPORT_DIRECTORY = 1;
        private const ushort FILE_RESOURCE_DIRECTORY = 2;
        private const ushort FILE_EXCEPTION_DIRECTORY = 3;
        private const ushort FILE_SECURITY_DIRECTORY = 4;
        private const ushort FILE_BASE_RELOCATION_TABLE = 5;
        private const ushort FILE_DEBUG_DIRECTORY = 6;
        private const ushort FILE_DESCRIPTION_STRING = 7;
        private const ushort FILE_MACHINE_VALUE = 8;            // Mips
        private const ushort FILE_THREAD_LOCAL_STORAGE = 9;
        private const ushort FILE_CALLBACK_DIRECTORY = 10;
        
        private const ushort DIRECTORY_ENTRY_EXPORT = 0;
        private const ushort DIRECTORY_ENTRY_IMPORT = 1;
        private const ushort DIRECTORY_ENTRY_RESOURCE = 2;
        private const ushort DIRECTORY_ENTRY_EXCEPTION = 3;
        private const ushort DIRECTORY_ENTRY_SECURITY = 4;
        private const ushort DIRECTORY_ENTRY_BASERELOC = 5;
        private const ushort DIRECTORY_ENTRY_DEBUG = 6;
        private const ushort DIRECTORY_ENTRY_COPYRIGHT = 7;
        private const ushort DIRECTORY_ENTRY_GLOBALPTR = 8;     // (MIPS GP)
        private const ushort DIRECTORY_ENTRY_TLS = 9;
        private const ushort DIRECTORY_ENTRY_LOAD_CONFIG = 10;
        private const ushort DIRECTORY_ENTRY_BOUND_IMPORT = 11;
        private const ushort DIRECTORY_ENTRY_IAT = 12;          // Import Address Table
        private const ushort DIRECTORY_ENTRY_DELAY_IMPORT = 13;
        private const ushort DIRECTORY_ENTRY_COM_DESCRIPTOR = 14;

        private const ushort SUBSYSTEM_UNKNOWN = 0;
        private const ushort SUBSYSTEM_WINDOWS_GUI = 2;         // Windows GUI subsystem
        private const ushort SUBSYSTEM_WINDOWS_CUI = 3;         // Windows character subsystem
        #endregion

        #region Custom definitions
        private const ushort NUMBER_OF_SECTIONS = 3;
        private const ushort NUMBER_OF_DIRECTORY_ENTRIES = 16;

        private const uint IMAGE_BASE = 0x00400000;
        private const uint TEXT_SECTION_BASE = 0x00002000;

        private const uint FILE_ALIGMENT = 0x00000200;
        private const uint SECTION_ALIGMENT = 0x00002000;
        private const uint NET_METADATA_SIGNATURE = 0x424a5342;
        private const int SIZE_OF_DOS_RESERVED_WORDS = 4;
        private const int SIZE_OF_DOS_RESERVED_WORDS2 = 10;
        #endregion

        private CompilationOptions _options;
        private TextSection _text;
        private RsrcSection _rsrc;
        private RelocSection _reloc;
        
        public PEWriter(AssemblyDefinition assemblyDef, Stream output) 
            : this(assemblyDef, output, new CompilationOptions())
        { }

        public PEWriter(AssemblyDefinition assemblyDef, Stream output, CompilationOptions options) 
            : base(output)
        {
            _options = options;
            _text = GetTextSection(assemblyDef);
            _rsrc = GetRsrcSection();
            _reloc = GetRelocSection();
        }

        public void Write()
        {
            WriteDOSHeader();
            WritePEFileHeader();
            WriteSectionHeaders();
            WriteSections();
        }

        private TextSection GetTextSection(AssemblyDefinition assemblyDef)
        {
            ByteBuffer resources = new ByteBuffer();
            ByteBuffer data = new ByteBuffer();
            MetadataContainer metadata = MetadataBuilder.Build(assemblyDef);
            TextMap map = GetTextMap(metadata, resources, data);

            return new TextSection()
            {
                Header = GetTextHeader(map),
                CLRHeader = GetCLRHeader(assemblyDef, map, metadata),
                Map = map,
                Metadata = metadata,
                Resources = resources,
                Data = data
            };
        }

        private TextMap GetTextMap(MetadataContainer metadata, ByteBuffer resources, ByteBuffer data)
        {
            TextMap map = new TextMap
            {
                { TextSegment.ImportAddressTable, 8 },
                { TextSegment.CLRHeader, ByteBuffer.SizeOf<CLRHeader>(), 8 },
                { TextSegment.ILCode, (uint)metadata.ILCode.Length, 4 },
                { TextSegment.Resources, (uint)resources.Length, 8 },
                { TextSegment.Data, (uint)data.Length, 4 },
                { TextSegment.StrongNameSignature, 0, 4 },
                { TextSegment.MetadataHeader, GetMetadataHeaderLength() },
                { TextSegment.TableHeap, (uint)metadata.Tables.Length, 4 },
                { TextSegment.StringHeap, (uint)metadata.Strings.Length, 4 },
                { TextSegment.UserStringHeap, (uint)metadata.UserStrings.Length, 4 },
                { TextSegment.GuidHeap, (uint)metadata.Guids.Length, 4 },
                { TextSegment.BlobHeap, (uint)metadata.Blobs.Length, 4 },
                { TextSegment.DebugDirectory, 0, 4 }
            };

            uint importDirRVA = map.GetNextVirtualAddress(TextSegment.DebugDirectory);
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
                Characteristics = SectionCharacteristic.MEM_READ |
                    SectionCharacteristic.MEM_EXECUTE |
                    SectionCharacteristic.CNT_CODE
            };
        }

        private CLRHeader GetCLRHeader(AssemblyDefinition assemblyDef, TextMap map, MetadataContainer metadata)
        {
            return new CLRHeader()
            {
                cb = map[TextSegment.CLRHeader].Size,
                MajorRuntimeVersion = 0x0002,
                MinorRuntimeVersion = 0x0005,
                MetaData = new DataDirectory(
                    map[TextSegment.MetadataHeader].VirtualAddress,
                    map.GetOffset(TextSegment.MetadataHeader, TextSegment.DebugDirectory)
                ),
                PEKind = PEFileKinds.ILOnly,
                EntryPointToken = metadata.EntryPointToken
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
                Characteristics = SectionCharacteristic.MEM_READ |
                    SectionCharacteristic.CNT_INITIALIZED_DATA
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
                Characteristics = SectionCharacteristic.MEM_READ |
                SectionCharacteristic.CNT_INITIALIZED_DATA |
                SectionCharacteristic.MEM_DISCARDABLE
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

        private byte[] GetDosStub()
        {
            return new byte[]
            {
                0x0e, 0x1f, 0xba, 0x0e, 0x00, 0xb4, 0x09,
                0xcd, 0x21, 0xb8, 0x01, 0x4c, 0xcd, 0x21,
                0x54, 0x68, 0x69, 0x73, 0x20, 0x70, 0x72,
                0x6f, 0x67, 0x72, 0x61, 0x6d, 0x20, 0x63,
                0x61, 0x6e, 0x6e, 0x6f, 0x74, 0x20, 0x62,
                0x65, 0x20, 0x72, 0x75, 0x6e, 0x20, 0x69,
                0x6e, 0x20, 0x44, 0x4f, 0x53, 0x20, 0x6d,
                0x6f, 0x64, 0x65, 0x2e, 0x0d, 0x0d, 0x0a,
                0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00
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
                AddressOfEntryPoint = _text.Map[TextSegment.StartupStub].VirtualAddress,
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
                DllCharacteristics = DllCharacteristics.DYNAMIC_BASE |
                    DllCharacteristics.NX_COMPAT |
                    DllCharacteristics.NO_SEH |
                    DllCharacteristics.TERMINAL_SERVER_AWARE,
                SizeOfStackReserve = 0x00100000,
                SizeOfStackCommit = 0x00001000,
                SizeOfHeapReserve = 0x00100000,
                SizeOfHeapCommit = 0x00001000,
                NumberOfRvaAndSizes = NUMBER_OF_DIRECTORY_ENTRIES,
                DataDirectory = GetDataDirectories()
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
                Characteristics = FileCharacteristics.EXECUTABLE_IMAGE |
                FileCharacteristics.MACHINE_32BIT
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
            Write(GetDosStub());
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
            WriteBuffer(_text.Metadata.ILCode);
            
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
            WriteUInt32(IMAGE_BASE + _text.Map[TextSegment.ImportAddressTable].VirtualAddress);
        }

        private void WriteImportDirectory()
        {
            WriteUInt32(_text.Map[TextSegment.ImportDirectory].VirtualAddress + 40); // ImportLookupTable
            WriteUInt32(0); // DateTimeStamp
            WriteUInt32(0); // ForwarderChain
            WriteUInt32(_text.Map[TextSegment.ImportHintNameTable].VirtualAddress + 14);
            WriteUInt32(_text.Map[TextSegment.ImportAddressTable].VirtualAddress);
            Advance(20);
            WriteUInt32(_text.Map[TextSegment.ImportHintNameTable].VirtualAddress);
            
            MoveTo(TextSegment.ImportHintNameTable);
            WriteUInt16(0); // Hint
            WriteBytes(ByteBuffer.ConvertToBytes("_CorExeMain"));
            WriteUInt8(0);
            WriteBytes(ByteBuffer.ConvertToBytes("mscoree.dll"));
            WriteUInt16(0);
        }

        private void WriteMetadata()
        {
            WriteHeap(TextSegment.TableHeap, _text.Metadata.Tables);
            WriteHeap(TextSegment.StringHeap, _text.Metadata.Strings);
            WriteHeap(TextSegment.UserStringHeap, _text.Metadata.UserStrings);
            WriteHeap(TextSegment.GuidHeap, _text.Metadata.Guids);
            WriteHeap(TextSegment.BlobHeap, _text.Metadata.Blobs);
        }

        private void WriteHeap(TextSegment heap, ByteBuffer buffer)
        {
            MoveTo(heap);
            WriteBuffer(buffer);
        }

        private void WriteMetadataHeader()
        {
            byte[] version = ByteBuffer.ConvertToZeroEndBytes("v4.0.30319");
            WriteUInt32(NET_METADATA_SIGNATURE);    // Signature
            WriteUInt16(1);                         // MajorVersion
            WriteUInt16(1);                         // MinorVersion
            WriteUInt32(0);                         // Reserved
            WriteUInt32((uint)version.Length);
            WriteBytes(version);
            WriteUInt16(0);                         // Flags
            WriteUInt16(5);                         // NumberOfStreams
            
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
            WriteBytes(ByteBuffer.ConvertToZeroEndBytes(name));
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

        private void WriteUInt32(uint value)
        {
            Write(value);
        }

        private void WriteUInt16(ushort value)
        {
            Write(value);
        }

        private void WriteUInt8(byte value)
        {
            Write(value);
        }

        private void WriteBytes(byte[] value)
        {
            Write(value);
        }

        private void WriteBuffer(ByteBuffer buffer)
        {
            Write(buffer.Buffer, 0, buffer.Length);
        }

        private void WriteStruct<T>(T value) where T : struct
        {
            Write(ByteBuffer.ConvertToBytes(value));
        }

        private void MoveTo(TextSegment segment)
        {
            SectionHeader header = _text.Header;
            uint rva = _text.Map[segment].VirtualAddress;
            uint raw = header.PointerToRawData + rva - header.VirtualAddress;

            BaseStream.Seek(raw, SeekOrigin.Begin);
        }

        private void MoveTo(uint raw)
        {
            BaseStream.Seek(raw, SeekOrigin.Begin);
        }

        private void Advance(int bytes)
        {
            BaseStream.Seek(bytes, SeekOrigin.Current);
        }
    }
}
