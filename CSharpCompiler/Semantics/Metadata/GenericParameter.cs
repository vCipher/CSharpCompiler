using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class GenericParameter : ITypeInfo
    {
        public int Index { get; private set; }
        public string Name { get; private set; }
        public string Namespace { get; private set; }
        public ElementType ElementType { get; private set; }
        public IAssemblyInfo Assembly { get; private set; }

        public GenericParameter(int index, ElementType elementType)
        {
            Index = index;
            Name = string.Empty;
            Namespace = string.Empty;
            ElementType = elementType;
            Assembly = null;
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IMetadataEntity other)
        {
            throw new NotImplementedException();
        }
    }
}
