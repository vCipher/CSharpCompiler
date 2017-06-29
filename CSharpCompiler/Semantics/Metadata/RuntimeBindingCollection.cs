using CSharpCompiler.Utility;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class RuntimeBindingCollection<T> : IReadOnlyCollection<T>
    {
        private object _syncLock;
        private Func<Dictionary<RuntimeBindingSignature, T>> _collectionFactory;
        private LazyWrapper<Dictionary<RuntimeBindingSignature, T>> _collection;

        public IReadOnlyDictionary<RuntimeBindingSignature, T> Collection => _collection.GetValue(ref _syncLock, _collectionFactory);
        public int Count => Collection.Count;

        public RuntimeBindingCollection(Func<IReadOnlyCollection<T>> sourceFactory, Func<T, RuntimeBindingSignature> signatureFactory)
        {
            _syncLock = new object();
            _collectionFactory = CreateCollectionFactory(sourceFactory, signatureFactory);
        }

        public T Get(RuntimeBindingSignature signature)
        {
            T memberRef;
            return Collection.TryGetValue(signature, out memberRef) ? memberRef : default(T);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Collection.Values.GetEnumerator();
        }

        private static Func<Dictionary<RuntimeBindingSignature, T>> CreateCollectionFactory(
            Func<IReadOnlyCollection<T>> sourceFactory, 
            Func<T, RuntimeBindingSignature> signatureFactory)
        {
            return () =>
            {
                var source = sourceFactory();
                var collection = new Dictionary<RuntimeBindingSignature, T>(source.Count);

                foreach (var item in source)
                {
                    var signature = signatureFactory(item);
                    collection.Add(signature, item);
                }

                return collection;
            };
        }
    }
}
