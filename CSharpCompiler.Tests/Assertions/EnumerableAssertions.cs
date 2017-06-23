using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.Tests.Assertions
{
    public sealed class EnumerableAssertions<T> : ObjectAssertions<IEnumerable<T>>
    {
        public EnumerableAssertions(IEnumerable<T> actual) : base(actual)
        { }

        public override void Be(IEnumerable<T> expected, ITestOutputHelper output)
        {
            Assert.Equal(expected, actual);
        }

        public void Be(IEnumerable<T> expected, ITestOutputHelper output, IEqualityComparer<T> comparer)
        {
            Assert.Equal(expected, actual, comparer);
        }

        public void BeSingle(T expected)
        {
            Assert.Single(actual, expected);
        }

        public void Contains(T expected)
        {
            Assert.Contains(expected, actual);
        }        
    }
}
