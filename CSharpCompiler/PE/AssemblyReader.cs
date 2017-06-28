using CSharpCompiler.PE.Metadata;
using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Sections;
using CSharpCompiler.PE.Sections.Text;
using CSharpCompiler.Semantics.Metadata;
using System;
using System.IO;

using static CSharpCompiler.PE.Constants;

namespace CSharpCompiler.PE
{
    public sealed class AssemblyReader : PEReader
    {
        private DosHeader _dosHeader;
        private FileHeader _peHeader;
        private CLRHeader _clrHeader;
        private MetadataHeader _metadataHeader;

        private SectionHeader[] _sectionHeaders;
        private DataDirectory[] _directories;

        private MetadataReader _metadata;

        public AssemblyReader(Stream input) : base(input)
        { }

        public AssemblyDefinition ReadAssembly()
        {
            ReadDosHeader();
            ReadPEFileHeader();
            ReadSectionHeaders();
            ReadSections();

            return ResolveAssembly();
        }

        private AssemblyDefinition ResolveAssembly()
        {
            return new MetadataSystem(_metadata).Assembly;
        }

        private void ReadDosHeader()
        {
            _dosHeader = ReadStruct<DosHeader>();
            MoveTo(_dosHeader.e_lfanew);
        }

        private void ReadPEFileHeader()
        {
            if (ReadUInt32() != NT_SIGNATURE)
                throw new BadImageFormatException();

            _peHeader = ReadStruct<FileHeader>();
            ReadOptionHeader();
        }

        private void ReadOptionHeader()
        {
            var isPe64 = ReadUInt16() == NT_OPTIONAL_HDR64_MAGIC;
            Advance(-2); // Move backward to correct struct deserialization

            if (isPe64)
            {
                var header = ReadStruct<OptionHeader64>();
                _directories = header.DataDirectories;
            }
            else
            {
                var header = ReadStruct<OptionHeader32>();
                _directories = header.DataDirectories;
            }

            if (_directories[DIRECTORY_ENTRY_COM_DESCRIPTOR].IsZero())
                throw new BadImageFormatException();
        }

        private void ReadSectionHeaders()
        {
            _sectionHeaders = new SectionHeader[_peHeader.NumberOfSections];
            for (int i = 0; i < _peHeader.NumberOfSections; i++)
            {
                _sectionHeaders[i] = ReadStruct<SectionHeader>();
            }
        }

        private void ReadSections()
        {
            ReadTextSection();
        }

        private void ReadTextSection()
        {
            ReadCLRHeader();
            ReadMetadataHeader();
            ReadMetadataStreams();
        }

        private void ReadCLRHeader()
        {
            var clr = _directories[DIRECTORY_ENTRY_COM_DESCRIPTOR];
            MoveTo(ResolveVirtualAddress(clr.RVA));
            _clrHeader = ReadStruct<CLRHeader>();
            _metadata = new MetadataReader(BaseStream, _clrHeader.EntryPointToken);
        }

        private void ReadMetadataHeader()
        {
            MoveTo(ResolveVirtualAddress(_clrHeader.Metadata.RVA));

            _metadataHeader = new MetadataHeader();
            _metadataHeader.Signature = ReadUInt32();
            _metadataHeader.MajorVersion = ReadUInt16();
            _metadataHeader.MinorVersion = ReadUInt16();
            _metadataHeader.Reserved = ReadUInt32();
            _metadataHeader.VersionLength = ReadUInt32();
            _metadataHeader.VersionString = ReadZeroTerminatedString((int)_metadataHeader.VersionLength);
            _metadataHeader.Flags = ReadUInt16();
            _metadataHeader.NumberOfStreams = ReadUInt16();

            if (_metadataHeader.Signature != NET_METADATA_SIGNATURE)
                throw new BadImageFormatException();
        }

        private void ReadMetadataStreams()
        {
            SectionHeader header;
            if (!TryGetSectionAtVirtualAddress(_clrHeader.Metadata.RVA, out header))
                throw new BadImageFormatException();
            
            for (int i = 0; i < _metadataHeader.NumberOfStreams; i++)
            {
                ReadMetadataStream(header);
            }

            _metadata.TableReader.ReadMetadataTablesContent();
        }

        private void ReadMetadataStream(SectionHeader sectionHeader)
        {
            var offset = ReadUInt32();
            var size = ReadInt32();
            var name = ReadAlignedString(METADATA_STREAM_NAME_LIMIT);
            var position = (uint)BaseStream.Position;

            MoveTo(ResolveVirtualAddress(_clrHeader.Metadata.RVA + offset));

            switch (name)
            {
                case "#~":
                case "#-":
                    _metadata.TableReader.ReadMetadataTablesSchema();
                    break;
                case "#Strings":
                    _metadata.Strings.WriteBytes(ReadBytes(size));
                    break;
                case "#Blob":
                    _metadata.Blobs.WriteBytes(ReadBytes(size));
                    break;
                case "#GUID":
                    _metadata.Guids.WriteBytes(ReadBytes(size));
                    break;
                case "#US":
                    _metadata.UserStrings.WriteBytes(ReadBytes(size));
                    break;
            }

            MoveTo(position);
        }

        private uint ResolveVirtualAddress(uint rva)
        {
            SectionHeader header;
            if (!TryGetSectionAtVirtualAddress(rva, out header))
                throw new BadImageFormatException();

            return rva + header.PointerToRawData - header.VirtualAddress;
        }

        private bool TryGetSectionAtVirtualAddress(uint rva, out SectionHeader header)
        {
            for (int i = 0; i < _sectionHeaders.Length; i++)
            {
                var candidate = _sectionHeaders[i];
                var upLimit = candidate.VirtualAddress + candidate.SizeOfRawData;
                var bottomLimit = candidate.VirtualAddress;

                if (rva >= bottomLimit && rva < upLimit)
                {
                    header = candidate;
                    return true;
                }
            }

            header = default(SectionHeader);
            return false;
        }
    }
}