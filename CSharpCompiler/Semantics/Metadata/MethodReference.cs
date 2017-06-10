using CSharpCompiler.Semantics.Resolvers;
using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodReference : IMethodInfo, IMemberReference
    {
        private Lazy<ITypeInfo> _returnType;
        private Lazy<ITypeInfo> _declaringType;
        private Lazy<Collection<ParameterDefinition>> _parameters;

        public string Name { get; private set; }
        public CallingConventions CallingConventions { get; private set; }

        public ITypeInfo ReturnType => _returnType.Value;
        public ITypeInfo DeclaringType => _declaringType.Value;
        public Collection<ParameterDefinition> Parameters => _parameters.Value;

        public MethodReference(IMethodReferenceResolver resolver)
        {
            Name = resolver.GetName();
            CallingConventions = resolver.GetCallingConventions();
            _returnType = new Lazy<ITypeInfo>(resolver.GetReturnType);
            _declaringType = new Lazy<ITypeInfo>(resolver.GetDeclaringType);
            _parameters = new Lazy<Collection<ParameterDefinition>>(() => resolver.GetParameters(this));
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitMethodReference(this);
        }

        public override int GetHashCode()
        {
            return MethodInfoComparer.Default.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return (obj is MethodReference) && Equals((MethodReference)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is MethodReference) && Equals((MethodReference)other);
        }

        public bool Equals(MethodReference other)
        {
            return MethodInfoComparer.Default.Equals(this, other);
        }        
    }
}
