using CSharpCompiler.Semantics.Builders;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax.Ast;
using System;

namespace CSharpCompiler.Compilation
{
    public sealed class CompilationContext
    {
        private Lazy<AssemblyDefinition> _assembly;
        private Lazy<ModuleDefinition> _module;
        private Lazy<TypeDefinition> _typeDef;

        public SyntaxTree SyntaxTree { get; private set; }
        public CompilationOptions Options { get; private set; }

        public AssemblyDefinition Assembly => _assembly.Value;
        public ModuleDefinition Module => _module.Value;
        public TypeDefinition TypeDefinition => _typeDef.Value;

        public CompilationContext(SyntaxTree syntaxTree, CompilationOptions options)
        {
            SyntaxTree = syntaxTree;
            Options = options;
            _assembly = new Lazy<AssemblyDefinition>(() => AssemblyBuilder.Build(this));
            _module = new Lazy<ModuleDefinition>(() => ModuleBuilder.Build(this));
            _typeDef = new Lazy<TypeDefinition>(() => TypeBuilder.Build(this));
        }
    }
}
