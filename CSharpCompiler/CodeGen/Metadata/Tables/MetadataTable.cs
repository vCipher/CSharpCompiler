using CSharpCompiler.CodeGen.Metadata.Heaps;
using CSharpCompiler.Semantics.Metadata;
using System.Collections.Generic;
using System.Collections;

namespace CSharpCompiler.CodeGen.Metadata.Tables
{
    public abstract class MetadataTable<TEntity, TRow> : IMetadataTable
        where TEntity : IMetadataEntity
        where TRow : struct
    {
        private List<TEntity> _entities;
        private List<TRow> _rows;

        public ushort Position
        {
            get { return (ushort)(_rows.Count + 1); }
        }

        public int Length
        {
            get { return _rows.Count; }
        }

        public MetadataTable()
        {
            _entities = new List<TEntity>();
            _rows = new List<TRow>();
        }

        public uint Add(TEntity entity, TRow row)
        {
            _entities.Add(entity);
            _rows.Add(row);
            return (uint)_rows.Count;
        }

        public bool TryGetToken(TEntity entity, out MetadataToken token)
        {
            uint rid = (uint)(_entities.IndexOf(entity) + 1);
            token = new MetadataToken(GetTokenType(), rid);
            return rid != 0;
        }

        public virtual MetadataToken GetToken(TEntity entity)
        {
            uint rid = (uint)(_entities.IndexOf(entity) + 1);
            return new MetadataToken(GetTokenType(), rid);
        }

        public virtual void Write(TableHeap heap)
        {
            foreach (TRow row in _rows)
                heap.WriteStruct(row);
        }

        public IEnumerator GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        protected abstract MetadataTokenType GetTokenType();
    }
}