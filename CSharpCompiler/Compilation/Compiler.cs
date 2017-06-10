using CSharpCompiler.Lexica;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Optimization;
using CSharpCompiler.Syntax;
using CSharpCompiler.Syntax.Ast;

namespace CSharpCompiler.Compilation
{
    public static class Compiler
    {
        public static AssemblyDefinition CompileAssembly(string content)
        {
            var tokens = Scanner.Scan(content);
            var parseTree = Parser.Parse(tokens);
            var syntaxTree = AstBuilder.Build(parseTree);
            var context = new CompilationContext(syntaxTree, new CompilationOptions());

            return AssemblyOptimizer.Optimize(context.Assembly);
        }

        public static AssemblyDefinition CompileAssembly(string content, CompilationOptions options)
        {
            var tokens = Scanner.Scan(content);
            var parseTree = Parser.Parse(tokens);
            var syntaxTree = AstBuilder.Build(parseTree);
            var context = new CompilationContext(syntaxTree, options);

            return AssemblyOptimizer.Optimize(context.Assembly);
        }
    }
}
