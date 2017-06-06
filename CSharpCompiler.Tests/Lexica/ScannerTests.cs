using CSharpCompiler.Lexica.Regexp;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using Xunit;
using Xunit.Abstractions;

using static CSharpCompiler.Lexica.Tokens.Tokens;

namespace CSharpCompiler.Lexica.Tests
{
    public class ScannerTests : TestCase
    {
        public ScannerTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void ScanTest_DefaultVocabulary()
        {
            string content = "int a = 1;";

            TokenEnumerable tokens = Scanner.Scan(content);

            tokens.Should().Be(new[] {
                INT,
                ID("a"),
                ASSIGN,
                INT_LITERAL("1"),
                SEMICOLON
            });
        }

        [Fact]
        public void ScanTest_CustomVocabulary()
        {
            string content = "int a = 1;";
            string vocabulary =
                @"int INT
                \w+ ID
                = ASSIGN
                \d+ INT_LITERAL
                ; SEMICOLON";

            TransitionTable table = TransitionTable.FromString(vocabulary);
            TokenEnumerable tokens = Scanner.Scan(content, table);

            tokens.Should().Be(new[] {
                INT,
                ID("a"),
                ASSIGN,
                INT_LITERAL("1"),
                SEMICOLON
            });
        }

        [Fact]
        public void ScanTest_NotAcceptInputString()
        {
            string content = "#";
            string vocabulary = "\\w ID";

            TransitionTable table = TransitionTable.FromString(vocabulary);
            Assert.Throws<NotAcceptLexemeException>(() => Scanner.Scan(content, table));
        }

        [Fact]
        public void ScanTest_StringLiteral()
        {
            string content = "\"hello, world!\"";
            string vocabulary = "\"\\.*\" STRING_LITERAL";

            TransitionTable table = TransitionTable.FromString(vocabulary);
            TokenEnumerable tokens = Scanner.Scan(content, table);

            tokens.Should().Single(STRING_LITERAL("hello, world!"));
        }
    }
}