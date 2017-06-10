using CSharpCompiler.PE.Metadata.Resolvers;
using CSharpCompiler.PE.Metadata.Signatures;
using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.PE.Metadata
{
    public sealed class MetadataSystem
    {
        private MetadataReader _reader;

        private Lazy<MethodDefinition> _entryPoint;
        private Lazy<AssemblyDefinition> _assembly;
        private Lazy<ModuleDefinition> _module;
                
        private Lazy<TypeDefinition[]> _types;
        private Lazy<MethodDefinition[]> _methods;
        private Lazy<FieldDefinition[]> _fields;
        private Lazy<AssemblyReference[]> _assemblyRefs;
        private Lazy<TypeReference[]> _typeRefs;
        private Lazy<IMemberReference[]> _memberRefs;
        private Lazy<CustomAttribute[]> _attributes;
        
        private Lazy<ILookup<MetadataToken, TypeReference>> _typeRefLookup;
        private Lazy<ILookup<MetadataToken, IMemberReference>> _memberRefLookup;
        private Lazy<ILookup<MetadataToken, CustomAttribute>> _attributeLookup;
        private Lazy<Dictionary<MetadataToken, ITypeInfo>> _enclosingTypeMapping;

        public MethodDefinition EntryPoint => _entryPoint.Value;
        public AssemblyDefinition Assembly => _assembly.Value;
        public ModuleDefinition Module => _module.Value;

        public TypeDefinition[] Types => _types.Value;
        public MethodDefinition[] Methods => _methods.Value;
        public FieldDefinition[] Fields => _fields.Value;
        public AssemblyReference[] AssemblyReferences => _assemblyRefs.Value;
        public TypeReference[] TypeReferences => _typeRefs.Value;
        public IMemberReference[] MemberReferences => _memberRefs.Value;
        public CustomAttribute[] CustomAttributes => _attributes.Value;

        public MetadataSystem(MetadataReader reader)
        {
            _reader = reader;

            _entryPoint = new Lazy<MethodDefinition>(() =>
                GetMethodDefinition(_reader.EntryPoint.RID));

            _assembly = new Lazy<AssemblyDefinition>(() =>
                AssemblyDefinitionResolver.Resolve(_reader.AssemblyTable.Row, this));

            _module = new Lazy<ModuleDefinition>(() =>
                ModuleDefinitionResolver.Resolve(_reader.ModuleTable.Row, this));

            _assemblyRefs = new Lazy<AssemblyReference[]>(() =>
                _reader.AssemblyRefTable
                    .Select((row, id) => AssemblyReferenceResolver.Resolve((uint)(id + 1), row, this))
                    .ToArray(_reader.AssemblyRefTable.Length));

            _types = new Lazy<TypeDefinition[]>(() =>
                _reader.TypeDefTable
                     .Select((row, id) => TypeDefinitionResolver.Resolve((uint)(id + 1), row, this))
                     .ToArray(_reader.TypeDefTable.Length));

            _methods = new Lazy<MethodDefinition[]>(() =>
                _types.Value.SelectMany(type => type.Methods)
                    .ToArray(_reader.MethodTable.Length));

            _fields = new Lazy<FieldDefinition[]>(() =>
                _types.Value.SelectMany(type => type.Fields)
                    .ToArray(_reader.FieldTable.Length));

            _typeRefLookup = new Lazy<ILookup<MetadataToken, TypeReference>>(() =>
                _reader.TypeRefTable.ToLookup(row => row.ResolutionScope,
                    row => TypeReferenceResolver.Resolve(row, this)));

            _memberRefLookup = new Lazy<ILookup<MetadataToken, IMemberReference>>(() =>
                _reader.MemberRefTable.ToLookup(row => row.Class,
                    row => MemberReferenceResolver.Resolve(row, this)));

            _attributeLookup = new Lazy<ILookup<MetadataToken, CustomAttribute>>(() =>
                _reader.CustomAttributeTable.ToLookup(row => row.Parent,
                    row => CustomAttributeResolver.Resolve(row, this)));

            _typeRefs = new Lazy<TypeReference[]>(() =>
                _typeRefLookup.Value.SelectMany(items => items)
                    .ToArray(_reader.TypeRefTable.Length));

            _memberRefs = new Lazy<IMemberReference[]>(() =>
                _memberRefLookup.Value.SelectMany(items => items)
                    .ToArray(_reader.MemberRefTable.Length));

            _attributes = new Lazy<CustomAttribute[]>(() =>
                _attributeLookup.Value.SelectMany(items => items)
                    .ToArray(_reader.CustomAttributeTable.Length));

            _enclosingTypeMapping = new Lazy<Dictionary<MetadataToken, ITypeInfo>>(() =>
                _reader.NestedClassTable.ToDictionary(
                    row => row.NestedClass, 
                    row => GetTypeInfo(row.EnclosingClass)));
        }

        public ByteBuffer ResolveBlob(uint index)
        {
            return _reader.ResolveBlob(index);
        }

        public byte[] ResolveBytes(uint index)
        {
            return _reader.ResolveBytes(index);
        }

        public Guid ResolveGuid(uint index)
        {
            return _reader.ResolveGuid(index);
        }

        public string ResolveString(uint index)
        {
            return _reader.ResolveString(index);
        }

        public string ResolveUserString(uint index)
        {
            return _reader.ResolveUserString(index);
        }

        public AssemblyReference GetAssemblyReference(uint rid)
        {
            return Lookup(AssemblyReferences, rid);
        }

        public IAssemblyInfo GetAssemblyInfo(MetadataToken token)
        {
            switch (token.Type)
            {
                case MetadataTokenType.Assembly: return Assembly;
                case MetadataTokenType.AssemblyRef: return GetAssemblyReference(token.RID);
                default: throw new NotSupportedException();
            }
        }

        public TypeDefinition GetTypeDefinition(uint rid)
        {
            return Lookup(Types, rid);
        }

        public TypeReference GetTypeReference(uint rid)
        {
            return Lookup(TypeReferences, rid);
        }

        public ITypeInfo GetTypeInfo(MetadataToken token)
        {
            switch (token.Type)
            {
                case MetadataTokenType.TypeDef: return GetTypeDefinition(token.RID);
                case MetadataTokenType.TypeRef: return GetTypeReference(token.RID);
                default: throw new NotSupportedException();
            }
        }

        public MethodDefinition GetMethodDefinition(uint rid)
        {
            return Lookup(Methods, rid);
        }

        public FieldDefinition GetFieldDefinition(uint rid)
        {
            return Lookup(Fields, rid);
        }

        public MethodReference GetMethodReference(uint rid)
        {
            return (MethodReference)Lookup(MemberReferences, rid);
        }

        public FieldReference GetFieldReference(uint rid)
        {
            return (FieldReference)Lookup(MemberReferences, rid);
        }

        public IMethodInfo GetMethodInfo(MetadataToken token)
        {
            switch (token.Type)
            {
                case MetadataTokenType.Method: return GetMethodDefinition(token.RID);
                case MetadataTokenType.MemberRef: return GetMethodReference(token.RID);
                default: throw new NotSupportedException();
            }
        }

        public IEnumerable<CustomAttribute> GetCustomAttributes(MetadataToken token)
        {
            return _attributeLookup.Value[token];
        }

        public Collection<MethodDefinition> GetMethods(uint typeRid, TypeDefRow typeRow, TypeDefinition typeDef)
        {
            var start = typeRow.MethodList;
            var count = GetMethodsCount(typeRid, typeRow);
            var methods = new MethodDefinition[count];

            for (uint offset = 0; offset < count; offset++)
            {
                var methodRid = start + offset;
                var methodRow = _reader.MethodTable[methodRid];
                methods[offset] = MethodDefinitionResolver.Resolve(methodRid, methodRow, this, typeDef);
            }

            return new Collection<MethodDefinition>(methods);
        }

        public Collection<FieldDefinition> GetFields(uint typeRid, TypeDefRow typeRow, TypeDefinition typeDef)
        {
            var start = typeRow.FieldList;
            var count = GetFieldsCount(typeRid, typeRow);
            var fields = new FieldDefinition[count];

            for (uint offset = 0; offset < count; offset++)
            {
                var fieldRid = start + offset;
                var fieldRow = _reader.FieldTable[fieldRid];
                fields[offset] = FieldDefinitionResolver.Resolve(fieldRid, fieldRow, this, typeDef);
            }

            return new Collection<FieldDefinition>(fields);
        }

        public Collection<ParameterDefinition> GetParameters(MethodRow methodRow, MethodSignatureReader signature, MethodDefinition methodDef)
        {
            var start = methodRow.ParamList;
            var count = signature.ParamCount;
            var parameters = new ParameterDefinition[count];

            for (uint offset = 0; offset < count; offset++)
            {
                var paramRid = start + offset;
                var paramRow = _reader.ParameterTable[paramRid];
                var paramType = signature.ParamTypes[paramRow.Sequence - 1];
                parameters[offset] = ParameterDefinitionResolver.Resolve(paramRid, paramRow, this, methodDef, paramType);
            }

            return new Collection<ParameterDefinition>(parameters);
        }

        public ITypeInfo GetEnclosingType(MetadataToken nestedTypeToken)
        {
            return _enclosingTypeMapping.Value[nestedTypeToken];
        }

        private int GetMethodsCount(uint typeRid, TypeDefRow typeRow)
        {
            var types = _reader.TypeDefTable;
            var methods = _reader.MethodTable;
            return (typeRid == types.Length)
                ? methods.Length - typeRow.MethodList
                : types[typeRid + 1].MethodList - typeRow.MethodList;
        }

        private int GetFieldsCount(uint typeRid, TypeDefRow typeRow)
        {
            var types = _reader.TypeDefTable;
            var fields = _reader.FieldTable;
            return (typeRid == types.Length)
                ? fields.Length - typeRow.FieldList
                : types[typeRid + 1].FieldList - typeRow.FieldList;
        }

        private TEntity Lookup<TEntity>(TEntity[] collection, uint rid)
        {
            return (rid == 0 || rid > collection.Length) 
                ? default(TEntity) 
                : collection[(int)rid - 1];
        }
    }
}
