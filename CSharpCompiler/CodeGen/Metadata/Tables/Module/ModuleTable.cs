using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.CodeGen.Metadata.Tables.Module
{
    public sealed class ModuleTable : MetadataTable<ModuleRow>
    {
        private MetadataBuilder _metadata;

        public ModuleTable(MetadataBuilder metadata)
        {
            _metadata = metadata;
        }

        public void Add(ModuleDefinition moduleDef)
        {
            ModuleRow row = CreateModuleRow(moduleDef);
            moduleDef.ResolveToken(Add(row));
        }

        private ModuleRow CreateModuleRow(ModuleDefinition moduleDef)
        {
            ModuleRow row = new ModuleRow();
            row.Generation = 0;
            row.NameIndex = _metadata.RegisterString(moduleDef.Name);
            row.Mvid = _metadata.RegisterGuid(moduleDef.Mvid);
            row.EncId = 0;
            row.EncBaseId = 0;

            return row;
        }
    }
}
