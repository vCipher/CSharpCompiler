using System.Collections.Generic;
using Xunit;

namespace CSharpCompiler.Lexica.Regexp.Tests
{
    public class TransitionTableTests
    {
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
            var actual = TransitionTable.FromFile("vocabulary.txt");

            Assert.Equal(expected, actual);
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

            Assert.Equal(expected, actual);
        }
    }
}