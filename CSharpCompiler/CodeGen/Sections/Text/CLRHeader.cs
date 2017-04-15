using CSharpCompiler.CodeGen.Metadata;
using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Sections.Text
{
    [StructLayout(LayoutKind.Explicit)]
    public struct CLRHeader
    {
        /// <summary>
        /// Size of the header in bytes.
        /// </summary>
        [FieldOffset(0)]
        public uint cb;
        /// <summary>
        /// Major number of the minimum version of the runtime required to run the program.
        /// </summary>
        [FieldOffset(4)]
        public ushort MajorRuntimeVersion;
        /// <summary>
        /// Minor number of the version of the runtime required to run the program.
        /// </summary>
        [FieldOffset(6)]
        public ushort MinorRuntimeVersion;
        /// <summary>
        /// RVA and size of the metadata.
        /// </summary>
        [FieldOffset(8)]
        public DataDirectory MetaData;
        /// <summary>
        /// Binary flags, discussed in the following section.
        /// </summary>
        [FieldOffset(16)]
        public PEFileKinds PEKind;

        /// <summary>
        /// Metadata identifier (token) of the entry point for the image file.
        /// </summary>
        [FieldOffset(20)]
        public MetadataToken EntryPointToken;
        /// <summary>
        /// In images of version 2.0 and newer, may contain RVA of the embedded native entry point method.
        /// </summary>
        [FieldOffset(20)]
        public uint EntryPointRVA;

        /// <summary>
        /// RVA and size of managed resources.
        /// </summary>
        [FieldOffset(24)]
        public DataDirectory Resources;
        /// <summary>
        /// RVA and size of the hash data for this PE file.
        /// </summary>
        [FieldOffset(32)]
        public DataDirectory StrongNameSignature;
        /// <summary>
        /// RVA and size of the Code Manager table.
        /// </summary>
        [FieldOffset(40)]
        public DataDirectory CodeManagerTable;
        /// <summary>
        /// RVA and size in bytes of an array of virtual table (v-table) fixups.
        /// </summary>
        [FieldOffset(48)]
        public DataDirectory VTableFixups;
        /// <summary>
        /// RVA and size of an array of addresses of jump thunks.
        /// </summary>
        [FieldOffset(56)]
        public DataDirectory ExportAddressTableJumps;
        /// <summary>
        /// Reserved for precompiled images.
        /// </summary>
        [FieldOffset(64)]
        public DataDirectory ManagedNativeHeader;
    }
}
