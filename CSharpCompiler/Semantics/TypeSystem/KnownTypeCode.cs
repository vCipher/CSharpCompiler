namespace CSharpCompiler.Semantics.TypeSystem
{
    public enum KnownTypeCode
    {
        /// <summary>
        /// Not one of the known types.
        /// </summary>
        None,
        /// <summary>
        /// object (System.Object)
        /// </summary>
        Object,
        /// <summary>
        /// bool (System.Boolean)
        /// </summary>
        Boolean,
        /// <summary>
        /// char (System.Char)
        /// </summary>
        Char,
        /// <summary>
        /// sbyte (System.SByte)
        /// </summary>
        SByte,
        /// <summary>
        /// byte (System.Byte)
        /// </summary>
        Byte,
        /// <summary>
        /// short (System.Int16)
        /// </summary>
        Int16,
        /// <summary>
        /// ushort (System.UInt16)
        /// </summary>
        UInt16,
        /// <summary>
        /// int (System.Int32)
        /// </summary>
        Int32,
        /// <summary>
        /// uint (System.UInt32)
        /// </summary>
        UInt32,
        /// <summary>
        /// long (System.Int64)
        /// </summary>
        Int64,
        /// <summary>
        /// ulong (System.UInt64)
        /// </summary>
        UInt64,
        /// <summary>
        /// native unsigned int
        /// </summary>
        UIntPtr,
        /// <summary>
        /// native int
        /// </summary>
        IntPtr,
        /// <summary>
        /// float (System.Single)
        /// </summary>
        Single,
        /// <summary>
        /// double (System.Double)
        /// </summary>
        Double,
        /// <summary>
        /// decimal (System.Decimal)
        /// </summary>
        Decimal,
        /// <summary>
        /// string (System.String)
        /// </summary>
        String,
        /// <summary>
        /// void (System.Void)
        /// </summary>
        Void,
        /// <summary>
        /// System.Type
        /// </summary>
        Type,
        /// <summary>
        /// System.Array
        /// </summary>
        Array,
        /// <summary>
        /// System.Attribute
        /// </summary>
        Attribute,
        /// <summary>
        /// System.ValueType
        /// </summary>
        ValueType,
        /// <summary>
        /// System.Enum
        /// </summary>
        Enum,
        /// <summary>
        /// System.Delegate
        /// </summary>
        Delegate
    }
}
