using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class TransitionTableBinaryReader : ITransitionTableBinaryReader
    {
        private BinaryReader _reader;

        public TransitionTableBinaryReader(Stream stream)
        {
            _reader = new BinaryReader(stream, Encoding.UTF8);
        }

        public TransitionTable Read()
        {
            var head = ReadHead();
            var transitions = ReadTransitions();
            var aliases = ReadAliases();
            return new TransitionTable(head, transitions, aliases);
        }

        private ushort ReadHead()
        {
            return _reader.ReadUInt16();
        }

        private Dictionary<char, ushort>[] ReadTransitions()
        {
            var transitionsLength = _reader.ReadInt32();
            var transitions = new Dictionary<char, ushort>[transitionsLength];

            for (int index = 0; index < transitionsLength; index++)
            {
                var mapLength = _reader.ReadInt32();
                var map = new Dictionary<char, ushort>(mapLength);

                for (int counter = 0; counter < mapLength; counter++)
                {
                    map.Add(_reader.ReadChar(), _reader.ReadUInt16());
                }

                transitions[index] = map;
            }

            return transitions;
        }

        private Dictionary<ushort, string> ReadAliases()
        {
            var length = _reader.ReadInt32();
            var aliases = new Dictionary<ushort, string>(length);

            for (int counter = 0; counter < length; counter++)
            {
                aliases.Add(_reader.ReadUInt16(), _reader.ReadString());
            }

            return aliases;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls        

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _reader?.Dispose();
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
