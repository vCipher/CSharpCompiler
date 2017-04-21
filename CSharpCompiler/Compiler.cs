using CSharpCompiler.Lexica;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Optimization;
using CSharpCompiler.Syntax;
using CSharpCompiler.Syntax.Ast;

namespace CSharpCompiler
{
    public static class Compiler
    {
        public static AssemblyDefinition CompileAssembly(string content)
        {
            var tokens = Scanner.Scan(content);
            var parseTree = Parser.Parse(tokens);
            var syntaxTree = AstBuilder.Build(parseTree);
            AssemblyDefinition assemblyDef = AssemblyBuilder.Build(syntaxTree);

            return AssemblyOptimizer.Optimize(assemblyDef);
        }

        public static AssemblyDefinition CompileAssembly(string content, CompilationOptions options)
        {
            var tokens = Scanner.Scan(content);
            var parseTree = Parser.Parse(tokens);
            var syntaxTree = AstBuilder.Build(parseTree);
            var assemblyDef = AssemblyBuilder.Build(syntaxTree, options);

            return AssemblyOptimizer.Optimize(assemblyDef);
        }
    }
}
