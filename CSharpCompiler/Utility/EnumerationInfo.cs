namespace CSharpCompiler.Utility
{
    public sealed class EnumerationInfo
    {
        public int Index { get; private set; }
        public bool IsFirst { get; private set; }
        public bool IsLast { get; private set; }

        public EnumerationInfo(int index, bool isFirst, bool isLast)
        {
            Index = index;
            IsFirst = isFirst;
            IsLast = isLast;
        }
    }
}
