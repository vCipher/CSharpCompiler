using CSharpCompiler.Lexica.Tokens;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CSharpCompiler.Syntax
{
    public sealed class ParseNode
    {
        public ParseNodeTag Tag { get; set; }

        public Token Terminal { get; set; }

        public ParseNode Parent { get; set; }

        public List<ParseNode> Children { get; set; }

        public ParseNode(Token terminal)
            : this(ParseNodeTag.Terminal, terminal, null, null)
        { }

        public ParseNode(ParseNodeTag tag, params ParseNode[] children)
            : this(tag, default(Token), null, children.ToList())
        { }

        public ParseNode(ParseNodeTag tag, Token terminal, ParseNode parent, List<ParseNode> children)
        {
            Tag = tag;
            Terminal = terminal;
            Parent = parent;
            Children = new List<ParseNode>();
            children.EmptyIfNull().ForEach(AddChild);
        }

        public void AddChild(ParseNode parseNode)
        {
            if (parseNode == null)
                throw new ArgumentNullException("parseNode");

            parseNode.Parent = this;
            Children.Add(parseNode);
        }
    }
}
