using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Syntax.Ast.Types
{
    public sealed class TypeRegistry
    {
        private Dictionary<string, AstType> _registry;

        public TypeRegistry()
        {
            _registry = new Dictionary<string, AstType>();
        }

        public void Register(string name, AstType type)
        {
            _registry.Add(name, type);
        }

        public TType Resolve<TType>(string name) where TType : AstType
        {
            AstType type;
            if (_registry.TryGetValue(name, out type))
                return (TType)type;

            return default(TType);
        }
    }
}
