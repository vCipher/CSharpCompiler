using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Lexica
{
    [Serializable]
    public class NotAcceptLexemeException : ScanException
    {
        public NotAcceptLexemeException(string lexeme) : base(string.Format("Scanner doesn't accept lexeme: {0}", lexeme))
        { }
    }
}
