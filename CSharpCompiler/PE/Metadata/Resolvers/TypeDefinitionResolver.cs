using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Utility;
using System.Collections.ObjectModel;
using System.Text;

namespace CSharpCompiler.PE.Metadata.Resolvers
{
    public sealed class TypeDefinitionResolver : ITypeDefinitionResolver
    {
        private MetadataToken _token;
        private TypeDefRow _row;
        private MetadataSystem _metadata;

        private TypeDefinitionResolver(uint rid, TypeDefRow row, MetadataSystem metadata)
        {
            _token = new MetadataToken(MetadataTokenType.TypeDef, rid);
            _row = row;
            _metadata = metadata;
        }

        public static TypeDefinition Resolve(uint rid, TypeDefRow row, MetadataSystem metadata)
        {
            var resolver = new TypeDefinitionResolver(rid, row, metadata);
            return new TypeDefinition(resolver);
        }

        public string GetName()
        {
            var builder = new StringBuilder();
            var name = _metadata.ResolveString(_row.Name);

            if (_row.Attributes.IsNested())
            {
                var enclosing = _metadata.GetEnclosingType(_token);
                builder.AppendFormat("{0}+", enclosing.Name);
            }

            return builder.Append(name).ToString();
        }

        public string GetNamespace()
        {
            return _metadata.ResolveString(_row.Namespace);
        }

        public ElementType GetElementType(ITypeInfo type)
        {
            var knownTypeCode = KnownType.GetTypeCode(type);
            if (knownTypeCode != KnownTypeCode.None)
                return KnownType.GetElementType(knownTypeCode);

            return ElementType.Class;
        }

        public IAssemblyInfo GetAssembly()
        {
            return _metadata.Assembly;
        }

        public TypeAttributes GetAttributes()
        {
            return _row.Attributes;
        }

        public ITypeInfo GetBaseType()
        {
            return _metadata.GetTypeInfo(_row.Extends);
        }

        public Collection<MethodDefinition> GetMethods(TypeDefinition typeDef)
        {
            return _metadata.GetMethods(_token.RID, _row, typeDef);
        }

        public Collection<FieldDefinition> GetFields(TypeDefinition typeDef)
        {
            return _metadata.GetFields(_token.RID, _row, typeDef);
        }

        public Collection<CustomAttribute> GetCustomAttributes(TypeDefinition typeDef)
        {
            return _metadata.GetCustomAttributes(_token)
                .ToCollection();
        }
    }
}
