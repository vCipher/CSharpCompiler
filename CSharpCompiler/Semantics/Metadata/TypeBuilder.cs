using CSharpCompiler.Semantic.Cil;
using CSharpCompiler.Syntax.Ast;
using CSharpCompiler.Syntax.Ast.Statements;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class TypeBuilder
    {
        private SyntaxTree _syntaxTree;
        private AssemblyDefinition _assemblyDef;
        private CompilationOptions _options;

        public TypeBuilder(SyntaxTree syntaxTree, AssemblyDefinition assemblyDef, CompilationOptions options)
        {
            _syntaxTree = syntaxTree;
            _assemblyDef = assemblyDef;
            _options = options;
        }

        public static TypeDefinition Build(SyntaxTree syntaxTree, AssemblyDefinition assemblyDef)
        {
            return new TypeBuilder(syntaxTree, assemblyDef, new CompilationOptions()).TypeDefinition();
        }

        public static TypeDefinition Build(SyntaxTree syntaxTree, AssemblyDefinition assemblyDef, CompilationOptions options)
        {
            return new TypeBuilder(syntaxTree, assemblyDef, options).TypeDefinition();
        }

        public TypeDefinition TypeDefinition()
        {
            TypeAttributes attributes = TypeAttributes.Public |
                TypeAttributes.AutoLayout |
                TypeAttributes.Class |
                TypeAttributes.AnsiClass |
                TypeAttributes.BeforeFieldInit;

            TypeDefinition typeDef = new TypeDefinition(
                _options.TypeName, _options.Namespace, attributes, _assemblyDef);

            _assemblyDef.EntryPoint = MethodDefinition(_syntaxTree.Statements, typeDef);
            typeDef.Methods.Add(_assemblyDef.EntryPoint);
            typeDef.Methods.Add(CtorDefinition(typeDef));

            return typeDef;
        }

        private MethodDefinition MethodDefinition(List<Stmt> statements, TypeDefinition typeDef)
        {
            MethodAttributes attributes = MethodAttributes.Public |
                MethodAttributes.Static |
                MethodAttributes.HideBySig |
                MethodAttributes.ReuseSlot;

            MethodDefinition methodDef = new MethodDefinition("Main", attributes, typeDef);
            MethodBuilder builder = methodDef.Body.GetBuilder();
            
            foreach (var stmt in statements)
            {
                stmt.Build(builder);
            }
            builder.Emit(OpCodes.Ret);

            return methodDef;
        }

        private MethodDefinition CtorDefinition(TypeDefinition typeDef)
        {
            MethodAttributes attributes = MethodAttributes.Public |
                MethodAttributes.HideBySig |
                MethodAttributes.ReuseSlot |
                MethodAttributes.SpecialName |
                MethodAttributes.RTSpecialName;

            MethodDefinition methodDef = new MethodDefinition(".ctor", attributes, typeDef);
            MethodBuilder builder = methodDef.Body.GetBuilder();
            builder.Emit(OpCodes.Ldarg_0);
            builder.Emit(OpCodes.Call, new MethodReference(typeof(object).GetConstructor(new Type[0])));
            builder.Emit(OpCodes.Ret);

            return methodDef;
        }
    }
}
