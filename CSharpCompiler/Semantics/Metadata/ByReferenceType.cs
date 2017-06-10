namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class ByReferenceType : TypeSpecification
    {
        public ByReferenceType(ITypeInfo containedType) : base(containedType)
        {
            Name = containedType.Name + "&";
            ElementType = ElementType.ByRef;
        }
    }
}
