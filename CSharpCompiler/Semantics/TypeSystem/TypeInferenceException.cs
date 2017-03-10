using CSharpCompiler.Lexica.Tokens;
using System;

namespace CSharpCompiler.Semantics.TypeSystem
{
    [Serializable]
    public class TypeInferenceException : Exception
    {
        public TypeInferenceException(Token literalValue) : base(GetMessage(literalValue)) { }

        public TypeInferenceException(string message) : base(message) { }

        private static string GetMessage(Token literalValue)
        {
            return string.Format("Can't inference type from literal: {0}", literalValue);
        }
    }
}
