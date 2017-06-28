using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.PE.Metadata.Tokens;

namespace CSharpCompiler.PE.Metadata
{
    public static class ExtensionMethods
    {
        public static MetadataTableType ToTableType(this MetadataTokenType tokenType)
        {
            return (MetadataTableType)(byte)((uint)tokenType >> 24);
        }

        public static MetadataTokenType ToTokenType(this MetadataTableType tableType)
        {
            return (MetadataTokenType)((uint)tableType << 24);
        }
    }
}
