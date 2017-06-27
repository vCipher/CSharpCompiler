using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.Lexica.Regexp.Tests
{
    public class TransitionTableTests : TestCase
    {
        public TransitionTableTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void FromFileTest()
        {
            var expected = new TransitionTable(
                head: 0,
                transitions: new Dictionary<char, ushort>[] 
                {
                    new Dictionary<char, ushort> { ['a'] = 1 },
                    new Dictionary<char, ushort> { ['b'] = 2 },
                    new Dictionary<char, ushort> { ['c'] = 3 },
                    new Dictionary<char, ushort> { }
                },
                aliases: new Dictionary<ushort, string>
                {
                    [3] = "ID"
                }
            );
            var actual = TransitionTable.FromVocabularyFile(Path.Combine(AppContext.BaseDirectory, "Content/vocabulary.txt"));

            actual.Should().Be(expected);
        }

        [Fact]
        public void FromStringTest()
        {
            var expected = new TransitionTable(
                head: 0,
                transitions: new Dictionary<char, ushort>[]
                {
                    new Dictionary<char, ushort> { ['a'] = 1, ['b'] = 2 },
                    new Dictionary<char, ushort> { },
                    new Dictionary<char, ushort> { }
                },
                aliases: new Dictionary<ushort, string>
                {
                    [1] = "ID",
                    [2] = "ID"
                }
            );
            var actual = TransitionTable.FromVocabularyString(@"a|b ID");

            actual.Should().Be(expected);
        }
    }
}