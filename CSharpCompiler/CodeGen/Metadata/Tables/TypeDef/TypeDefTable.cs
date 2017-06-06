using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System.Collections.ObjectModel;

namespace CSharpCompiler.CodeGen.Metadata.Tables.TypeRef
{
    public sealed class TypeDefTable : MetadataTable<TypeDefinition, TypeDefRow>
    {
        private MetadataBuilder _metadata;

        public TypeDefTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public void AddModuleTypeDefinition()
        {
            var typeDef = CreateModuleTypeDefinition();
            Add(typeDef, CreateTypeDefRow(typeDef));
        }

        public void AddRange(Collection<TypeDefinition> types)
        {
            types.EmptyIfNull().ForEach(typeDef => Add(typeDef));
        }

        public void Add(TypeDefinition typeDef)
        {
            Add(typeDef, CreateTypeDefRow(typeDef));
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.TypeDef;
        }

        private TypeDefinition CreateModuleTypeDefinition()
        {
            var attr = TypeAttributes.NotPublic |
                    TypeAttributes.AutoLayout |
                    TypeAttributes.Class |
                    TypeAttributes.AnsiClass;

            return new TypeDefinition("<Module>", string.Empty, attr, null, null);
        }

        private TypeDefRow CreateTypeDefRow(TypeDefinition typeDef)
        {
            return new TypeDefRow()
            {
                Attributes = typeDef.Attributes,
                Namespace = _metadata.RegisterString(typeDef.Namespace),
                Name = _metadata.RegisterString(typeDef.Name),
                Extends = _metadata.GetCodedRid(typeDef.BaseType, CodedTokenType.TypeDefOrRef),
                FieldList = _metadata.FieldTable.AddRange(typeDef.Fields),
                MethodList = _metadata.MethodTable.AddRange(typeDef.Methods)
            };
        }
    }
}
