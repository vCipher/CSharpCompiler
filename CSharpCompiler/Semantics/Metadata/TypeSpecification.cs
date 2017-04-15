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

        public override int GetHashCode()
        {
            return TypeInfoComparer.Default.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TypeSpecification)) return false;
            return Equals((TypeSpecification)obj);
        }

        public bool Equals(ITypeInfo other)
        {
            if (!(other is TypeSpecification)) return false;
            return Equals((TypeSpecification)other);
        }

        public bool Equals(TypeSpecification other)
        {
            return TypeInfoComparer.Default.Equals(this, other);
        }
    }
}
