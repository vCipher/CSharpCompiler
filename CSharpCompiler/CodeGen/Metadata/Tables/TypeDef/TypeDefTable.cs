using System;
using System.Collections.ObjectModel;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;

namespace CSharpCompiler.CodeGen.Metadata.Tables.TypeRef
{
    public sealed class TypeDefTable : MetadataTable<TypeDefRow>
    {
        private MetadataBuilder _metadata;

        public TypeDefTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public void AddModuleTypeDefinition()
        {
            Add(CreateModuleTypeDefRow());
        }

        public void AddRange(Collection<TypeDefinition> types)
        {
            types.EmptyIfNull().ForEach(typeDef => Add(typeDef));
        }

        public void Add(TypeDefinition typeDef)
        {
            uint rid = Add(CreateTypeDefRow(typeDef));
            typeDef.ResolveToken(rid);
        }

        private TypeDefRow CreateModuleTypeDefRow()
        {
            TypeDefRow row = new TypeDefRow();
            row.Attributes = TypeAttributes.NotPublic |
                TypeAttributes.AutoLayout |
                TypeAttributes.Class |
                TypeAttributes.AnsiClass;
            row.Name = _metadata.RegisterString("<Module>");
            row.FieldList = _metadata.FieldTable.Position;
            row.MethodList = _metadata.MethodTable.Position;

            return row;
        }

        private TypeDefRow CreateTypeDefRow(TypeDefinition typeDef)
        {
            TypeDefRow row = new TypeDefRow();
            row.Attributes = typeDef.Attributes;
            row.Namespace = _metadata.RegisterString(typeDef.Namespace);
            row.Name = _metadata.RegisterString(typeDef.Name);
            row.Extends = _metadata.GetCodedRID(typeDef.BaseType, CodedTokenType.TypeDefOrRef);
            row.FieldList = _metadata.FieldTable.AddRange(typeDef.Fields);
            row.MethodList = _metadata.MethodTable.AddRange(typeDef.Methods);

            return row;
        }
    }
}
