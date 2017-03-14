using System;

namespace CSharpCompiler.Tests.Data
{
    public sealed class GuidConverter : IDataConverter
    {
        public object Convert(string input)
        {
            return Guid.Parse(input);
        }
    }
}
