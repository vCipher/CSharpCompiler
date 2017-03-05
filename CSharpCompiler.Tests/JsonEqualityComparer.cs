using CSharpCompiler.Lexica;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Tests
{
    public sealed class JsonEqualityComparer<T> : IEqualityComparer<T>
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            Formatting = Formatting.Indented
        };

        public bool Equals(T x, T y)
        {
            return string.Equals(
                JsonConvert.SerializeObject(x, Settings),
                JsonConvert.SerializeObject(y, Settings)
            );
        }

        public int GetHashCode(T obj)
        {
            return JsonConvert.SerializeObject(obj, Settings).GetHashCode();
        }
    }
}
