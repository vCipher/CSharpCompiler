using CSharpCompiler.Lexica.Regexp;
using CSharpCompiler.Lexica.Tokens;

namespace CSharpCompiler.Lexica
{
    public sealed class ScannerOptions
    {
        private static readonly TokenTag[] DEFAULT_BLACK_LIST = new[] 
        {
            TokenTag.NEW_LINE,
            TokenTag.WHITE_SPACE,
            TokenTag.LINE_COMMENT,
            TokenTag.MULTI_LINE_COMMENT
        };

        public TokenTag[] BlackList { get; set; }
        public TransitionTable Transitions { get; set; }

        public ScannerOptions()
        {
            BlackList = DEFAULT_BLACK_LIST;
            Transitions = TransitionTable.Default;
        }
    }
}
