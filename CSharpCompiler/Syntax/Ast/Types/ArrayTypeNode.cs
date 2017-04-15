using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Types
{
    public sealed class ArrayTypeNode : TypeNode
    {
        public TypeNode ContainedType { get; private set; }
        public int Rank { get; private set; }

        public ArrayTypeNode(TypeNode containedType, int rank)
        {
            ContainedType = containedType;
            Rank = rank;
        }

        public override ITypeInfo ToType()
        {
            return new ArrayType(ContainedType.ToType(), Rank);
        }
    }
}
