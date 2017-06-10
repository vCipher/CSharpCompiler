using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Utility;
using System;

namespace CSharpCompiler.PE.Metadata
{
    public sealed class MetadataReader
    {
        private MetadataHeaps _heaps;
        private MetadataToken _entryPoint;

        private Func<uint, ByteBuffer> _resolveBlob;
        private Func<uint, Guid> _resolveGuid;
        private Func<uint, string> _resolveString;
        private Func<uint, string> _resolveUserString;

        public MetadataToken EntryPoint => _entryPoint;
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

        public MetadataReader(MetadataHeaps heaps, MetadataToken entryPoint)
        {
            _heaps = heaps;
            _entryPoint = entryPoint;
            _resolveBlob = Func.Memoize<uint, ByteBuffer>(_heaps.Blobs.ReadBlob);
            _resolveGuid = Func.Memoize<uint, Guid>(_heaps.Guids.ReadGuid);
            _resolveString = Func.Memoize<uint, string>(_heaps.Strings.ReadString);
            _resolveUserString = Func.Memoize<uint, string>(_heaps.UserStrings.ReadString);
        }

        public TTable GetOrCreate<TTable>(MetadataTableType type) where TTable : IMetadataTable, new()
        {
            return _heaps.Tables.GetOrCreate<TTable>(type);
        }

        public ByteBuffer ResolveBlob(uint index)
        {
            if (index == 0)
                return new ByteBuffer();

            return _resolveBlob(index);
        }

        public byte[] ResolveBytes(uint index)
        {
            if (index == 0)
                return Empty<byte>.Array;

            return _resolveBlob(index).Buffer;
        }

        public Guid ResolveGuid(uint index)
        {
            return _resolveGuid(index);
        }

        public string ResolveString(uint index)
        {
            if (index == 0)
                return string.Empty;

            return _resolveString(index);
        }

        public string ResolveUserString(uint index)
        {
            if (index == 0)
                return string.Empty;

            return _resolveUserString(index);
        }
    }
}
