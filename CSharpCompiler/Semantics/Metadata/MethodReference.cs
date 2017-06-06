using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Utility;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodReference : IMethodInfo
    {
        public string Name { get; private set; }
        public ITypeInfo ReturnType { get; private set; }
        public ITypeInfo DeclaringType { get; private set; }
        public CallingConventions CallingConventions { get; private set; }
        public Collection<ParameterDefinition> Parameters { get; private set; }
        
        public MethodReference(System.Reflection.ConstructorInfo ctorInfo)
        {
            Name = ctorInfo.Name;
            ReturnType = KnownType.Void;
            DeclaringType = TypeFactory.Create(ctorInfo.DeclaringType);
            CallingConventions = GetCallingConventions(ctorInfo);
            Parameters = ctorInfo.GetParameters()
                .Select(param => new ParameterDefinition(param.Name, TypeFactory.Create(param.ParameterType), this))
                .ToCollection();
        }

        public MethodReference(System.Reflection.MethodInfo methodInfo)
        {
            Name = methodInfo.Name;
            ReturnType = TypeFactory.Create(methodInfo.ReturnType);
            DeclaringType = TypeFactory.Create(methodInfo.DeclaringType);
            CallingConventions = GetCallingConventions(methodInfo);
            Parameters = methodInfo.GetParameters()
                .Select(param => new ParameterDefinition(param.Name, TypeFactory.Create(param.ParameterType), this))
                .ToCollection();
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
            if (!(obj is MethodReference)) return false;
            return Equals((MethodReference)obj);
        }

        public bool Equals(IMethodInfo other)
        {
            if (!(other is MethodReference)) return false;
            return Equals((MethodReference)other);
        }

        public bool Equals(MethodReference other)
        {
            return MethodInfoComparer.Default.Equals(this, other);
        }

        private CallingConventions GetCallingConventions(System.Reflection.MethodBase methodBase)
        {
            CallingConventions conventions = CallingConventions.Default;

            if (methodBase.CallingConvention.HasFlag(System.Reflection.CallingConventions.HasThis))
                conventions |= CallingConventions.HasThis;

            if (methodBase.CallingConvention.HasFlag(System.Reflection.CallingConventions.ExplicitThis))
                conventions |= CallingConventions.ExplicitThis;

            if (methodBase.CallingConvention.HasFlag(System.Reflection.CallingConventions.VarArgs))
                conventions |= CallingConventions.VarArg;

            return conventions;
        }        
    }
}
