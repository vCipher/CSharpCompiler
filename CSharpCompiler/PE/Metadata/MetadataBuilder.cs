using CSharpCompiler.PE.Cil;
using CSharpCompiler.PE.Metadata.Resolvers;
using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System;
using System.Collections.Generic;

namespace CSharpCompiler.PE.Metadata
{
    public sealed class MetadataBuilder
    {
        private AssemblyDefinition _assemblyDef;
        private MetadataHeaps _heaps;
        private ILCodeWriter _ilCode;

        private Func<ByteBuffer, uint> _writeBlob;
        private Func<Guid, uint> _writeGuid;
        private Func<string, uint> _writeString;
        private Func<string, uint> _writeUserString;
        private Func<IMetadataEntity, MetadataToken> _resolveToken;

        public AssemblyTable AssemblyTable => GetOrCreate<AssemblyTable>(MetadataTableType.Assembly);
        public AssemblyRefTable AssemblyRefTable => GetOrCreate<AssemblyRefTable>(MetadataTableType.AssemblyRef);
        public CustomAttributeTable CustomAttributeTable => GetOrCreate<CustomAttributeTable>(MetadataTableType.CustomAttribute);
        public FieldTable FieldTable => GetOrCreate<FieldTable>(MetadataTableType.Field);
        public MemberRefTable MemberRefTable => GetOrCreate<MemberRefTable>(MetadataTableType.MemberRef);
        public MethodTable MethodTable => GetOrCreate<MethodTable>(MetadataTableType.Method);
        public ModuleTable ModuleTable => GetOrCreate<ModuleTable>(MetadataTableType.Module);
        public ParamTable ParamTable => GetOrCreate<ParamTable>(MetadataTableType.Param);
        public StandAloneSigTable StandAloneSigTable => GetOrCreate<StandAloneSigTable>(MetadataTableType.StandAloneSig);
        public TypeDefTable TypeDefTable => GetOrCreate<TypeDefTable>(MetadataTableType.TypeDef);
        public TypeRefTable TypeRefTable => GetOrCreate<TypeRefTable>(MetadataTableType.TypeRef);
        public TypeSpecTable TypeSpecTable => GetOrCreate<TypeSpecTable>(MetadataTableType.TypeSpec);

        private MetadataBuilder(AssemblyDefinition assemblyDef)
        {
            _assemblyDef = assemblyDef;
            _heaps = new MetadataHeaps();
            _ilCode = new ILCodeWriter(this);
            _writeBlob = Func.Memoize<ByteBuffer, uint>(_heaps.Blobs.WriteBlob);
            _writeGuid = Func.Memoize<Guid, uint>(_heaps.Guids.WriteGuid);
            _writeString = Func.Memoize<string, uint>(_heaps.Strings.WriteString, StringComparer.Ordinal);
            _writeUserString = Func.Memoize<string, uint>(_heaps.UserStrings.WriteString, StringComparer.Ordinal);
            _resolveToken = Func.Memoize<IMetadataEntity, MetadataToken>(entity => MetadataTokenResolver.ResolveToken(entity, this));
        }

        public static MetadataBuildResult Build(AssemblyDefinition assemblyDef)
        {
            return new MetadataBuilder(assemblyDef).Build();
        }

        public uint WriteGuid(Guid guid)
        {
            return _writeGuid(guid);
        }

        public uint WriteString(string @string)
        {
            if (string.IsNullOrEmpty(@string))
                return 0;

            return _writeString(@string);
        }

        public uint WriteUserString(string @string)
        {
            if (@string == null)
                return 0;

            return _writeUserString(@string);
        }

        public uint WriteBlob(byte[] bytes)
        {
            if (bytes.IsNullOrEmpty())
                return 0;

            return _writeBlob(new ByteBuffer(bytes));
        }

        public uint WriteBlob(ByteBuffer buffer)
        {
            if (buffer.Length == 0)
                return 0;

            return _writeBlob(buffer);
        }

        public uint WriteMethod(MethodDefinition methodDef)
        {
            return _ilCode.WriteMethod(methodDef);
        }

        public uint WriteSignature(IMethodInfo methodInfo)
        {
            var signature = StandAloneSignature.GetMethodSignature(methodInfo, this);
            return WriteBlob(signature);
        }

        public uint WriteSignature(IMemberReference memberRef)
        {
            var signature = StandAloneSignature.GetMemberReferenceSignature(memberRef, this);
            return WriteBlob(signature);
        }

        public uint WriteSignature(CustomAttribute attribute)
        {
            var signature = StandAloneSignature.GetAttributeSignature(attribute, this);
            return WriteBlob(signature);
        }

        public uint WriteSignature(FieldDefinition fieldDef)
        {
            var signature = StandAloneSignature.GetFieldSignature(fieldDef, this);
            return WriteBlob(signature);
        }

        public uint WriteSignature(ITypeInfo type)
        {
            var signature = StandAloneSignature.GetTypeSignature(type, this);
            return WriteBlob(signature);
        }

        public TTable GetOrCreate<TTable>(MetadataTableType type) where TTable : IMetadataTable
        {
            return _heaps.Tables.GetOrCreate<TTable>(type);
        }

        public MetadataToken ResolveToken(IMetadataEntity entity)
        {
            return (entity == null) ? new MetadataToken() : _resolveToken(entity);
        }

        private MetadataBuildResult Build()
        {
            BuildModule(_assemblyDef.Module);
            BuildAssembly(_assemblyDef);
            BuildTypes(_assemblyDef.Module.Types);

            _heaps.Tables.Write();

            return new MetadataBuildResult(ResolveToken(_assemblyDef.EntryPoint), _heaps, _ilCode);
        }

        private void BuildModule(ModuleDefinition moduleDef)
        {
            ResolveToken(ModuleTypeDefinitionResolver.Resolve());
            ResolveToken(moduleDef);
        }

        private void BuildAssembly(AssemblyDefinition assemblyDef)
        {
            ResolveToken(assemblyDef);
            foreach (var attr in assemblyDef.CustomAttributes)
            {
                ResolveToken(attr);
            }
        }

        private void BuildTypes(IEnumerable<TypeDefinition> types)
        {
            foreach (var type in types)
            {
                ResolveToken(type);
            }
        }
    }
}
