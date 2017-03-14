namespace CSharpCompiler.Tests.Data
{
    public sealed class StringConverter : IDataConverter
    {
        public object Convert(string input)
        {
            return input;
        }
    }
}
