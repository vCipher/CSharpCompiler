using CSharpCompiler.CodeGen.Metadata.Tables;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.CodeGen.Metadata
{
    public sealed class MetadataTokenResolver : IMetadataEntityVisitor
    {
        private MetadataBuilder _builder;
        private MetadataToken _token;

        private MetadataTokenResolver(MetadataBuilder builder)
        {
            _builder = builder;
            _token = new MetadataToken();
        }

        public static MetadataToken ResolveToken(IMetadataEntity entity, MetadataBuilder builder)
        {
            if (entity == null) return new MetadataToken();

            var resolver = new MetadataTokenResolver(builder);
            entity.Accept(resolver);
            return resolver._token;
        }

        public void VisitAssemblyDefinition(AssemblyDefinition entity) => Resolve(_builder.AssemblyTable, entity);
        public void VisitAssemblyReference(AssemblyReference entity) => Resolve(_builder.AssemblyRefTable, entity);
        public void VisitCustomAttribute(CustomAttribute entity) => Resolve(_builder.CustomAttributeTable, entity);
        public void VisitFieldDefinition(FieldDefinition entity) => Resolve(_builder.FieldTable, entity);
        public void VisitMethodDefinition(MethodDefinition entity) => Resolve(_builder.MethodTable, entity);
        public void VisitMethodReference(MethodReference entity) => Resolve(_builder.MemberRefTable, entity);
        public void VisitModuleDefinition(ModuleDefinition entity) => Resolve(_builder.ModuleTable, entity);
        public void VisitParameterDefinition(ParameterDefinition entity) => Resolve(_builder.ParameterTable, entity);
        public void VisitStandAloneSignature(StandAloneSignature entity) => Resolve(_builder.StandAloneSigTable, entity);
        public void VisitTypeDefinition(TypeDefinition entity) => Resolve(_builder.TypeDefTable, entity);
        public void VisitTypeReference(TypeReference entity) => Resolve(_builder.TypeRefTable, entity);
        public void VisitTypeSpecification(TypeSpecification entity) { /* TODO: implement */}

        private void Resolve<TEntity, TRow>(MetadataTable<TEntity, TRow> table, TEntity entity)
            where TEntity : IMetadataEntity
            where TRow : struct
        {
            _token = table.GetToken(entity);
        }
    }
}
