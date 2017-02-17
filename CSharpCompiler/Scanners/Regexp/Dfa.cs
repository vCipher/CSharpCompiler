using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Scanners.Regexp
{
    public sealed class Dfa
    {
        public static readonly DfaBuilder Builder = new DfaBuilder();

        public DfaState Head { get; set; }
        
        public Dfa(DfaState head)
        {
            Head = head;
        }
    }
}
