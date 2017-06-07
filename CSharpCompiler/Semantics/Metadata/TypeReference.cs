namespace CSharpCompiler.Semantics.Metadata
{
    public class TypeReference : ITypeInfo
    {
        public string Name { get; private set; }
        public string Namespace { get; private set; }
        public ElementType ElementType { get; private set; }
        public IAssemblyInfo Assembly { get; private set; }
        
        public TypeReference(string name, string @namespace, ElementType elementType, IAssemblyInfo assembly)
        {
            Name = name;
            Namespace = @namespace;
            ElementType = elementType;
            Assembly = assembly;
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitTypeReference(this);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}, {2}", Namespace, Name, Assembly);
        }

        public override int GetHashCode()
        {
            return TypeInfoComparer.Default.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return (obj is TypeReference) && TypeInfoComparer.Default.Equals(this, (TypeReference)obj);
        }

        public bool Equals(ITypeInfo other)
        {
            return (other is TypeReference) && Equals((TypeReference)other);
        }

        public bool Equals(TypeReference other)
        {
            return TypeInfoComparer.Default.Equals(this, other);
        }
    }
}
