using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public sealed class TypeRegistry
    {
        private ConcurrentDictionary<string, TypeRegistry> _registries;
        private ConcurrentDictionary<string, TypeDefinition> _types;

        public TypeRegistry()
        {
            _registries = new ConcurrentDictionary<string, TypeRegistry>();
            _types = new ConcurrentDictionary<string, TypeDefinition>();
        }

        public void Register(TypeDefinition type)
        {
            var parts = string.IsNullOrEmpty(type.Namespace)
                ? Empty<string>.Array
                : type.Namespace.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            var registry = this;
            foreach (var part in parts)
            {
                registry = registry._registries.GetOrAdd(part, _ => new TypeRegistry());
            }

            registry.ThrowIfTypeConflictExists(type);
            registry._types[type.Name] = type;
        }

        public bool TrySearch(Stack<string> parts, out TypeDefinition type)
        {
            var regstry = this;
            while (parts.Any())
            {
                var part = parts.Pop();

                TypeRegistry tmp;
                if (regstry._registries.TryGetValue(part, out tmp))
                {
                    regstry = tmp;
                    continue;
                }
                
                if (regstry._types.TryGetValue(part, out type))
                {
                    return true;
                }
            }

            type = null;
            return false;
        }

        private void ThrowIfTypeConflictExists(TypeDefinition type)
        {
            TypeRegistry registry;
            if (_registries.TryGetValue(type.Name, out registry))
                throw new SemanticException("Type conflict occures: {0} and {1}", type, registry);

            TypeDefinition otherType;
            if (_types.TryGetValue(type.Name, out otherType))
                throw new SemanticException("Type conflict occures: {0} and {1}", type, otherType);
        }
    }
}
