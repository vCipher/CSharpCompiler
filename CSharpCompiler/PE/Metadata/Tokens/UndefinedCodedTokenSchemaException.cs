using System;

namespace CSharpCompiler.PE.Metadata.Tokens
{
    public sealed class UndefinedCodedTokenSchemaException : Exception
    {
        public UndefinedCodedTokenSchemaException() { }
        public UndefinedCodedTokenSchemaException(string message) : base(message) { }
        public UndefinedCodedTokenSchemaException(string message, Exception inner) : base(message, inner) { }
        public UndefinedCodedTokenSchemaException(string format, params object[] args) : base(string.Format(format, args)) { }
    }
}
