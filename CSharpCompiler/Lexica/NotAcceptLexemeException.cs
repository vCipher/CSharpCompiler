namespace CSharpCompiler.Lexica
{
    public class NotAcceptLexemeException : ScanException
    {
        public NotAcceptLexemeException(string lexeme) : base("Scanner doesn't accept lexeme: {0}", lexeme)
        { }
    }
}
