using CSharpCompiler.PE.Metadata.Heaps;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.PE.Metadata.Tables
{
    public abstract class OneRowMetadataTable<TRow> : MetadataTable<TRow>
    {
        public TRow Row => this[1u];

        public OneRowMetadataTable() : base() { }
        public OneRowMetadataTable(int count) : base(count) { }
    }

    public abstract class MetadataTable<TRow> : IMetadataTable, IEnumerable<TRow>
    {
        private List<TRow> _rows;
        private int _count;

        public int Length => _count;

        public TRow this[uint rid]
        {
            get => _rows[(int)(rid - 1)];
            set => _rows[(int)(rid - 1)] = value;
        }

        public MetadataTable()
        {
            _rows = new List<TRow>();
            _count = 0;
        }

        public MetadataTable(int count)
        {
            _rows = new List<TRow>(count);
            _count = count;
        }

        public uint Add(TRow row)
        {
            _rows.Add(row);
            _count = Math.Max(_count, _rows.Count);
            return (uint)_rows.Count;
        }

        public void Write(TableHeapWriter writer)
        {
            foreach (TRow row in _rows)
            {
                WriteRow(row, writer);
            }
        }

        public void Read(TableHeapReader reader)
        {
            for (int index = 0; index < Length; index++)
            {
                var row = ReadRow(reader);
                if (index >= _rows.Count)
                    _rows.Add(row);
                else
                    _rows[index] = row;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        IEnumerator<TRow> IEnumerable<TRow>.GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        protected abstract void WriteRow(TRow row, TableHeapWriter writer);
        protected abstract TRow ReadRow(TableHeapReader reader);
    }
}