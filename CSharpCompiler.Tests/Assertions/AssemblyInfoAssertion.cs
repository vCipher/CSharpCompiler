using CSharpCompiler.Semantics.Metadata;
using Xunit.Sdk;

namespace CSharpCompiler.Tests.Assertions
{
    public sealed class AssemblyInfoAssertion : ObjectAssertions<IAssemblyInfo>
    {
        public AssemblyInfoAssertion(IAssemblyInfo actual) : base(actual)
        { }

        public override void Be(IAssemblyInfo expected, Xunit.Abstractions.ITestOutputHelper output)
        {
            if (!AssemblyInfoComparer.Default.Equals(expected, actual))
            {
                output?.WriteLine("Expected:\n {0}\n", expected);
                output?.WriteLine("Actual:\n {0}\n", actual);
                throw new EqualException(expected, actual);
            }
        }
    }
}
