using CSharpCompiler.Semantics.Metadata;
using System.Runtime.InteropServices;

namespace CSharpCompiler.CodeGen.Metadata.Tables.TypeRef
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TypeDefRow
    {
        public TypeAttributes Attributes;
        public ushort Name;
        public ushort Namespace;
        public ushort Extends;
        public ushort FieldList;
        public ushort MethodList;
    }
}
