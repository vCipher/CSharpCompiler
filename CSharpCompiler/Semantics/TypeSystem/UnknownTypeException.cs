using System;
using System.Runtime.Serialization;

namespace CSharpCompiler.Semantics.TypeSystem
{
    [Serializable]
    public sealed class UnknownTypeException : SemanticException
    {
        public UnknownTypeException(string typeName) : base("Unknown type: {0}", typeName) { }
    }
}