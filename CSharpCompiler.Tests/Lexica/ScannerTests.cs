using CSharpCompiler.Lexica.Regexp;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Tests;
using System.Collections.Generic;
using Xunit;

using static CSharpCompiler.Lexica.Tokens.Tokens;
using Xunit.Abstractions;

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
            List<Token> tokens = Scanner.Scan(content);

            Assert.Equal(new[] {
                INT,
                ID("a"),
                ASSIGN,
                INT_CONST("1"),
                SEMICOLON
            }, tokens);
        }

        [Fact]
        public void ScanTest_CustomVocabulary()
        {
            string content = "int a = 1;";
            string vocabulary =
                @"int INT
                \w+ ID
                = ASSIGN
                \d+ INT_CONST
                ; SEMICOLON";

            TransitionTable table = TransitionTable.FromString(vocabulary);
            List<Token> tokens = Scanner.Scan(content, table);
                        
            Assert.Equal(new[] {
                INT,
                ID("a"),
                ASSIGN,
                INT_CONST("1"),
                SEMICOLON
            }, tokens);
        }

        [Fact]
        public void ScanTest_NotAcceptInputString()
        {
            string content = "1";
            string vocabulary = "\\w ID";

            TransitionTable table = TransitionTable.FromString(vocabulary);
            Assert.Throws<NotAcceptLexemeException>(() => Scanner.Scan(content, table));
        }
    }
}