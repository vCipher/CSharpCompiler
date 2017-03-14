using System;

namespace CSharpCompiler.Semantics.Metadata
{
    [Flags]
    public enum CallingConventions : byte
    {
        Default = 0x00,
        VarArg = 0x05,
        Field = 0x06,
        LocalSig = 0x07,
        Property = 0x08,
        Unmanaged = 0x09,
        GenericInst = 0x0a,
        NativeVarArg = 0x0b,
        Max = 0x0c,
        Mask = 0x0f,
        HasThis = 0x20,
        ExplicitThis = 0x40,
        Generic = 0x10
    }
}
