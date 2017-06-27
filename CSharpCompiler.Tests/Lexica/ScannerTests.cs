using CSharpCompiler.Lexica.Regexp;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using CSharpCompiler.Utility;
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

            var tokens = Scanner.Scan(content);

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
                ; SEMICOLON
                \s WHITE_SPACE";

            var table = TransitionTable.FromVocabularyString(vocabulary);
            var tokens = Scanner.Scan(content, new ScannerOptions { Transitions = table });

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

            var table = TransitionTable.FromVocabularyString(vocabulary);
            Assert.Throws<NotAcceptLexemeException>(() => Scanner.Scan(content, new ScannerOptions { Transitions = table }));
        }

        [Fact]
        public void ScanTest_StringLiteral()
        {
            string content = "\"hello, world!\"";
            string vocabulary = "\"\\.*\" STRING_LITERAL";

            var table = TransitionTable.FromVocabularyString(vocabulary);
            var tokens = Scanner.Scan(content, new ScannerOptions { Transitions = table });

            tokens.Should().BeSingle(STRING_LITERAL("hello, world!"));
        }

        [Fact]
        public void ScanTest_LineComment()
        {
            string content = 
                @"
// first line
// second line";

            string vocabulary = @"
                \s WHITE_SPACE
                \$ NEW_LINE
                //\.*\$? LINE_COMMENT";

            var table = TransitionTable.FromVocabularyString(vocabulary);
            var options = new ScannerOptions { Transitions = table, BlackList = Empty<TokenTag>.Array };
            var tokens = Scanner.Scan(content, options);

            tokens.Should().Be(new[]{
                NEW_LINE,
                LINE_COMMENT("// first line\r\n"),
                LINE_COMMENT("// second line")
            });
        }

        [Fact]
        public void ScanTest_MultiLineComment()
        {
            string content =
                @"/* comment */";

            string vocabulary = @"
                \s WHITE_SPACE
                \$ NEW_LINE
                /\*(\.|\$)*\*/ MULTI_LINE_COMMENT";

            var table = TransitionTable.FromVocabularyString(vocabulary);
            var options = new ScannerOptions { Transitions = table, BlackList = Empty<TokenTag>.Array };
            var tokens = Scanner.Scan(content, options);

            tokens.Should().Be(new[]{
                MULTI_LINE_COMMENT("/* comment */"),
            });
        }

        [Fact]
        public void ScanTest_InjectedComments()
        {
            string content = "int a = /* comments */1;";

            var tokens = Scanner.Scan(content);

            tokens.Should().Be(new[] {
                INT,
                ID("a"),
                ASSIGN,
                INT_LITERAL("1"),
                SEMICOLON
            });
        }

        [Fact]
        public void ScanTest_AppendComments()
        {
            string content = "int a = 1; // comments";

            var tokens = Scanner.Scan(content);

            tokens.Should().Be(new[] {
                INT,
                ID("a"),
                ASSIGN,
                INT_LITERAL("1"),
                SEMICOLON
            });
        }
    }
}