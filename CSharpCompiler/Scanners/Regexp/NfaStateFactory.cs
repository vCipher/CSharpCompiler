using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Scanners.Regexp
{
    public sealed class NfaStateFactory
    {
        private int _counter;

        public NfaStateFactory()
        {
            _counter = 0;
        }

        public NfaStateFactory(NfaStateFactory factory)
        {
            _counter = factory._counter;
        }

        public NfaState Create()
        {
            return new NfaState(_counter++);
        }
    }
}
