using CSharpCompiler.Syntax.Ast;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public sealed class TypeInferenceContext
    {
        public VarScope Variables { get; set; }
    }
}
