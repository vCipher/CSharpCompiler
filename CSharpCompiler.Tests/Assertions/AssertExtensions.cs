using CSharpCompiler.Lexica.Regexp;

namespace CSharpCompiler.Tests.Assertions
{
    public static class AssertExtensions
    {
        public static ObjectAssersions<T> Should<T>(this T actual)
        {
            return new ObjectAssersions<T>(actual);
        }

        public static TransitionTableAssertions Should(this TransitionTable actual)
        {
            return new TransitionTableAssertions(actual);
        }
    }
}
