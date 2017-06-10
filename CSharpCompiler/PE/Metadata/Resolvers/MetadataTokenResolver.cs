using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;

namespace CSharpCompiler.PE.Metadata
{
    public sealed class MetadataTokenResolver : IMetadataEntityVisitor
    {
        private MetadataBuilder _metadata;
        private MetadataToken _token;

        private MetadataTokenResolver(MetadataBuilder metadata)
        {
            _metadata = metadata;
            _token = new MetadataToken();
        }

        public static MetadataToken ResolveToken(IMetadataEntity entity, MetadataBuilder builder)
        {
            var resolver = new MetadataTokenResolver(builder);
            entity.Accept(resolver);
            return resolver._token;
        }

        public void VisitAssemblyDefinition(AssemblyDefinition entity) => ResolveAssemblyToken(entity);
        public void VisitAssemblyReference(AssemblyReference entity) => ResolveAssemblyRefToken(entity);
        public void VisitCustomAttribute(CustomAttribute entity) => ResolveCustomAttributeToken(entity);
        public void VisitFieldDefinition(FieldDefinition entity) => ResolveFieldToken(entity);
        public void VisitMethodReference(MethodReference entity) => ResolveMemberRefToken(entity);
        public void VisitFieldReference(FieldReference entity) => ResolveMemberRefToken(entity);
        public void VisitMethodDefinition(MethodDefinition entity) => ResolveMethodToken(entity);
        public void VisitModuleDefinition(ModuleDefinition entity) => ResolveModuleToken(entity);
        public void VisitParameterDefinition(ParameterDefinition entity) => ResolveParamToken(entity);
        public void VisitStandAloneSignature(StandAloneSignature entity) => ResolveStandAloneSigToken(entity);
        public void VisitTypeDefinition(TypeDefinition entity) => ResolveTypeDefToken(entity);
        public void VisitTypeReference(TypeReference entity) => ResolveTypeRefToken(entity);
        public void VisitTypeSpecification(ITypeInfo entity) => ResolveTypeSpecToken(entity);

        private void ResolveAssemblyToken(AssemblyDefinition entity)
        {
            var row = CreateAssemblyRow(entity, _metadata);
            var rid = _metadata.AssemblyTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.Assembly, rid);
        }

