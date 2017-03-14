using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Syntax.Ast
{
    [Serializable]
    public class DublicateVariableDeclarationException : SyntaxException
    {
        public DublicateVariableDeclarationException(string name) 
            : base("Variable: {0}, is already defined", name)
        { }
    }
}
