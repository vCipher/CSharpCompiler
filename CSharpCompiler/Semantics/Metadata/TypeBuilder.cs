using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.TypeSystem;
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
            var attributes = TypeAttributes.Public |
                TypeAttributes.AutoLayout |
                TypeAttributes.Class |
                TypeAttributes.AnsiClass |
                TypeAttributes.BeforeFieldInit;

            var typeDef = new TypeDefinition(
                _options.TypeName, 
                _options.Namespace, 
                attributes, 
                KnownType.Object, 
                _assemblyDef);

            _assemblyDef.EntryPoint = MethodDefinition(_syntaxTree, typeDef);
            typeDef.Methods.Add(_assemblyDef.EntryPoint);
            typeDef.Methods.Add(CtorDefinition(typeDef));

            return typeDef;
        }

        private MethodDefinition MethodDefinition(SyntaxTree syntaxTree, TypeDefinition typeDef)
        {
            var attributes = MethodAttributes.Public |
                MethodAttributes.Static |
                MethodAttributes.HideBySig |
                MethodAttributes.ReuseSlot;

            var methodDef = new MethodDefinition("Main", attributes, typeDef);
            var builder = new MethodBuilder(methodDef.Body);
            syntaxTree.Accept(builder);

            return methodDef;
        }

        private MethodDefinition CtorDefinition(TypeDefinition typeDef)
        {
            var attributes = MethodAttributes.Public |
                MethodAttributes.HideBySig |
                MethodAttributes.ReuseSlot |
                MethodAttributes.SpecialName |
                MethodAttributes.RTSpecialName;

            var methodDef = new MethodDefinition(".ctor", attributes, typeDef);
            var emitter = new OpCodeEmitter(methodDef.Body);
            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Call, new MethodReference(typeof(object).GetConstructor(new Type[0])));
            emitter.Emit(OpCodes.Ret);

            return methodDef;
        }
    }
}
