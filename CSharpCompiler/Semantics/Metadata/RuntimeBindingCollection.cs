using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class RuntimeBindingCollection<T> : IReadOnlyCollection<T>
    {
        private Lazy<Dictionary<RuntimeBindingSignature, T>> _collection;
        public int Count => _collection.Value.Count;

        public RuntimeBindingCollection(Func<IReadOnlyCollection<T>> sourceFactory, Func<T, RuntimeBindingSignature> signatureFactory)
        {
            var collectionFactory = CreateCollectionFactory(sourceFactory, signatureFactory);
            _collection = new Lazy<Dictionary<RuntimeBindingSignature, T>>(collectionFactory);
        }

        public T Get(RuntimeBindingSignature signature)
        {
            T memberRef;
            return _collection.Value.TryGetValue(signature, out memberRef) ? memberRef : default(T);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.Value.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.Value.Values.GetEnumerator();
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
