using System;

namespace CSharpCompiler.Semantics
{
    [Serializable]
    public class SemanticException : Exception
    {
        public SemanticException() { }
        public SemanticException(string message) : base(message) { }
        public SemanticException(string message, Exception inner) : base(message, inner) { }
        protected SemanticException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
