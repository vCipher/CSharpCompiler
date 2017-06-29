using CSharpCompiler.Semantics.Resolvers;
using System.Collections.ObjectModel;
using System.Threading;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodDefinition : IMethodInfo, ICustomAttributeProvider
    {
        private object _syncRoot = new object();
        private IMethodDefinitionResolver _resolver;

        private bool _callingConventionsInited;
        private CallingConventions _callingConventions;

        private bool _attributesInited;
        private MethodAttributes _attributes;

        private bool _implAttributesInited;
        private MethodImplAttributes _implAttributes;

        private string _name;
        private MethodBody _body;
        private ITypeInfo _returnType;
        private ITypeInfo _declaringType;
        private Collection<ParameterDefinition> _parameters;
        private Collection<CustomAttribute> _customAttributes;

        public CallingConventions CallingConventions => LazyInitializer.EnsureInitialized(ref _callingConventions, ref _callingConventionsInited, ref _syncRoot, _resolver.GetCallingConventions);
        public MethodAttributes Attributes => LazyInitializer.EnsureInitialized(ref _attributes, ref _attributesInited, ref _syncRoot, _resolver.GetAttributes);
        public MethodImplAttributes ImplAttributes => LazyInitializer.EnsureInitialized(ref _implAttributes, ref _implAttributesInited, ref _syncRoot, _resolver.GetImplAttributes);

        public string Name => LazyInitializer.EnsureInitialized(ref _name, _resolver.GetName);
        public MethodBody Body => LazyInitializer.EnsureInitialized(ref _body, _resolver.GetMethodBody);
        public ITypeInfo ReturnType => LazyInitializer.EnsureInitialized(ref _returnType, _resolver.GetReturnType);
        public ITypeInfo DeclaringType => LazyInitializer.EnsureInitialized(ref _declaringType, _resolver.GetDeclaringType);
        public Collection<ParameterDefinition> Parameters => LazyInitializer.EnsureInitialized(ref _parameters, () => _resolver.GetParameters(this));
        public Collection<CustomAttribute> CustomAttributes => LazyInitializer.EnsureInitialized(ref _customAttributes, () => _resolver.GetCustomAttributes(this));

        public bool HasThis
        {
            get { return (Attributes & MethodAttributes.Static) == 0; }
        }

        public MethodDefinition(IMethodDefinitionResolver resolver)
        {
            _resolver = resolver;
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
