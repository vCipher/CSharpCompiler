using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax.Ast.Types
{
    public sealed class PrimitiveType : AstType
    {
        public Token TypeToken { get; private set; }

        public PrimitiveType(Token typeToken)
        {
            TypeToken = typeToken;
        }

        public override IType ToType()
        {
            var typeCode = TypeToken.Tag.GetKnownTypeCode();
            if (typeCode == KnownTypeCode.None)
                throw new UnknownTypeException(TypeToken.ToString());

            return new KnownType(typeCode);
        }
    }
}
