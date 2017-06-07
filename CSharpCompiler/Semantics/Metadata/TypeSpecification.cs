namespace CSharpCompiler.Semantics.Metadata
{
    public class TypeSpecification : ITypeInfo
    {
        public string Name { get; private set; }
        public string Namespace { get; private set; }
        public ElementType ElementType { get; private set; }
        public IAssemblyInfo Assembly { get; private set; }
        public ITypeInfo ContainedType { get; private set; }

        public TypeSpecification(string name, string @namespace, ElementType elementType, ITypeInfo containedType)
        {
            Name = name;
            Namespace = Namespace;
            ElementType = elementType;
            ContainedType = containedType;
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitTypeSpecification(this);
        }

        public override int GetHashCode()
        {
            return TypeInfoComparer.Default.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return (obj is TypeSpecification) && Equals((TypeSpecification)obj);
        }

        public bool Equals(ITypeInfo other)
        {
            return (other is TypeSpecification) && Equals((TypeSpecification)other);
        }

        public bool Equals(TypeSpecification other)
        {
            return TypeInfoComparer.Default.Equals(this, other);
        }
    }
}
