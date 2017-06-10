using System;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public static class MetadataTableFactory
    {
        public static IMetadataTable Create(MetadataTableType type)
        {
            switch (type)
            {
                case MetadataTableType.Module: return new ModuleTable();
                case MetadataTableType.TypeRef: return new TypeRefTable();
                case MetadataTableType.TypeDef: return new TypeDefTable();
                case MetadataTableType.Field: return new FieldTable();
                case MetadataTableType.Method: return new MethodTable();
                case MetadataTableType.Param: return new ParamTable();
                case MetadataTableType.InterfaceImpl: return new InterfaceImplTable();
                case MetadataTableType.MemberRef: return new MemberRefTable();
                case MetadataTableType.Constant: return new ConstantTable();
                case MetadataTableType.CustomAttribute: return new CustomAttributeTable();
                case MetadataTableType.FieldMarshal: return new FieldMarshalTable();
                case MetadataTableType.DeclSecurity: return new DeclSecurityTable();
                case MetadataTableType.ClassLayout: return new ClassLayoutTable();
                case MetadataTableType.FieldLayout: return new FieldLayoutTable();
                case MetadataTableType.StandAloneSig: return new StandAloneSigTable();
                case MetadataTableType.EventMap: return new EventMapTable();
                case MetadataTableType.Event: return new EventTable();
                case MetadataTableType.PropertyMap: return new PropertyMapTable();
                case MetadataTableType.Property: return new PropertyTable();
                case MetadataTableType.MethodSemantics: return new MethodSemanticsTable();
                case MetadataTableType.MethodImpl: return new MethodImplTable();
                case MetadataTableType.ModuleRef: return new ModuleRefTable();
                case MetadataTableType.TypeSpec: return new TypeSpecTable();
                case MetadataTableType.ImplMap: return new ImplMapTable();
                case MetadataTableType.FieldRVA: return new FieldRVATable();
                case MetadataTableType.Assembly: return new AssemblyTable();
                case MetadataTableType.AssemblyRef: return new AssemblyRefTable();
                case MetadataTableType.File: return new FileTable();
                case MetadataTableType.ExportedType: return new ExportedTypeTable();
                case MetadataTableType.ManifestResource: return new ManifestResourceTable();
                case MetadataTableType.NestedClass: return new NestedClassTable();
                case MetadataTableType.GenericParam: return new GenericParamTable();
                case MetadataTableType.MethodSpec: return new MethodSpecTable();
                case MetadataTableType.GenericParamConstraint: return new GenericParamConstraintTable();
                default: throw new NotSupportedException();
            }
        }

        public static IMetadataTable Create(MetadataTableType type, int count)
        {
            switch (type)
            {
                case MetadataTableType.Module: return new ModuleTable(count);
                case MetadataTableType.TypeRef: return new TypeRefTable(count);
                case MetadataTableType.TypeDef: return new TypeDefTable(count);
                case MetadataTableType.Field: return new FieldTable(count);
                case MetadataTableType.Method: return new MethodTable(count);
                case MetadataTableType.Param: return new ParamTable(count);
                case MetadataTableType.InterfaceImpl: return new InterfaceImplTable(count);
                case MetadataTableType.MemberRef: return new MemberRefTable(count);
                case MetadataTableType.Constant: return new ConstantTable(count);
                case MetadataTableType.CustomAttribute: return new CustomAttributeTable(count);
                case MetadataTableType.FieldMarshal: return new FieldMarshalTable(count);
                case MetadataTableType.DeclSecurity: return new DeclSecurityTable(count);
                case MetadataTableType.ClassLayout: return new ClassLayoutTable(count);
                case MetadataTableType.FieldLayout: return new FieldLayoutTable(count);
                case MetadataTableType.StandAloneSig: return new StandAloneSigTable(count);
                case MetadataTableType.EventMap: return new EventMapTable(count);
                case MetadataTableType.Event: return new EventTable(count);
                case MetadataTableType.PropertyMap: return new PropertyMapTable(count);
                case MetadataTableType.Property: return new PropertyTable(count);
                case MetadataTableType.MethodSemantics: return new MethodSemanticsTable(count);
                case MetadataTableType.MethodImpl: return new MethodImplTable(count);
                case MetadataTableType.ModuleRef: return new ModuleRefTable(count);
                case MetadataTableType.TypeSpec: return new TypeSpecTable(count);
                case MetadataTableType.ImplMap: return new ImplMapTable(count);
                case MetadataTableType.FieldRVA: return new FieldRVATable(count);
                case MetadataTableType.Assembly: return new AssemblyTable(count);
                case MetadataTableType.AssemblyRef: return new AssemblyRefTable(count);
                case MetadataTableType.File: return new FileTable(count);
                case MetadataTableType.ExportedType: return new ExportedTypeTable(count);
                case MetadataTableType.ManifestResource: return new ManifestResourceTable(count);
                case MetadataTableType.NestedClass: return new NestedClassTable(count);
                case MetadataTableType.GenericParam: return new GenericParamTable(count);
                case MetadataTableType.MethodSpec: return new MethodSpecTable(count);
                case MetadataTableType.GenericParamConstraint: return new GenericParamConstraintTable(count);
                default: throw new NotSupportedException();
            }
        }
    }
}
