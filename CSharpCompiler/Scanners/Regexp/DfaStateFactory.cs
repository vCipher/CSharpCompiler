using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Scanners.Regexp
{
    public sealed class DfaStateFactory
    {
        private int _counter;

        public DfaStateFactory()
        {
            _counter = 0;
        }

        public DfaStateFactory(DfaStateFactory factory)
        {
            _counter = factory._counter;
        }

        public DfaState Create(IEnumerable<NfaState> nfaStates)
        {
            return new DfaState(_counter++, nfaStates);
        }

        public DfaState CreateEmpty()
        {
            return new DfaState(-1, Enumerable.Empty<NfaState>());
        }
    }
}
