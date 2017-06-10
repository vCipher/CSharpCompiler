using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public static class FieldFactory
    {
        private static Func<FieldDefinition, FieldReference> _getFieldReference
            = Func.Memoize<FieldDefinition, FieldReference>(FieldReferenceResolver.Resolve);

        public static FieldReference GetFieldReference(IFieldInfo Field)
        {
            if (Field is FieldReference) return (FieldReference)Field;
            return _getFieldReference((FieldDefinition)Field);
        }

        private sealed class FieldReferenceResolver : IFieldReferenceResolver
        {
            private FieldDefinition _fieldDef;

            private FieldReferenceResolver(FieldDefinition fieldDef)
            {
                _fieldDef = fieldDef;
            }

            public static FieldReference Resolve(FieldDefinition fieldDef)
            {
                var resolver = new FieldReferenceResolver(fieldDef);
                return new FieldReference(resolver);
            }

            public ITypeInfo GetDeclaringType()
            {
                return TypeFactory.GetTypeReference(_fieldDef.DeclaringType);
            }

            public ITypeInfo GetFieldType()
            {
                return TypeFactory.GetTypeReference(_fieldDef.FieldType);
            }

            public string GetName()
            {
                return _fieldDef.Name;
            }
        }
    }
}
