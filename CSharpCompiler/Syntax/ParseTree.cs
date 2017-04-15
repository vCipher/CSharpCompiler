namespace CSharpCompiler.Syntax
{
    public sealed class ParseTree : ParseNode
    {
        public ParseTree(params ParseNode[] children) : base(ParseNodeTag.Program, children)
        { }
    }
}
