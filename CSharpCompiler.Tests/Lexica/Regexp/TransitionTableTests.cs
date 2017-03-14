using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using System.Collections.Generic;
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
                transitions: new Dictionary<int, Dictionary<char, int>> {
                    [0] = new Dictionary<char, int> { ['a'] = 1 },
                    [1] = new Dictionary<char, int> { ['b'] = 2 },
                    [2] = new Dictionary<char, int> { ['c'] = 3 },
                    [3] = new Dictionary<char, int> { }
                },
                aliases: new Dictionary<int, string> {
                    [3] = "ID"
                }
            );
            var actual = TransitionTable.FromFile("Content/vocabulary.txt");

            actual.Should().Be(expected);
        }

        [Fact]
        public void FromStringTest()
        {
            var expected = new TransitionTable(
                head: 0,
                transitions: new Dictionary<int, Dictionary<char, int>>
                {
                    [0] = new Dictionary<char, int> { ['a'] = 1, ['b'] = 2 },
                    [1] = new Dictionary<char, int> { },
                    [2] = new Dictionary<char, int> { }
                },
                aliases: new Dictionary<int, string>
                {
                    [1] = "ID",
                    [2] = "ID"
                }
            );
            var actual = TransitionTable.FromString(@"a|b ID");

            actual.Should().Be(expected);
        }
    }
}