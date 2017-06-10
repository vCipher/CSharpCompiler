using CSharpCompiler.Semantics.Resolvers;
using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodDefinition : IMethodInfo, ICustomAttributeProvider
    {
        private Lazy<MethodBody> _body;
        private Lazy<ITypeInfo> _returnType;
        private Lazy<ITypeInfo> _declaringType;
        private Lazy<CallingConventions> _callingConventions;
        private Lazy<Collection<ParameterDefinition>> _parameters;
        private Lazy<Collection<CustomAttribute>> _customAttributes;

        public string Name { get; private set; }
        public MethodAttributes Attributes { get; private set; }
        public MethodImplAttributes ImplAttributes { get; private set; }
        
        public MethodBody Body => _body.Value;
        public ITypeInfo ReturnType => _returnType.Value;
        public ITypeInfo DeclaringType => _declaringType.Value;
        public CallingConventions CallingConventions => _callingConventions.Value;
        public Collection<ParameterDefinition> Parameters => _parameters.Value;
        public Collection<CustomAttribute> CustomAttributes => _customAttributes.Value;

        public bool HasThis
        {
            get { return (Attributes & MethodAttributes.Static) == 0; }
        }

        public MethodDefinition(IMethodDefinitionResolver resolver)
        {
            Name = resolver.GetName();
            ImplAttributes = resolver.GetImplAttributes();
            Attributes = resolver.GetAttributes();
            _body = new Lazy<MethodBody>(resolver.GetMethodBody);
            _returnType = new Lazy<ITypeInfo>(resolver.GetReturnType);
            _declaringType = new Lazy<ITypeInfo>(resolver.GetDeclaringType);
            _callingConventions = new Lazy<CallingConventions>(resolver.GetCallingConventions);
            _parameters = new Lazy<Collection<ParameterDefinition>>(() => resolver.GetParameters(this));
            _customAttributes = new Lazy<Collection<CustomAttribute>>(() => resolver.GetCustomAttributes(this));
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitMethodDefinition(this);
        }

        public override int GetHashCode()
        {
            return MethodInfoComparer.Default.GetHashCode(this);
        }

        public override bool  Equals(object obj)
        {
            return (obj is MethodDefinition) && Equals((MethodDefinition)obj);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is MethodDefinition) && Equals((MethodDefinition)other);
        }

        public bool Equals(MethodDefinition other)
        {
            return MethodInfoComparer.Default.Equals(this, other);
        }
    }
}
