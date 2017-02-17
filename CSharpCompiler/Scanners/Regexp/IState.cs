using System.Collections.Generic;

namespace CSharpCompiler.Scanners.Regexp
{
    public interface IState
    {
        int Id { get; }

        bool IsAccepting { get; }

        string Alias { get; }

        ISet<Transition> Transitions { get; }        
    }
}
