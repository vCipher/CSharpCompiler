using CSharpCompiler.Semantics.Resolvers;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class FieldDefinition : IFieldInfo
    {
        private Lazy<ITypeInfo> _fieldType;
        private Lazy<ITypeInfo> _declaringType;

        public string Name { get; private set; }
        public FieldAttributes Attributes { get; private set; }

        public ITypeInfo FieldType => _fieldType.Value;
        public ITypeInfo DeclaringType => _declaringType.Value;

        public FieldDefinition(IFieldDefinitionResolver resolver)
        {
            Name = resolver.GetName();
            Attributes = resolver.GetAttributes();
            _fieldType = new Lazy<ITypeInfo>(resolver.GetDeclaringType);
            _declaringType = new Lazy<ITypeInfo>(resolver.GetFieldType);
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitFieldDefinition(this);
        }

        public override int GetHashCode()
        {
            return FieldInfoComparer.Default.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return (obj is FieldDefinition) && Equals((FieldDefinition)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is FieldDefinition) && Equals((FieldDefinition)other);
        }

        public bool Equals(FieldDefinition other)
        {
            return FieldInfoComparer.Default.Equals(this, other);
        }
    }
}
