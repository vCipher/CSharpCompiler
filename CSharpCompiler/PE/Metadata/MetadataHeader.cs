namespace CSharpCompiler.PE.Metadata
{
    public struct MetadataHeader
    {
        public uint Signature;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public uint Reserved;
        public uint VersionLength;
        public string VersionString;
        public ushort Flags;
        public ushort NumberOfStreams;
    }
}
