namespace CSharpCompiler.PE
{
    public static class Constants
    {
        // Win32 definitions for Windows NT
        public const ushort DOS_SIGNATURE = 0x5a4d;        // MZ
        public const uint NT_SIGNATURE = 0x00004550;	    // PE00
        
        public const ushort FILE_MACHINE_UNKNOWN = 0x0000;
        public const ushort FILE_MACHINE_I386 = 0x014c;
        
        public const ushort SIZEOF_FILE_HEADER = 20;
        public const ushort SIZEOF_DOS_HEADER = 128;
        public const ushort SIZEOF_ROM_OPTIONAL_HEADER = 56;
        public const ushort SIZEOF_STD_OPTIONAL_HEADER = 28;
        public const ushort SIZEOF_NT_OPTIONAL32_HEADER = 224;
        public const ushort SIZEOF_NT_OPTIONAL64_HEADER = 240;
        public const ushort SIZEOF_SHORT_NAME = 8;
        public const ushort SIZEOF_SECTION_HEADER = 40;
        public const ushort SIZEOF_SYMBOL = 18;
        public const ushort SIZEOF_AUX_SYMBOL = 18;
        public const ushort SIZEOF_RELOCATION = 10;
        public const ushort SIZEOF_BASE_RELOCATION = 8;
        public const ushort SIZEOF_LINENUMBER = 6;
        public const ushort SIZEOF_ARCHIVE_MEMBER_HDR = 60;

        public const ushort NT_OPTIONAL_HDR32_MAGIC = 0x10b;
        public const ushort NT_OPTIONAL_HDR64_MAGIC = 0x20b;

        public const ushort FILE_EXPORT_DIRECTORY = 0;
        public const ushort FILE_IMPORT_DIRECTORY = 1;
        public const ushort FILE_RESOURCE_DIRECTORY = 2;
        public const ushort FILE_EXCEPTION_DIRECTORY = 3;
        public const ushort FILE_SECURITY_DIRECTORY = 4;
        public const ushort FILE_BASE_RELOCATION_TABLE = 5;
        public const ushort FILE_DEBUG_DIRECTORY = 6;
        public const ushort FILE_DESCRIPTION_STRING = 7;
        public const ushort FILE_MACHINE_VALUE = 8;            // Mips
        public const ushort FILE_THREAD_LOCAL_STORAGE = 9;
        public const ushort FILE_CALLBACK_DIRECTORY = 10;
        
        public const ushort DIRECTORY_ENTRY_EXPORT = 0;
        public const ushort DIRECTORY_ENTRY_IMPORT = 1;
        public const ushort DIRECTORY_ENTRY_RESOURCE = 2;
        public const ushort DIRECTORY_ENTRY_EXCEPTION = 3;
        public const ushort DIRECTORY_ENTRY_SECURITY = 4;
        public const ushort DIRECTORY_ENTRY_BASERELOC = 5;
        public const ushort DIRECTORY_ENTRY_DEBUG = 6;
        public const ushort DIRECTORY_ENTRY_COPYRIGHT = 7;
        public const ushort DIRECTORY_ENTRY_GLOBALPTR = 8;     // (MIPS GP)
        public const ushort DIRECTORY_ENTRY_TLS = 9;
        public const ushort DIRECTORY_ENTRY_LOAD_CONFIG = 10;
        public const ushort DIRECTORY_ENTRY_BOUND_IMPORT = 11;
        public const ushort DIRECTORY_ENTRY_IAT = 12;          // Import Address Table
        public const ushort DIRECTORY_ENTRY_DELAY_IMPORT = 13;
        public const ushort DIRECTORY_ENTRY_COM_DESCRIPTOR = 14;

        public const ushort SUBSYSTEM_UNKNOWN = 0;
        public const ushort SUBSYSTEM_WINDOWS_GUI = 2;         // Windows GUI subsystem
        public const ushort SUBSYSTEM_WINDOWS_CUI = 3;         // Windows character subsystem

        // Custom definitions
        public const ushort NUMBER_OF_SECTIONS = 3;
        public const ushort NUMBER_OF_DIRECTORY_ENTRIES = 16;

        public const uint IMAGE_BASE = 0x00400000;
        public const uint TEXT_SECTION_BASE = 0x00002000;

        public const uint FILE_ALIGMENT = 0x00000200;
        public const uint SECTION_ALIGMENT = 0x00002000;
        public const uint NET_METADATA_SIGNATURE = 0x424a5342;
        public const int METADATA_STREAM_NAME_LIMIT = 32;
        public const int SIZE_OF_DOS_RESERVED_WORDS = 4;
        public const int SIZE_OF_DOS_RESERVED_WORDS2 = 10;
        public const int SIZE_OF_IAT = 8;

        // PE stubs
    	public static readonly byte[] DOS_HEADER_STUB = new byte[]
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
}