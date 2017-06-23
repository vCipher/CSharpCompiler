﻿using CSharpCompiler.Utility;

namespace CSharpCompiler.PE.Sections.Resources
{
    public sealed class Win32ResourceBuffer : ByteBuffer
    {
        public Win32ResourceBuffer() : base(new byte[]
        {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00,
            0x10, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x80, 0x18, 0x00, 0x00, 0x00, 0x38, 0x00, 0x00, 0x80,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x00, 0x00, 0x50, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x68, 0x00, 0x00, 0x80,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x90, 0x00, 0x00, 0x00,
            0xa0, 0x40, 0x00, 0x00, 0x44, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0xe8, 0x42, 0x00, 0x00, 0xea, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x44, 0x02, 0x34, 0x00, 0x00, 0x00, 0x56, 0x00, 0x53, 0x00, 0x5f, 0x00, 0x56, 0x00, 0x45, 0x00,
            0x52, 0x00, 0x53, 0x00, 0x49, 0x00, 0x4f, 0x00, 0x4e, 0x00, 0x5f, 0x00, 0x49, 0x00, 0x4e, 0x00,
            0x46, 0x00, 0x4f, 0x00, 0x00, 0x00, 0x00, 0x00, 0xbd, 0x04, 0xef, 0xfe, 0x00, 0x00, 0x01, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x3f, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x44, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x56, 0x00, 0x61, 0x00, 0x72, 0x00, 0x46, 0x00, 0x69, 0x00, 0x6c, 0x00, 0x65, 0x00,
            0x49, 0x00, 0x6e, 0x00, 0x66, 0x00, 0x6f, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x00, 0x04, 0x00,
            0x00, 0x00, 0x54, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6e, 0x00, 0x73, 0x00, 0x6c, 0x00, 0x61, 0x00,
            0x74, 0x00, 0x69, 0x00, 0x6f, 0x00, 0x6e, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xb0, 0x04,
            0xa4, 0x01, 0x00, 0x00, 0x01, 0x00, 0x53, 0x00, 0x74, 0x00, 0x72, 0x00, 0x69, 0x00, 0x6e, 0x00,
            0x67, 0x00, 0x46, 0x00, 0x69, 0x00, 0x6c, 0x00, 0x65, 0x00, 0x49, 0x00, 0x6e, 0x00, 0x66, 0x00,
            0x6f, 0x00, 0x00, 0x00, 0x80, 0x01, 0x00, 0x00, 0x01, 0x00, 0x30, 0x00, 0x30, 0x00, 0x30, 0x00,
            0x30, 0x00, 0x30, 0x00, 0x34, 0x00, 0x62, 0x00, 0x30, 0x00, 0x00, 0x00, 0x2c, 0x00, 0x02, 0x00,
            0x01, 0x00, 0x46, 0x00, 0x69, 0x00, 0x6c, 0x00, 0x65, 0x00, 0x44, 0x00, 0x65, 0x00, 0x73, 0x00,
            0x63, 0x00, 0x72, 0x00, 0x69, 0x00, 0x70, 0x00, 0x74, 0x00, 0x69, 0x00, 0x6f, 0x00, 0x6e, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x30, 0x00, 0x08, 0x00, 0x01, 0x00, 0x46, 0x00,
            0x69, 0x00, 0x6c, 0x00, 0x65, 0x00, 0x56, 0x00, 0x65, 0x00, 0x72, 0x00, 0x73, 0x00, 0x69, 0x00,
            0x6f, 0x00, 0x6e, 0x00, 0x00, 0x00, 0x00, 0x00, 0x30, 0x00, 0x2e, 0x00, 0x30, 0x00, 0x2e, 0x00,
            0x30, 0x00, 0x2e, 0x00, 0x30, 0x00, 0x00, 0x00, 0x38, 0x00, 0x0b, 0x00, 0x01, 0x00, 0x49, 0x00,
            0x6e, 0x00, 0x74, 0x00, 0x65, 0x00, 0x72, 0x00, 0x6e, 0x00, 0x61, 0x00, 0x6c, 0x00, 0x4e, 0x00,
            0x61, 0x00, 0x6d, 0x00, 0x65, 0x00, 0x00, 0x00, 0x54, 0x00, 0x61, 0x00, 0x72, 0x00, 0x67, 0x00,
            0x65, 0x00, 0x74, 0x00, 0x2e, 0x00, 0x65, 0x00, 0x78, 0x00, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x28, 0x00, 0x02, 0x00, 0x01, 0x00, 0x4c, 0x00, 0x65, 0x00, 0x67, 0x00, 0x61, 0x00, 0x6c, 0x00,
            0x43, 0x00, 0x6f, 0x00, 0x70, 0x00, 0x79, 0x00, 0x72, 0x00, 0x69, 0x00, 0x67, 0x00, 0x68, 0x00,
            0x74, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x40, 0x00, 0x0b, 0x00, 0x01, 0x00, 0x4f, 0x00,
            0x72, 0x00, 0x69, 0x00, 0x67, 0x00, 0x69, 0x00, 0x6e, 0x00, 0x61, 0x00, 0x6c, 0x00, 0x46, 0x00,
            0x69, 0x00, 0x6c, 0x00, 0x65, 0x00, 0x6e, 0x00, 0x61, 0x00, 0x6d, 0x00, 0x65, 0x00, 0x00, 0x00,
            0x54, 0x00, 0x61, 0x00, 0x72, 0x00, 0x67, 0x00, 0x65, 0x00, 0x74, 0x00, 0x2e, 0x00, 0x65, 0x00,
            0x78, 0x00, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00, 0x34, 0x00, 0x08, 0x00, 0x01, 0x00, 0x50, 0x00,
            0x72, 0x00, 0x6f, 0x00, 0x64, 0x00, 0x75, 0x00, 0x63, 0x00, 0x74, 0x00, 0x56, 0x00, 0x65, 0x00,
            0x72, 0x00, 0x73, 0x00, 0x69, 0x00, 0x6f, 0x00, 0x6e, 0x00, 0x00, 0x00, 0x30, 0x00, 0x2e, 0x00,
            0x30, 0x00, 0x2e, 0x00, 0x30, 0x00, 0x2e, 0x00, 0x30, 0x00, 0x00, 0x00, 0x38, 0x00, 0x08, 0x00,
            0x01, 0x00, 0x41, 0x00, 0x73, 0x00, 0x73, 0x00, 0x65, 0x00, 0x6d, 0x00, 0x62, 0x00, 0x6c, 0x00,
            0x79, 0x00, 0x20, 0x00, 0x56, 0x00, 0x65, 0x00, 0x72, 0x00, 0x73, 0x00, 0x69, 0x00, 0x6f, 0x00,
            0x6e, 0x00, 0x00, 0x00, 0x30, 0x00, 0x2e, 0x00, 0x30, 0x00, 0x2e, 0x00, 0x30, 0x00, 0x2e, 0x00,
            0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xef, 0xbb, 0xbf, 0x3c, 0x3f, 0x78, 0x6d, 0x6c,
            0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6f, 0x6e, 0x3d, 0x22, 0x31, 0x2e, 0x30, 0x22, 0x20, 0x65,
            0x6e, 0x63, 0x6f, 0x64, 0x69, 0x6e, 0x67, 0x3d, 0x22, 0x55, 0x54, 0x46, 0x2d, 0x38, 0x22, 0x20,
            0x73, 0x74, 0x61, 0x6e, 0x64, 0x61, 0x6c, 0x6f, 0x6e, 0x65, 0x3d, 0x22, 0x79, 0x65, 0x73, 0x22,
            0x3f, 0x3e, 0x0d, 0x0a, 0x3c, 0x61, 0x73, 0x73, 0x65, 0x6d, 0x62, 0x6c, 0x79, 0x20, 0x78, 0x6d,
            0x6c, 0x6e, 0x73, 0x3d, 0x22, 0x75, 0x72, 0x6e, 0x3a, 0x73, 0x63, 0x68, 0x65, 0x6d, 0x61, 0x73,
            0x2d, 0x6d, 0x69, 0x63, 0x72, 0x6f, 0x73, 0x6f, 0x66, 0x74, 0x2d, 0x63, 0x6f, 0x6d, 0x3a, 0x61,
            0x73, 0x6d, 0x2e, 0x76, 0x31, 0x22, 0x20, 0x6d, 0x61, 0x6e, 0x69, 0x66, 0x65, 0x73, 0x74, 0x56,
            0x65, 0x72, 0x73, 0x69, 0x6f, 0x6e, 0x3d, 0x22, 0x31, 0x2e, 0x30, 0x22, 0x3e, 0x0d, 0x0a, 0x20,
            0x20, 0x3c, 0x61, 0x73, 0x73, 0x65, 0x6d, 0x62, 0x6c, 0x79, 0x49, 0x64, 0x65, 0x6e, 0x74, 0x69,
            0x74, 0x79, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6f, 0x6e, 0x3d, 0x22, 0x31, 0x2e, 0x30, 0x2e,
            0x30, 0x2e, 0x30, 0x22, 0x20, 0x6e, 0x61, 0x6d, 0x65, 0x3d, 0x22, 0x4d, 0x79, 0x41, 0x70, 0x70,
            0x6c, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x61, 0x70, 0x70, 0x22, 0x2f, 0x3e, 0x0d,
            0x0a, 0x20, 0x20, 0x3c, 0x74, 0x72, 0x75, 0x73, 0x74, 0x49, 0x6e, 0x66, 0x6f, 0x20, 0x78, 0x6d,
            0x6c, 0x6e, 0x73, 0x3d, 0x22, 0x75, 0x72, 0x6e, 0x3a, 0x73, 0x63, 0x68, 0x65, 0x6d, 0x61, 0x73,
            0x2d, 0x6d, 0x69, 0x63, 0x72, 0x6f, 0x73, 0x6f, 0x66, 0x74, 0x2d, 0x63, 0x6f, 0x6d, 0x3a, 0x61,
            0x73, 0x6d, 0x2e, 0x76, 0x32, 0x22, 0x3e, 0x0d, 0x0a, 0x20, 0x20, 0x20, 0x20, 0x3c, 0x73, 0x65,
            0x63, 0x75, 0x72, 0x69, 0x74, 0x79, 0x3e, 0x0d, 0x0a, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x3c,
            0x72, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x65, 0x64, 0x50, 0x72, 0x69, 0x76, 0x69, 0x6c, 0x65,
            0x67, 0x65, 0x73, 0x20, 0x78, 0x6d, 0x6c, 0x6e, 0x73, 0x3d, 0x22, 0x75, 0x72, 0x6e, 0x3a, 0x73,
            0x63, 0x68, 0x65, 0x6d, 0x61, 0x73, 0x2d, 0x6d, 0x69, 0x63, 0x72, 0x6f, 0x73, 0x6f, 0x66, 0x74,
            0x2d, 0x63, 0x6f, 0x6d, 0x3a, 0x61, 0x73, 0x6d, 0x2e, 0x76, 0x33, 0x22, 0x3e, 0x0d, 0x0a, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x3c, 0x72, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x65,
            0x64, 0x45, 0x78, 0x65, 0x63, 0x75, 0x74, 0x69, 0x6f, 0x6e, 0x4c, 0x65, 0x76, 0x65, 0x6c, 0x20,
            0x6c, 0x65, 0x76, 0x65, 0x6c, 0x3d, 0x22, 0x61, 0x73, 0x49, 0x6e, 0x76, 0x6f, 0x6b, 0x65, 0x72,
            0x22, 0x20, 0x75, 0x69, 0x41, 0x63, 0x63, 0x65, 0x73, 0x73, 0x3d, 0x22, 0x66, 0x61, 0x6c, 0x73,
            0x65, 0x22, 0x2f, 0x3e, 0x0d, 0x0a, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x3c, 0x2f, 0x72, 0x65,
            0x71, 0x75, 0x65, 0x73, 0x74, 0x65, 0x64, 0x50, 0x72, 0x69, 0x76, 0x69, 0x6c, 0x65, 0x67, 0x65,
            0x73, 0x3e, 0x0d, 0x0a, 0x20, 0x20, 0x20, 0x20, 0x3c, 0x2f, 0x73, 0x65, 0x63, 0x75, 0x72, 0x69,
            0x74, 0x79, 0x3e, 0x0d, 0x0a, 0x20, 0x20, 0x3c, 0x2f, 0x74, 0x72, 0x75, 0x73, 0x74, 0x49, 0x6e,
            0x66, 0x6f, 0x3e, 0x0d, 0x0a, 0x3c, 0x2f, 0x61, 0x73, 0x73, 0x65, 0x6d, 0x62, 0x6c, 0x79, 0x3e,
            0x0d, 0x0a, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        }) { }
    }
}