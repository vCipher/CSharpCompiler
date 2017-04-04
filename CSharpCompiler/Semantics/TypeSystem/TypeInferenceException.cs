namespace CSharpCompiler.Semantics.TypeSystem
{
    public class TypeInferenceException : SemanticException
    {
        public TypeInferenceException(string format, params object[] args) : base(format, args)
        { }
    }
}
