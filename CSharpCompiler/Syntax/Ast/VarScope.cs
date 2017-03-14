using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Syntax.Ast.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Syntax.Ast
{
    public sealed class VarScope
    {
        private Dictionary<string, VarDeclaration> _scope;

        public VarScope()
        {
            _scope = new Dictionary<string, VarDeclaration>();
        }

        public bool HasVariable(string name)
        {
            return _scope.ContainsKey(name);
        }

        public void Register(string name, VarDeclaration variable)
        {
            _scope.Add(name, variable);
        }

        public VarDeclaration Resolve(string name)
        {
            VarDeclaration variable;
            if (_scope.TryGetValue(name, out variable))
                return variable;

            throw new UndefinedVariableException(name);
        }

        public bool TryResolve(string name, out VarDeclaration variable)
        {
            return _scope.TryGetValue(name, out variable);
        }
    }
}
