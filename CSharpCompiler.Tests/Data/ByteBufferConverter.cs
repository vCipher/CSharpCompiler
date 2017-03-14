using CSharpCompiler.CodeGen;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace CSharpCompiler.Tests.Data
{
    public sealed class ByteBufferConverter<TBuffer> : IDataConverter where TBuffer : ByteBuffer
    {
        private static readonly Regex _pattern = new Regex(@"0[x](?<hex>[0-9a-f]+)", RegexOptions.IgnoreCase);

        public object Convert(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Activator.CreateInstance(typeof(TBuffer));

            byte[] bytes = _pattern.Matches(input)
                .Cast<Match>()
                .Select(m => m.Groups["hex"].Value)
                .Select(str => byte.Parse(str, NumberStyles.HexNumber))
                .ToArray();

            return Activator.CreateInstance(typeof(TBuffer), bytes);
        }
    }
}
