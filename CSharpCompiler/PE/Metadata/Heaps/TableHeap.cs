using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.PE.Metadata.Heaps
{
    public sealed class TableHeap : IEnumerable<IMetadataTable>
    {
        public const int TABLES_COUNT = 45;
        
        private IMetadataTable[] _tables;

        public AssemblyTable AssemblyTable => GetOrCreate<AssemblyTable>(MetadataTableType.Assembly);
        public AssemblyRefTable AssemblyRefTable => GetOrCreate<AssemblyRefTable>(MetadataTableType.AssemblyRef);
        public CustomAttributeTable CustomAttributeTable => GetOrCreate<CustomAttributeTable>(MetadataTableType.CustomAttribute);
        public FieldTable FieldTable => GetOrCreate<FieldTable>(MetadataTableType.Field);
        public MemberRefTable MemberRefTable => GetOrCreate<MemberRefTable>(MetadataTableType.MemberRef);
        public MethodTable MethodTable => GetOrCreate<MethodTable>(MetadataTableType.Method);
        public ModuleTable ModuleTable => GetOrCreate<ModuleTable>(MetadataTableType.Module);
        public ParamTable ParameterTable => GetOrCreate<ParamTable>(MetadataTableType.Param);
        public StandAloneSigTable StandAloneSigTable => GetOrCreate<StandAloneSigTable>(MetadataTableType.StandAloneSig);
        public TypeDefTable TypeDefTable => GetOrCreate<TypeDefTable>(MetadataTableType.TypeDef);
        public TypeRefTable TypeRefTable => GetOrCreate<TypeRefTable>(MetadataTableType.TypeRef);
        public NestedClassTable NestedClassTable => GetOrCreate<NestedClassTable>(MetadataTableType.NestedClass);

        public TableHeap()
        {
            _tables = new IMetadataTable[TABLES_COUNT];
        }

        public TTable GetOrCreate<TTable>(MetadataTableType type) where TTable : IMetadataTable
        {
            return (TTable)GetOrCreate(type, () => MetadataTableFactory.Create(type));
        }

        public IMetadataTable GetOrCreate(MetadataTableType type)
        {
            return GetOrCreate(type, () => MetadataTableFactory.Create(type));
        }

        public IMetadataTable GetOrCreate(MetadataTableType type, Func<IMetadataTable> factory)
        {
            var table = _tables[(int)type];
            if (table == null)
            {
                table = factory();
                _tables[(int)type] = table;
            }
            return table;
        }

        public bool TryGetTable(MetadataTokenType tokenType, out IMetadataTable table)
        {
            if (tokenType == MetadataTokenType.String)
            {
                table = null;
                return false;
            }

            return TryGetTable(tokenType.ToTableType(), out table);
        }

        public bool TryGetTable(MetadataTableType tableType, out IMetadataTable table)
        {
            table = _tables[(int)tableType];
            return table != null;
        }

        public ulong GetValidMask()
        {
            ulong valid = 0;
            for (int index = 0; index < TABLES_COUNT; index++)
            {
                if (_tables[index] != null)
                    valid |= (1ul << index);
            }

            return valid;
        }

        public ulong GetSortedMask()
        {
            ulong sorted = 0;
            sorted |= 1ul << (int)MetadataTableType.InterfaceImpl;
            sorted |= 1ul << (int)MetadataTableType.Constant;
            sorted |= 1ul << (int)MetadataTableType.CustomAttribute;
            sorted |= 1ul << (int)MetadataTableType.FieldMarshal;
            sorted |= 1ul << (int)MetadataTableType.DeclSecurity;
            sorted |= 1ul << (int)MetadataTableType.ClassLayout;
            sorted |= 1ul << (int)MetadataTableType.FieldLayout;
            sorted |= 1ul << (int)MetadataTableType.EventMap;
            sorted |= 1ul << (int)MetadataTableType.PropertyMap;
            sorted |= 1ul << (int)MetadataTableType.MethodSemantics;
            sorted |= 1ul << (int)MetadataTableType.MethodImpl;
            sorted |= 1ul << (int)MetadataTableType.ImplMap;
            sorted |= 1ul << (int)MetadataTableType.FieldRVA;
            sorted |= 1ul << (int)MetadataTableType.NestedClass;
            sorted |= 1ul << (int)MetadataTableType.GenericParam;
            sorted |= 1ul << (int)MetadataTableType.GenericParamConstraint;

            return sorted;
        }

        public IEnumerator<IMetadataTable> GetEnumerator()
        {
            for (int index = 0; index < TABLES_COUNT; index++)
            {
                var table = _tables[index];
                if (table != null)
                    yield return table;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
