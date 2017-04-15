using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Types
{
    public sealed class PrimitiveTypeNode : TypeNode
    {
        public Token TypeToken { get; private set; }

        public PrimitiveTypeNode(Token typeToken)
        {
            TypeToken = typeToken;
        }

        public override ITypeInfo ToType()
        {
            return KnownType.Get(TypeToken.Tag.GetKnownTypeCode());
        }
    }
}
