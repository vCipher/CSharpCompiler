using CSharpCompiler.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class TransitionTableWriter : IDisposable
    {
        private TextWriter _writer;

        public TransitionTableWriter(Stream stream)
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
            return string.Format("int head = {0};", table.Head);
        }

        private string GetTransitionsSourceCode(TransitionTable table)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var transitions = new Dictionary<int, Dictionary<char, int>>");
            sb.AppendLine("{");
            table.Transitions.ForEach((from, info) => AppendTransitionsSourceCode(sb, from.Key, from.Value, info));
            sb.AppendLine("};");

            return sb.ToString();
        }

        private void AppendTransitionsSourceCode(StringBuilder sb, int from, Dictionary<char, int> states, EnumerationInfo info)
        {
            sb.AppendFormat("[{0}] = new Dictionary<char, int>", from);
            sb.Append("{");
            states.ForEach((state, innerInfo) => AppendTransiotionSourceCode(sb, state.Key, state.Value, innerInfo));

            if (info.IsLast) sb.AppendLine(" }");
            else sb.AppendLine(" },");
        }

        private void AppendTransiotionSourceCode(StringBuilder sb, char @char, int state, EnumerationInfo info)
        {
            sb.AppendFormat("['{0}'] = {1}", Escape(@char), state);
            if (!info.IsLast) sb.Append(", ");
        }

        private string Escape(char @char)
        {
            return (@char == '\\') ? "\\\\" : new string(@char, 1);
        }

        private string GetAliasesSourceCode(TransitionTable table)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var aliases = new Dictionary<int, string>");
            sb.AppendLine("{");
            table.Aliases.ForEach((alias, info) => AppendAliasSourceCode(sb, alias.Key, alias.Value, info));
            sb.AppendLine("};");

            return sb.ToString();
        }

        private void AppendAliasSourceCode(StringBuilder sb, int state, string alias, EnumerationInfo info)
        {
            sb.AppendFormat("[{0}] = \"{1}\"", state, alias);
            if (!info.IsLast) sb.Append(", ");
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
