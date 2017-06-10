using System;

namespace CSharpCompiler.Semantics.Metadata
{
    [Flags]
    public enum EventAttributes : ushort
    {
        None = 0x0000,
        /// <summary>
        /// Event is special
        /// </summary>
        SpecialName = 0x0200,
        /// <summary>
        /// CLI provides 'special' behavior, depending upon the name of the event
        /// </summary>
        RTSpecialName = 0x0400
    }
}
