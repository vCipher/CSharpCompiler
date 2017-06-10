using CSharpCompiler.Semantics.Resolvers;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class FieldReference : IFieldInfo, IMemberReference
    {
        private Lazy<ITypeInfo> _fieldType;
        private Lazy<ITypeInfo> _declaringType;

        public string Name { get; private set; }

        public ITypeInfo FieldType => _fieldType.Value;
        public ITypeInfo DeclaringType => _declaringType.Value;

        public FieldReference(IFieldReferenceResolver resolver)
        {
            Name = resolver.GetName();
            _fieldType = new Lazy<ITypeInfo>(resolver.GetDeclaringType);
            _declaringType = new Lazy<ITypeInfo>(resolver.GetFieldType);
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitFieldReference(this);
        }

        public override int GetHashCode()
        {
            return FieldInfoComparer.Default.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return (obj is FieldReference) && Equals((FieldReference)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is FieldReference) && Equals((FieldReference)other);
        }

        public bool Equals(FieldReference other)
        {
            return FieldInfoComparer.Default.Equals(this, other);
        }
    }
}
