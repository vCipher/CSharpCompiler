using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Syntax
{
    public sealed class ParseTree : ParseNode
    {
        public ParseTree(params ParseNode[] children) : base(ParseNodeTag.Program, children)
        { }
    }
}
