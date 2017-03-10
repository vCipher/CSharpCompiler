using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class VarLocation : Expression
    {
        public string VarName { get; private set; }

        public VarLocation(string varName)
        {
            VarName = varName;
        }
    }
}
