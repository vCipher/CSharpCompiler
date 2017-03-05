using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Lexica.Regexp
{
    public interface ITransitionState<TState> : IState where TState : IState
    {
        ISet<Transition<TState>> Transitions { get; }
    }
}
