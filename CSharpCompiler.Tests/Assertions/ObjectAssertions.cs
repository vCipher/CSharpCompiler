using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CSharpCompiler.Tests.Assertions
{
    public class ObjectAssertions<T>
    {
        protected T actual;

        public ObjectAssertions(T actual)
        {
            this.actual = actual;
        }

        public void Be(T expected)
        {
            Be(expected, null);
        }

        public virtual void Be(T expected, ITestOutputHelper output)
        {
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

            var actualJson = JsonConvert.SerializeObject(actual, settings);
            var expectedJson = JsonConvert.SerializeObject(expected, settings);

            if (!string.Equals(expectedJson, actualJson))
            {
                output?.WriteLine("Expected:\n {0}\n", expectedJson);
                output?.WriteLine("Actual:\n {0}\n", actualJson);
                throw new EqualException(expected, actual);
            }
        }
    }
}
