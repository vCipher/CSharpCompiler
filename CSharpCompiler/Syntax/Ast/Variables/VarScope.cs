using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast.Variables
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
