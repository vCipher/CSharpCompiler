using CSharpCompiler.Compilation;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Syntax.Ast;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Semantics.Builders
{
    public sealed class TypeBuilder : ITypeDefinitionResolver
    {
        private CompilationContext _context;

        private TypeBuilder(CompilationContext context)
        {
            _context = context;
        }

        public static TypeDefinition Build(CompilationContext context)
        {
            var builder = new TypeBuilder(context);
            return new TypeDefinition(builder);
        }

        public string GetName()
        {
            return _context.Options.TypeName;
        }

        public string GetNamespace()
        {
            return _context.Options.Namespace;
        }        

        public ElementType GetElementType(ITypeInfo type)
        {
            return ElementType.Class;
        }

        public TypeAttributes GetAttributes()
        {
            return TypeAttributes.Public |
                TypeAttributes.AutoLayout |
                TypeAttributes.Class |
                TypeAttributes.AnsiClass |
                TypeAttributes.BeforeFieldInit;
        }

        public ITypeInfo GetBaseType()
        {
            return KnownType.Object;
        }

        public IAssemblyInfo GetAssembly()
        {
            return _context.Assembly;
        }

        public Collection<MethodDefinition> GetMethods(TypeDefinition typeDef)
        {
            return new Collection<MethodDefinition>
            {
                MethodDefinition(_context.SyntaxTree, typeDef),
                CtorDefinition(typeDef)
            };
        }

        public Collection<FieldDefinition> GetFields(TypeDefinition typeDef)
        {
            return new Collection<FieldDefinition>();
        }

        public Collection<CustomAttribute> GetCustomAttributes(TypeDefinition typeDef)
        {
            return new Collection<CustomAttribute>();
        }

        private MethodDefinition MethodDefinition(SyntaxTree syntaxTree, TypeDefinition typeDef)
        {
            var usings = new List<List<string>>();
            var context = new SemanticContext(usings, GetTypeRegistry(), typeDef);
            return MethodBuilder.Build(syntaxTree, typeDef, context);
        }

        private MethodDefinition CtorDefinition(TypeDefinition typeDef)
        {
            return ConstructorBuilder.Build(typeDef);
        }

        private TypeRegistry GetTypeRegistry()
        {
            var registry = new TypeRegistry();
            var references = _context.Assembly.References;

            foreach (var assemblyRef in references)
            {
                var assemblyDef = AssemblyFactory.GetAssemblyDefinition(assemblyRef);
                var types = assemblyDef.Module.Types.Where(type => !type.IsModuleType);
                foreach (var type in types)
                {
                    registry.Register(type);
                }
            }

            return registry;
        }
    }
}
