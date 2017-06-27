using System;

namespace CSharpCompiler.Lexica.Regexp
{
    public interface ITransitionTableWriter : IDisposable
    {
        void Write(TransitionTable table);
    }
}