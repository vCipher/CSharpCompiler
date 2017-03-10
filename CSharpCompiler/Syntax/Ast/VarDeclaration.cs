using CSharpCompiler.Syntax.Ast.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Syntax.Ast
{
    public sealed class VarDeclaration : AstNode
    {
        public AstType Type { get; private set; }

        public List<VarDeclarator> Declarators { get; private set; }

        /// <summary>
        /// Create a local variable declaration with the implicit typification.
        /// </summary>
        public VarDeclaration(IEnumerable<VarDeclarator> declarators)
        {
            Type = null;
            Declarators = new List<VarDeclarator>(declarators);
        }

        /// <summary>
        /// Create a local variable declaration with the implicit typification.
        /// </summary>
        public VarDeclaration(params VarDeclarator[] declarators)
        {
            Type = null;
            Declarators = new List<VarDeclarator>(declarators);
        }

        /// <summary>
        /// Create a local variable declaration with the explicit typification.
        /// </summary>
        public VarDeclaration(AstType type, IEnumerable<VarDeclarator> declarators)
        {
            Type = type;
            Declarators = new List<VarDeclarator>(declarators);
        }

        /// <summary>
        /// Create a local variable declaration with the explicit typification.
        /// </summary>
        public VarDeclaration(AstType type, params VarDeclarator[] declarators)
        {
            Type = type;
            Declarators = new List<VarDeclarator>(declarators);
        }
    }
}
