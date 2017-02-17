using Xunit;
using System.Collections.Generic;
using System.IO;
using CSharpCompiler.Scanners.Regexp;
using CSharpCompiler.Scanners.Tokens;

using static CSharpCompiler.Scanners.Tokens.CSharpTokens;

namespace CSharpCompiler.Scanners.Tests
{
    public class ScannerTests
    {
        [Fact]
        public void ScanTest()
        {
            string content = "int a = 1;";
            string vocabulary =
@"int INT
\w+ ID
= ASSIGN
\d+ INT_CONST
; SEMICOLON";

            var table = TransitionTable.FromString(vocabulary);
            var scanner = new Scanner(table);
            var tokens = scanner.Scan(content);
                        
            Assert.Equal(new[] {
                INT,
                ID("a"),
                ASSIGN,
                INT_CONST("1"),
                SEMICOLON
            }, tokens);
        }

        [Fact]
        public void ScanTest_VocabularyFile()
        {
            var content = "int a = 1;";
            var table = TransitionTable.FromFile("vocabulary.txt");
            var scanner = new Scanner(table);
            var tokens = scanner.Scan(content);

            Assert.Equal(new[] {
                INT,
                ID("a"),
                ASSIGN,
                INT_CONST("1"),
                SEMICOLON
            }, tokens);
        }

        [Fact]
        public void ScanTest_NotAccept()
        {
            var content = "1";
            var table = TransitionTable.FromString("\\w ID");
            var scanner = new Scanner(table);

            Assert.Throws<ScanException>(() => scanner.Scan(content));
        }
    }
}