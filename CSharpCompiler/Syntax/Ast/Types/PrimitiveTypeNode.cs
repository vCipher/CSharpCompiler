using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Types
{
    public sealed class PrimitiveTypeNode : TypeNode
    {
        public Token TypeToken { get; private set; }

        public PrimitiveTypeNode(Token typeToken)
        {
            TypeToken = typeToken;
        }

        public override void Accept(ITypeVisitor visitor)
        {
            visitor.VisitPrimitiveType(this);
        }
    }
}
