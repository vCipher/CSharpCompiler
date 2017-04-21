using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Contains(T expected)
        {
            Assert.Contains(expected, actual);
        }

        public void Single(T expected)
        {
            Assert.Single(actual, expected);
        }
    }
}
