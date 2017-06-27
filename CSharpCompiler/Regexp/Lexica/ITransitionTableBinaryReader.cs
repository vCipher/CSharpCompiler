using System;

namespace CSharpCompiler.Lexica.Regexp
{
    public interface ITransitionTableBinaryReader : IDisposable
    {
        TransitionTable Read();
    }
}