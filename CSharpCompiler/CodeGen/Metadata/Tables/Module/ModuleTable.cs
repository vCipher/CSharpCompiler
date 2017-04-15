using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Module
{
    public sealed class ModuleTable : MetadataTable<ModuleDefinition, ModuleRow>
    {
        private MetadataBuilder _metadata;

        public ModuleTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public void Add(ModuleDefinition moduleDef)
        {
            Add(moduleDef, CreateModuleRow(moduleDef));
        }

        protected override MetadataTokenType GetTokenType()
        {
            return MetadataTokenType.Module;
        }

        private ModuleRow CreateModuleRow(ModuleDefinition moduleDef)
        {
            return new ModuleRow()
            {
                Generation = 0,
                Name = _metadata.RegisterString(moduleDef.Name),
                Mvid = _metadata.RegisterGuid(moduleDef.Mvid),
                EncId = 0,
                EncBaseId = 0
            };
        }
    }
}
