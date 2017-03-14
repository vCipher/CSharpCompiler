using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DosHeader
    {
        /// <summary>
        /// 00: MZ Header signature
        /// </summary>
        public ushort e_magic;
        /// <summary>
        /// 02: Bytes on last page of file
        /// </summary>
        public ushort e_cblp;
        /// <summary>
        /// 04: Pages in file
        /// </summary>
        public ushort e_cp;
        /// <summary>
        /// 06: Relocations
        /// </summary>
        public ushort e_crlc;
        /// <summary>
        /// 08: Size of header in paragraphs
        /// </summary>
        public ushort e_cparhdr;
        /// <summary>
        /// 0a: Minimum extra paragraphs needed
        /// </summary>
        public ushort e_minalloc;
        /// <summary>
        /// 0c: Maximum extra paragraphs needed
        /// </summary>
        public ushort e_maxalloc;
        /// <summary>
        /// 0e: Initial (relative) SS value
        /// </summary>
        public ushort e_ss;
        /// <summary>
        /// 10: Initial SP value
        /// </summary>
        public ushort e_sp;
        /// <summary>
        /// 12: Checksum
        /// </summary>
        public ushort e_csum;
        /// <summary>
        /// 14: Initial IP value
        /// </summary>
        public ushort e_ip;
        /// <summary>
        /// 16: Initial (relative) CS value
        /// </summary>
        public ushort e_cs;
        /// <summary>
        /// 18: File address of relocation table
        /// </summary>
        public ushort e_lfarlc;
        /// <summary>
        /// 1a: Overlay number
        /// </summary>
        public ushort e_ovno;
        /// <summary>
        /// 1c: Reserved words
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] e_res;
        /// <summary>
        /// 24: OEM identifier (for e_oeminfo)
        /// </summary>
        public ushort e_oemid;
        /// <summary>
        /// 26: OEM information; e_oemid specific
        /// </summary>
        public ushort e_oeminfo;
        /// <summary>
        /// 28: Reserved words
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public ushort[] e_res2;
        /// <summary>
        /// 3c: Offset to extended header
        /// </summary>
        public uint e_lfanew;
    }
}
