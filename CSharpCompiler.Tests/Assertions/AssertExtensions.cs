using CSharpCompiler.Lexica.Regexp;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.CodeGen;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax;
using CSharpCompiler.Syntax.Ast;
using System.IO;

namespace CSharpCompiler.Tests.Assertions
{
    public static class AssertExtensions
    {
        public static EnumerableAssertions<Token> Should(this TokenEnumerable actual)
        {
            return new EnumerableAssertions<Token>(actual);
        }

        public static TransitionTableAssertions Should(this TransitionTable actual)
        {
            return new TransitionTableAssertions(actual);
        }

        public static StreamAssertions Should(this Stream actual)
        {
            return new StreamAssertions(actual);
        }

        public static ByteBufferAssertions Should(this ByteBuffer actual)
        {
            return new ByteBufferAssertions(actual);
        }

        public static ObjectAssertions<Nfa> Should(this Nfa actual)
        {
            return new ObjectAssertions<Nfa>(actual);
        }

        public static ObjectAssertions<TypeDefinition> Should(this TypeDefinition actual)
        {
            return new ObjectAssertions<TypeDefinition>(actual);
        }

        public static ObjectAssertions<AstNode> Should(this AstNode actual)
        {
            return new ObjectAssertions<AstNode>(actual);
        }

        public static ObjectAssertions<ParseNode> Should(this ParseNode actual)
        {
            return new ObjectAssertions<ParseNode>(actual);
        }
    }
}
