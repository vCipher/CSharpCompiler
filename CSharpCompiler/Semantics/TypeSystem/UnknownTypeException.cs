using System;
using System.Runtime.Serialization;

namespace CSharpCompiler.Semantics.TypeSystem
{
    [Serializable]
    public sealed class UnknownTypeException : Exception
    {
        public UnknownTypeException(string typeName) : base(string.Format("Unknown type: {0}", typeName))
        { }
    }
}