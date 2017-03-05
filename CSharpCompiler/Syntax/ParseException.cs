using System;

namespace CSharpCompiler.Syntax
{
    [Serializable]
    public class ParseException : Exception
    {
        public ParseException(string message) : base(message) { }
    }
}
