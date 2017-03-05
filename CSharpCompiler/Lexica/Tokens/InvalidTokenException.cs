using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Lexica.Tokens
{
    [Serializable]
    public class InvalidTokenException : ScanException
    {
        public InvalidTokenException(string alias)
            : base(string.Format("Unsupported token {0}", alias))
        { }
    }
}
