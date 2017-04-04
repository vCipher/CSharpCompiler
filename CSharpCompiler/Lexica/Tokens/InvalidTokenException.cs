namespace CSharpCompiler.Lexica.Tokens
{
    public class InvalidTokenException : ScanException
    {
        public InvalidTokenException(string alias) : base("Unsupported token {0}", alias)
        { }
    }
}
