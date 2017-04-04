namespace CSharpCompiler.Semantics.TypeSystem
{
    public sealed class UnknownTypeException : SemanticException
    {
        public UnknownTypeException(string typeName) : base("Unknown type: {0}", typeName)
        { }
    }
}