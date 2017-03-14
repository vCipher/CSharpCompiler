using CSharpCompiler.Lexica.Regexp;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.Tests.Assertions
{
    public sealed class TransitionTableAssertions : ObjectAssertions<TransitionTable>
    {
        public TransitionTableAssertions(TransitionTable actual) : base(actual)
        { }

        public override void Be(TransitionTable expected, ITestOutputHelper output)
        {
            Assert.Equal(expected, actual, new TransitionTableComparer());
        }
    }
}
