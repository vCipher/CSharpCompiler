using CSharpCompiler.Utility;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class TransitionTableSourceWriter : ITransitionTableWriter
    {
        private TextWriter _writer;

        public TransitionTableSourceWriter(Stream stream)
        {
            _writer = new StreamWriter(stream);
        }

        public void Write(TransitionTable table)
        {
            _writer.WriteLine("using System.Collections.Generic;");
            _writer.WriteLine();
            _writer.WriteLine("namespace CSharpCompiler.Lexica.Regexp");
            _writer.WriteLine("{");
            _writer.WriteLine("public static class TransitionTableSource");
            _writer.WriteLine("{");
            _writer.WriteLine("public static TransitionTable GetTransitionTable()");
            _writer.WriteLine("{");

            _writer.WriteLine(GetHeadSourceCode(table));
            _writer.WriteLine(GetTransitionsSourceCode(table));
            _writer.WriteLine(GetAliasesSourceCode(table));
            _writer.WriteLine("return new TransitionTable(head, transitions, aliases);");

            _writer.WriteLine("}");
            _writer.WriteLine("}");
            _writer.WriteLine("}");
        }

        private string GetHeadSourceCode(TransitionTable table)
        {
            return string.Format("ushort head = {0};", table.Head);
        }

        private string GetTransitionsSourceCode(TransitionTable table)
        {
            var length = table.Transitions.Length;
            var sb = new StringBuilder();
            sb.AppendFormat("var transitions = new Dictionary<char, ushort>[{0}];", length);
            sb.AppendLine();
            
            for (ushort index = 0; index < length; index++)
            {
                var states = table.Transitions[index];
                AppendTransitionsSourceCode(sb, index, states);
            }

            return sb.ToString();
        }

        private void AppendTransitionsSourceCode(StringBuilder sb, ushort from, Dictionary<char, ushort> states)
        {
            if (states.Count == 0)
            {
                sb.AppendFormat("transitions[{0}] = new Dictionary<char, ushort>();", from);
                sb.AppendLine();
                return;
            }

            sb.AppendFormat("transitions[{0}] = new Dictionary<char, ushort>({1}) ", from, states.Count);
            sb.Append("{");

            states.ForEach((state, info) =>
            {
                sb.AppendFormat("['{0}'] = {1}", Escape(state.Key), state.Value);
                if (!info.IsLast) sb.Append(", ");
            });

            sb.AppendLine(" };");
        }

        private string Escape(char @char)
        {
            switch (@char)
            {
                case '\\': return "\\\\";
                case '\n': return "\\n";
                case '\r': return "\\r";
                case '\t': return "\\t";
                default: return new string(@char, 1);
            }
        }

        private string GetAliasesSourceCode(TransitionTable table)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("var aliases = new Dictionary<ushort, string>({0});", table.Aliases.Count);
            sb.AppendLine();
            
            foreach (var alias in table.Aliases)
            {
                sb.AppendFormat("aliases[{0}] = \"{1}\";", alias.Key, alias.Value);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _writer.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
