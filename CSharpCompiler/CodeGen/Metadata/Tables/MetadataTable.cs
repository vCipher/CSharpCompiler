using CSharpCompiler.CodeGen.Metadata.Heaps;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.CodeGen.Metadata.Tables
{
    public abstract class MetadataTable<TRow> : IMetadataTable where TRow : struct
    {
        protected List<Func<TRow>> _bindings;

        public int Length
        {
            get { return _bindings.Count; }
        }

        public ushort Position
        {
            get { return (ushort)(_bindings.Count + 1); }
        }

        public MetadataTable()
        {
            _bindings = new List<Func<TRow>>();
        }

        public uint Add(TRow row)
        {
            _bindings.Add(() => row);
            return (uint)_bindings.Count;
        }

        public uint Add(Func<TRow> binding)
        {
            _bindings.Add(binding);
            return (uint)_bindings.Count;
        }

        public virtual void Write(TableHeap heap)
        {
            foreach (Func<TRow> binding in _bindings)
                heap.WriteStruct(binding());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _bindings.GetEnumerator();
        }
    }
}