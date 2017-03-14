using Newtonsoft.Json;
using System;

namespace CSharpCompiler.Tests.Data
{
    public sealed class ObjectConverter : IDataConverter
    {
        private Type _type;

        public ObjectConverter(Type type)
        {
            _type = type;
        }
        
        public object Convert(string input)
        {
            return JsonConvert.DeserializeObject(input, _type);
        }
    }
}