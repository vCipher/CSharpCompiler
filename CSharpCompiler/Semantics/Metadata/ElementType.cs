namespace CSharpCompiler.Semantics.Metadata
{
    /// <summary>
    /// Element type for signature
    /// </summary>
    public enum ElementType : byte
    {
        None = 0x00,
        Void = 0x01,
        Boolean = 0x02,
        Char = 0x03,
        Int8 = 0x04,
        UInt8 = 0x05,
        Int16 = 0x06,
        UInt16 = 0x07,
        Int32 = 0x08,
        UInt32 = 0x09,
        Int64 = 0x0a,
        UInt64 = 0x0b,
        Single = 0x0c,
        Double = 0x0d,
        String = 0x0e,
        Ptr = 0x0f,
        ByRef = 0x10,
        ValueType = 0x11,
        Class = 0x12,
        Var = 0x13,
        Array = 0x14,
        GenericInst = 0x15,
        TypedByRef = 0x16,
        IntPtr = 0x18,
        UIntPtr = 0x19,
        FunctionPointer = 0x1b,
        Object = 0x1c,
        SizeArray = 0x1d,
        MVar = 0x1e,
        RequiredModifier = 0x1f,
        OptionalModifier = 0x20,
        Internal = 0x21,
        Modifier = 0x40,
        Sentinel = 0x41,
        Pinned = 0x45,
        Type = 0x50,
        Boxed = 0x51,
        Enum = 0x55
    }
}
