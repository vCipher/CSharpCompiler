using CSharpCompiler.Lexica.Tokens;
using System;

namespace CSharpCompiler.Semantics.TypeSystem
{
    [Serializable]
    public class TypeInferenceException : SemanticException
    {
        public TypeInferenceException(string format, params object[] args) : base(format, args)
        { }
    }
}
