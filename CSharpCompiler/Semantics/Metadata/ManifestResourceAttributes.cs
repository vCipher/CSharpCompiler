using System;

namespace CSharpCompiler.Semantics.Metadata
{
    [Flags]
    public enum ManifestResourceAttributes : uint
    {
        VisibilityMask = 0x0007,
        /// <summary>
        /// The resource is exported from the Assembly
        /// </summary>
        Public = 0x0001,
        /// <summary>
        /// The resource is private to the Assembly
        /// </summary>
        Private = 0x0002
    }
}
