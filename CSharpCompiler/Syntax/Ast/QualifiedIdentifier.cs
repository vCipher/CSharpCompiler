using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast
{
    public sealed class QualifiedIdentifier : AstNode
    {
        public IList<string> Parts { get; private set; }

        public QualifiedIdentifier(IEnumerable<string> parts)
        {
            Parts = new List<string>(parts);
        }

        public QualifiedIdentifier(params string[] parts)
        {
            Parts = parts;
        }

        public override string ToString()
        {
            return string.Join(".", Parts);
        }
    }
}
