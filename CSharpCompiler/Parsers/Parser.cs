using CSharpCompiler.Scanners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Parsers
{
    public sealed class Parser
    {
        private readonly Scanner _scanner;

        public Parser(Scanner scanner)
        {
            _scanner = scanner;
        }

        public object Parse()
        {
            return null;
        }
    }
}
