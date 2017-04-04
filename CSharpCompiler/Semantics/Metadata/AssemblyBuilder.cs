using CSharpCompiler.Syntax.Ast;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class AssemblyBuilder
    {
        private SyntaxTree _syntaxTree;
        private CompilationOptions _options;

        private AssemblyBuilder(SyntaxTree syntaxTree, CompilationOptions options)
        {
            _syntaxTree = syntaxTree;
            _options = options;
        }

        public static AssemblyDefinition Build(SyntaxTree syntaxTree)
        {
            return Build(syntaxTree, new CompilationOptions());
        }

        public static AssemblyDefinition Build(SyntaxTree syntaxTree, CompilationOptions options)
        {
            return new AssemblyBuilder(syntaxTree, options).Build();
        }

        private AssemblyDefinition Build()
        {
            ModuleDefinition moduleDef = new ModuleDefinition(_options.Mvid, _options.AssemblyName);
            AssemblyDefinition assemblyDef = new AssemblyDefinition(_options.AssemblyName, moduleDef);

            assemblyDef.References.Add(new AssemblyReference(typeof(object).GetTypeInfo().Assembly.GetName()));
            assemblyDef.CustomAttributes.Add(GetCompilationRelaxationsAttribute(assemblyDef));
            assemblyDef.CustomAttributes.Add(GetRuntimeCompatibilityAttribute(assemblyDef));
            moduleDef.Types.Add(TypeBuilder.Build(_syntaxTree, assemblyDef, _options));

            return assemblyDef;
        }

        private CustomAttribute GetCompilationRelaxationsAttribute(AssemblyDefinition assemblyDef)
        {
            Type type = typeof(CompilationRelaxationsAttribute);
            return new CustomAttribute(type, type.GetConstructor(new Type[] { typeof(int) }), assemblyDef);
        }

        private CustomAttribute GetRuntimeCompatibilityAttribute(AssemblyDefinition assemblyDef)
        {
            Type type = typeof(RuntimeCompatibilityAttribute);
            return new CustomAttribute(type, type.GetConstructor(new Type[0]), assemblyDef);
        }
    }
}
