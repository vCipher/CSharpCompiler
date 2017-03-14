using CSharpCompiler.CodeGen;
using System;
using System.Collections.Generic;

namespace CSharpCompiler.Tests.Data
{
    public static class DataConverterFactory
    {
        private static Dictionary<Type, IDataConverter> _bindings;

        static DataConverterFactory()
        {
            _bindings = new Dictionary<Type, IDataConverter>();
            _bindings.Add(typeof(string), new StringConverter());
            _bindings.Add(typeof(Guid), new GuidConverter());
            _bindings.Add(typeof(ByteBuffer), new ByteBufferConverter<ByteBuffer>());
        }

        public static IDataConverter Create(Type dataType)
        {
            if (dataType == null) throw new ArgumentNullException("dataType");
            if (dataType == typeof(object)) throw new ArgumentException("dataType");

            IDataConverter converter;
            if (_bindings.TryGetValue(dataType, out converter))
                return converter;

            return new ObjectConverter(dataType);
        }
    }
}