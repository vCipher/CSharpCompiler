using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpCompiler.Syntax
{
    public class ParseNode
    {
        public ParseNodeTag Tag { get; private set; }

        public Token Token { get; private set; }

        public ParseNode Parent { get; private set; }

        public ParseNodeCollection Children { get; private set; }

        public bool IsTerminal
        {
            get { return Token != default(Token); }
        }

        public ParseNode(Token terminal)
            : this(ParseNodeTag.Terminal, terminal, null, Empty<ParseNode>.Array)
        { }

        public ParseNode(ParseNodeTag tag, params ParseNode[] children)
            : this(tag, default(Token), null, children)
        { }

        public ParseNode(ParseNodeTag tag, Token terminal, ParseNode parent, IEnumerable<ParseNode> children)
        {
            Tag = tag;
            Token = terminal;
            Parent = parent;
            Children = new ParseNodeCollection(children);
            Children.ForEach(child => child.Parent = this);
        }

        public ParseNode AddChild(ParseNode parseNode)
        {
            if (parseNode == null) throw new ArgumentNullException("parseNode");

            parseNode.Parent = this;
            Children.Add(parseNode);

            return this;
        }

        public ParseNode Unwrap()
        {
            return Children.Single();
        }

        public ParseNode Wrap(ParseNodeTag tag)
        {
            return new ParseNode(tag, this);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Tag: {0}", Tag);
            if (IsTerminal) sb.AppendFormat(", Token: {0}", Token);

            return sb.ToString();
        }
    }
}
