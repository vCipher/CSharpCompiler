using System.Collections.Generic;

namespace CSharpCompiler.Lexica.Regexp
{
    public interface ITransitionState<TState> : IState where TState : IState
    {
        ISet<Transition<TState>> Transitions { get; }
    }
}
