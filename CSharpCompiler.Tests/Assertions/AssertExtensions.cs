using CSharpCompiler.Lexica.Regexp;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System.Collections.Generic;
using System.IO;

namespace CSharpCompiler.Tests.Assertions
{
    public static class AssertExtensions
    {
        public static ObjectAssertions<object> Should(this object actual) => new ObjectAssertions<object>(actual);
        public static EnumerableAssertions<T> Should<T>(this IEnumerable<T> actual) => new EnumerableAssertions<T>(actual);
        public static EnumerableAssertions<Token> Should(this TokenEnumerable actual) => new EnumerableAssertions<Token>(actual);
        public static TransitionTableAssertions Should(this TransitionTable actual) => new TransitionTableAssertions(actual);
        public static StreamAssertions Should(this Stream actual) => new StreamAssertions(actual);
        public static ByteBufferAssertions Should(this ByteBuffer actual) => new ByteBufferAssertions(actual);
        public static AssemblyInfoAssertion Should(this IAssemblyInfo actual) => new AssemblyInfoAssertion(actual);
    }
}
