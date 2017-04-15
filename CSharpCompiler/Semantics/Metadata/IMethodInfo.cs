using System.Collections.ObjectModel;
using CSharpCompiler.Semantics.Metadata;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public interface IMethodInfo : IMetadataEntity, IEquatable<IMethodInfo>
    {
        string Name { get; }
        CallingConventions CallingConventions { get; }
        ITypeInfo DeclaringType { get; }
        ITypeInfo ReturnType { get; }
        Collection<ParameterDefinition> Parameters { get; }
    }
}