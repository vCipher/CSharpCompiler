namespace CSharpCompiler.Semantics.Metadata
{
    public abstract class TypeSpecification : ITypeInfo
    {
        public string Name { get; protected set; }
        public string Namespace { get; protected set; }
        public ElementType ElementType { get; protected set; }
        public IAssemblyInfo Assembly { get; protected set; }
        public ITypeInfo ContainedType { get; protected set; }

        public TypeSpecification(ITypeInfo containedType)
        {
            Namespace = containedType.Namespace;
            Assembly = containedType.Assembly;
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
            return (obj is ITypeInfo) && Equals((ITypeInfo)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is ITypeInfo) && Equals((ITypeInfo)other);
        }

        public bool Equals(ITypeInfo other)
        {
            return TypeInfoComparer.Default.Equals(this, other);
        }
    }
}
