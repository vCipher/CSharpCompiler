using System.IO;
using System.Text;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class TransitionTableBinaryWriter : ITransitionTableWriter
    {
        private BinaryWriter _writer;

        public TransitionTableBinaryWriter(Stream stream)
        {
            _writer = new BinaryWriter(stream, Encoding.UTF8);
        }

        public void Write(TransitionTable table)
        {
            WriteHead(table);
            WriteTransitions(table);
            WriteAliases(table);
        }
        
        private void WriteHead(TransitionTable table)
        {
            _writer.Write(table.Head);
        }

        private void WriteTransitions(TransitionTable table)
        {
            _writer.Write(table.Transitions.Length);

            foreach (var map in table.Transitions)
            {
                _writer.Write(map.Count);
                foreach (var pair in map)
                {
                    _writer.Write(pair.Key);
                    _writer.Write(pair.Value);
                }
            }
        }

        private void WriteAliases(TransitionTable table)
        {
            _writer.Write(table.Aliases.Count);
            
            foreach (var pair in table.Aliases)
            {
                _writer.Write(pair.Key);
                _writer.Write(pair.Value);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _writer?.Dispose();
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
