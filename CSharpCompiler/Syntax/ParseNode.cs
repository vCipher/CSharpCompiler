using CSharpCompiler.Lexica.Tokens;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

namespace CSharpCompiler.Syntax
{
    public class ParseNode
    {
        public ParseNodeTag Tag { get; private set; }

        public Token Token { get; private set; }

        public ParseNode Parent { get; private set; }

        public List<ParseNode> Children { get; private set; }

        public bool IsTerminal
        {
            get { return Token != default(Token); }
        }

        public ParseNode(Token terminal)
            : this(ParseNodeTag.Terminal, terminal, null, null)
        { }

        public ParseNode(ParseNodeTag tag, params ParseNode[] children)
            : this(tag, default(Token), null, children.ToList())
        { }

        public ParseNode(ParseNodeTag tag, Token terminal, ParseNode parent, List<ParseNode> children)
        {
            Tag = tag;
            Token = terminal;
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

        public override string ToString()
        {
            var sb = new StringBuilder().AppendFormat("[Tag: {0}", Tag);
            if (IsTerminal) sb.AppendFormat(", Token: {0}", Token);
            if (Children.Any()) sb.AppendFormat(", Children: {0}", string.Join(", ", Children));
            return sb.ToString();
        }
    }
}
