namespace CSharpCompiler.Semantics.TypeSystem
{
    public static class TypeFactory
    {
        public static IType Create(KnownTypeCode typeCode)
        {
            if (typeCode == KnownTypeCode.None)
                return new UserType();

            return new KnownType(typeCode);
        }
    }
}
