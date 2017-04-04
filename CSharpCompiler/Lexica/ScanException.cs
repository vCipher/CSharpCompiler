using System;

namespace CSharpCompiler.Lexica
{
    public class ScanException : Exception
    {
        public ScanException() { }
        public ScanException(string message) : base(message) { }
        public ScanException(string format, params object[] args) : base(string.Format(format, args)) { }
    }
}
