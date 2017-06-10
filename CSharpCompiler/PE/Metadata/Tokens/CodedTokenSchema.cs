using CSharpCompiler.PE.Metadata.Heaps;
using CSharpCompiler.PE.Metadata.Tables;
using System;
using System.Collections.Generic;

namespace CSharpCompiler.PE.Metadata.Tokens
{
    public static class CodedTokenSchema
    {
        private static Dictionary<CodedTokenType, MetadataTokenType[]> _schema;
        private static Dictionary<CodedTokenType, int> _sizes;

        static CodedTokenSchema()
        {
            _schema = new Dictionary<CodedTokenType, MetadataTokenType[]>();
            _sizes = new Dictionary<CodedTokenType, int>(_schema.Count);

            _schema.Add(CodedTokenType.TypeDefOrRef, new MetadataTokenType[]
            {
                MetadataTokenType.TypeDef,
                MetadataTokenType.TypeRef,
                MetadataTokenType.TypeSpec
            });

            _schema.Add(CodedTokenType.HasConstant, new MetadataTokenType[]
            {
                MetadataTokenType.Field,
                MetadataTokenType.Param,
                MetadataTokenType.Property
            });

            _schema.Add(CodedTokenType.HasCustomAttribute, new MetadataTokenType[]
            {
                MetadataTokenType.Method,
                MetadataTokenType.Field,
                MetadataTokenType.TypeRef,
                MetadataTokenType.TypeDef,
                MetadataTokenType.Param,
                MetadataTokenType.InterfaceImpl,
                MetadataTokenType.MemberRef,
                MetadataTokenType.Module,
                MetadataTokenType.Permission,
                MetadataTokenType.Property,
                MetadataTokenType.Event,
                MetadataTokenType.Signature,
                MetadataTokenType.ModuleRef,
                MetadataTokenType.TypeSpec,
                MetadataTokenType.Assembly,
                MetadataTokenType.AssemblyRef,
                MetadataTokenType.File,
                MetadataTokenType.ExportedType,
                MetadataTokenType.ManifestResource,
                MetadataTokenType.GenericParam
            });

            _schema.Add(CodedTokenType.HasFieldMarshal, new MetadataTokenType[]
            {
                MetadataTokenType.Field,
                MetadataTokenType.Param
            });

            _schema.Add(CodedTokenType.HasDeclSecurity, new MetadataTokenType[]
            {
                MetadataTokenType.TypeDef,
                MetadataTokenType.Method,
                MetadataTokenType.Assembly
            });

            _schema.Add(CodedTokenType.MemberRefParent, new MetadataTokenType[]
            {
                MetadataTokenType.TypeDef,
                MetadataTokenType.TypeRef,
                MetadataTokenType.ModuleRef,
                MetadataTokenType.Method,
                MetadataTokenType.TypeSpec
            });

            _schema.Add(CodedTokenType.HasSemantics, new MetadataTokenType[]
            {
                MetadataTokenType.Event,
                MetadataTokenType.Property
            });

            _schema.Add(CodedTokenType.MethodDefOrRef, new MetadataTokenType[]
            {
                MetadataTokenType.Method,
                MetadataTokenType.MemberRef
            });

            _schema.Add(CodedTokenType.MemberForwarded, new MetadataTokenType[]
            {
                MetadataTokenType.Field,
                MetadataTokenType.Method
            });

            _schema.Add(CodedTokenType.Implementation, new MetadataTokenType[]
            {
                MetadataTokenType.File,
                MetadataTokenType.AssemblyRef,
                MetadataTokenType.ExportedType
            });

            _schema.Add(CodedTokenType.CustomAttributeType, new MetadataTokenType[]
            {
                MetadataTokenType.TypeRef,
                MetadataTokenType.TypeDef,
                MetadataTokenType.Method,
                MetadataTokenType.MemberRef,
                MetadataTokenType.String
            });

            _schema.Add(CodedTokenType.ResolutionScope, new MetadataTokenType[]
            {
                MetadataTokenType.Module,
                MetadataTokenType.ModuleRef,
                MetadataTokenType.AssemblyRef,
                MetadataTokenType.TypeRef
            });

            _schema.Add(CodedTokenType.TypeOrMethodDef, new MetadataTokenType[]
            {
                MetadataTokenType.TypeDef,
                MetadataTokenType.Method
            });
            
            foreach (var pairs in _schema)
            {
                _sizes.Add(pairs.Key, GetTagSize(pairs.Value));
            }
        }

        public static CodedToken GetCodedToken(MetadataToken token, CodedTokenType type)
        {
            if (token.RID == 0)
                return new CodedToken();

            MetadataTokenType[] types;
            if (!_schema.TryGetValue(type, out types))
                throw new UndefinedCodedTokenSchemaException("Schema for coded token: {0} is undefined", type);

            int index = Array.IndexOf(types, token.Type);
            if (index == -1)
                throw new UndefinedCodedTokenSchemaException("Schema for coded token: {0} and metadata token: {1} is undefined", type, token.Type);

            uint value = token.RID << _sizes[type];
            return new CodedToken(value | (uint)index);
        }

        public static MetadataToken GetMetadataToken(CodedToken token, CodedTokenType type)
        {
            MetadataTokenType[] types;
            if (!_schema.TryGetValue(type, out types))
                throw new UndefinedCodedTokenSchemaException("Schema for coded token: {0} is undefined", type);

            int bits = _sizes[type];
            uint index = 0u;
            uint value = token.Value;

            for (int i = 0; i < bits; i++)
            {
                index |= value & (1u << i);
            }

            MetadataTokenType tokenType = types[index];
            uint rid = (value >> bits);

            return new MetadataToken(tokenType, rid);
        }

        public static bool IsLargeToken(CodedTokenType type, TableHeap heap)
        {
            MetadataTokenType[] types;
            if (!_schema.TryGetValue(type, out types))
                throw new UndefinedCodedTokenSchemaException("Schema for coded token: {0} is undefined", type);

            int bits = _sizes[type];
            int max = 0;

            for (int i = 0; i < types.Length; i++)
            {
                IMetadataTable table;
                int size = heap.TryGetTable(types[i], out table) ? table.Length : 0;
                max = Math.Max(size, max);
            }

            return max >= (1 << (16 - bits));
        }

        private static int GetTagSize(MetadataTokenType[] types)
        {
            return (int)Math.Ceiling(Math.Log(types.Length, 2));
        }
    }
}
