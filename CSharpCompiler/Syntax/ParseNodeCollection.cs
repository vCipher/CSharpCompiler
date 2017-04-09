using CSharpCompiler.Lexica.Tokens;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace CSharpCompiler.Syntax
{
    public sealed class ParseNodeCollection : Collection<ParseNode>
    {
        private int _index;

        public ParseNodeCollection(IEnumerable<ParseNode> children) : base(new List<ParseNode>(children))
        {
            _index = 0;
        }

        public bool Check(ParseNodeTag tag)
        {
            return (_index >= 0 && _index < Items.Count)
                && Items[_index].Tag == tag;
        }

        public bool Check(TokenTag tag)
        {
            return (_index >= 0 && _index < Items.Count)
                && Items[_index].Token.Tag == tag;
        }

        public ParseNodeCollection Skip(TokenTag tag)
        {
            ParseNode node = Items[_index++];
            if (!node.IsTerminal || node.Token.Tag != tag)
                throw new SyntaxException("Unexpected parse node: {0} (expected: {1})", node.Tag, tag);

            return this;
        }

        public ParseNode GetAndMove(ParseNodeTag tag)
        {
            ParseNode node = Items[_index++];
            if (node.Tag != tag)
                throw new SyntaxException("Unexpected parse node: {0} (expected: {1})", node.Tag, tag);

            return node;
        }

        public T GetAndMove<T>(ParseNodeTag tag, Func<ParseNode, T> factory)
        {
            ParseNode node = Items[_index++];
            if (node.Tag != tag)
                throw new SyntaxException("Unexpected parse node: {0} (expected: {1})", node.Tag, tag);

            return factory(node);
        }
    }
}
