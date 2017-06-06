using Xunit.Abstractions;

namespace CSharpCompiler.Tests
{
    public class TestCase
    {
        public ITestOutputHelper Output { get; private set; }

        public TestCase(ITestOutputHelper output)
        {
            Output = output;
        }
    }
}
