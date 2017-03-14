using System;

namespace CSharpCompiler.CodeGen.Metadata
{
    [Serializable]
    public class UndefinedCodedTokenSchemaException : Exception
    {
        public UndefinedCodedTokenSchemaException() { }
        public UndefinedCodedTokenSchemaException(string message) : base(message) { }
        public UndefinedCodedTokenSchemaException(string message, Exception inner) : base(message, inner) { }
        public UndefinedCodedTokenSchemaException(string format, params object[] args) : base(string.Format(format, args)) { }
        protected UndefinedCodedTokenSchemaException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
