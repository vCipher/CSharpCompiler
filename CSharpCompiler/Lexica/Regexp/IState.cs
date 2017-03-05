using System.Collections.Generic;

namespace CSharpCompiler.Lexica.Regexp
{
    public interface IState
    {
        int Id { get; }

        bool IsAccepting { get; }

        string Alias { get; }
    }
}