        private void ResolveAssemblyRefToken(AssemblyReference entity)
        {
            var row = CreateAssemblyRefRow(entity, _metadata);
            var rid = _metadata.AssemblyRefTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.AssemblyRef, rid);
        }

        private void ResolveCustomAttributeToken(CustomAttribute entity)
        {
            var row = CreateCustomAttributeRow(entity, _metadata);
            var rid = _metadata.CustomAttributeTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.CustomAttribute, rid);
        }

        private void ResolveFieldToken(FieldDefinition entity)
        {
            var row = CreateFieldRow(entity, _metadata);
            var rid = _metadata.FieldTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.Field, rid);
        }

        private void ResolveMemberRefToken(IMemberReference entity)
        {
            var row = CreateMemberRefRow(entity, _metadata);
            var rid = _metadata.MemberRefTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.MemberRef, rid);
        }

        private void ResolveMethodToken(MethodDefinition entity)
        {
            var row = CreateMethodRow(entity, _metadata);
            var rid = _metadata.MethodTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.Method, rid);
        }

        private void ResolveModuleToken(ModuleDefinition entity)
        {
            var row = CreateModuleRow(entity, _metadata);
            var rid = _metadata.ModuleTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.Module, rid);
        }

        private void ResolveParamToken(ParameterDefinition entity)
        {
            var row = CreateParamRow(entity, _metadata);
            var rid = _metadata.ParamTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.Param, rid);
        }

        private void ResolveStandAloneSigToken(StandAloneSignature entity)
        {
            var row = CreateStandAloneSigRow(entity, _metadata);
            var rid = _metadata.StandAloneSigTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.Signature, rid);
        }

        private void ResolveTypeDefToken(TypeDefinition entity)
        {
            var row = CreateTypeDefRow(entity, _metadata);
            var rid = _metadata.TypeDefTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.TypeDef, rid);
        }

        private void ResolveTypeRefToken(TypeReference entity)
        {
            var row = CreateTypeRefRow(entity, _metadata);
            var rid = _metadata.TypeRefTable.Add(row);
            _token = new MetadataToken(MetadataTokenType.TypeRef, rid);
        }

        private void ResolveTypeSpecToken(ITypeInfo entity)
        {
            var row = CreateTypeSpecRow(entity, _metadata);
            var rid = _metadata.TypeSpecTable.Add(row);
            _token = new MetadataToken();
        }

        private AssemblyRow CreateAssemblyRow(AssemblyDefinition entity, MetadataBuilder metadata)
        {
            return new AssemblyRow(
                AssemblyHashAlgorithm.SHA1,
                (ushort)entity.Version.Major,
                (ushort)entity.Version.Minor,
                (ushort)entity.Version.Build,
                (ushort)entity.Version.Revision,
                entity.Attributes,
                metadata.WriteBlob(entity.PublicKey),
                metadata.WriteString(entity.Name),
                metadata.WriteString(entity.Culture)
            );
        }

        private AssemblyRefRow CreateAssemblyRefRow(AssemblyReference entity, MetadataBuilder metadata)
        {
            return new AssemblyRefRow(
                (ushort)entity.Version.Major,
                (ushort)entity.Version.Minor,
                (ushort)entity.Version.Build,
                (ushort)entity.Version.Revision,
                entity.Attributes,
                metadata.WriteBlob(
                    entity.PublicKeyToken.IsNullOrEmpty()
                        ? entity.PublicKey
                        : entity.PublicKeyToken),
                metadata.WriteString(entity.Name),
                metadata.WriteString(entity.Culture),
                metadata.WriteBlob(entity.Hash)
            );
        }

        private CustomAttributeRow CreateCustomAttributeRow(CustomAttribute attribute, MetadataBuilder metadata)
        {
            return new CustomAttributeRow(
                metadata.ResolveToken(attribute.Owner),
                metadata.ResolveToken(attribute.Constructor),
                metadata.WriteSignature(attribute)
            );
        }

        private FieldRow CreateFieldRow(FieldDefinition entity, MetadataBuilder metadata)
        {
            return new FieldRow(
                entity.Attributes,
                metadata.WriteString(entity.Name),
                metadata.WriteSignature(entity)
            );
        }

        private MemberRefRow CreateMemberRefRow(IMemberReference entity, MetadataBuilder metadata)
        {
            return new MemberRefRow(
                metadata.ResolveToken(entity.DeclaringType),
                metadata.WriteString(entity.Name),
                metadata.WriteSignature(entity)
            );
        }

        private MethodRow CreateMethodRow(MethodDefinition entity, MetadataBuilder metadata)
        {
            var row = new MethodRow(
                metadata.WriteMethod(entity),
                entity.ImplAttributes,
                entity.Attributes,
                metadata.WriteString(entity.Name),
                metadata.WriteSignature(entity),
                (ushort)(metadata.ParamTable.Length + 1)
            );

            foreach (var param in entity.Parameters)
                metadata.ResolveToken(param);

            return row;
        }

        private ModuleRow CreateModuleRow(ModuleDefinition entity, MetadataBuilder metadata)
        {
            return new ModuleRow(
                0,
                metadata.WriteString(entity.Name),
                metadata.WriteGuid(entity.Mvid),
                0,
                0
            );
        }

        private ParamRow CreateParamRow(ParameterDefinition entity, MetadataBuilder metadata)
        {
            return new ParamRow(
                entity.Attributes,
                (ushort)(entity.Index + 1),
                metadata.WriteString(entity.Name)
            );
        }

        private StandAloneSigRow CreateStandAloneSigRow(StandAloneSignature entity, MetadataBuilder metadata)
        {
            return new StandAloneSigRow(metadata.WriteBlob(entity));
        }

        private TypeDefRow CreateTypeDefRow(TypeDefinition entity, MetadataBuilder metadata)
        {
            var row = new TypeDefRow(
                entity.Attributes,
                metadata.WriteString(entity.Name),
                metadata.WriteString(entity.Namespace),
                metadata.ResolveToken(entity.BaseType),
                (ushort)(metadata.FieldTable.Length + 1),
                (ushort)(metadata.MethodTable.Length + 1)
            );

            foreach (var field in entity.Fields)
                metadata.ResolveToken(field);

            foreach (var method in entity.Methods)
                metadata.ResolveToken(method);

            return row;
        }

        private TypeRefRow CreateTypeRefRow(TypeReference entity, MetadataBuilder metadata)
        {
            return new TypeRefRow(
                metadata.ResolveToken(entity.Assembly),
                metadata.WriteString(entity.Name),
                metadata.WriteString(entity.Namespace)
            );
        }

        private TypeSpecRow CreateTypeSpecRow(ITypeInfo entity, MetadataBuilder metadata)
        {
            return new TypeSpecRow(metadata.WriteSignature(entity));
        }
    }
}
