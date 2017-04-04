using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit.Sdk;

namespace CSharpCompiler.Tests.Data
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class FileDataAttribute : DataAttribute
    {
        private static readonly Regex TEST_PATTERN = new Regex(@"# test.*", RegexOptions.IgnoreCase);
        private static readonly Regex PARAM_PATTERN = new Regex(@"## param.*", RegexOptions.IgnoreCase);

        public string File { get; private set; }

        public FileDataAttribute(string file)
        {
            File = Path.Combine(AppContext.BaseDirectory, file);
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (!System.IO.File.Exists(File)) throw new FileNotFoundException("Test data not found", "File");

            IDataConverter[] converters = testMethod.GetParameters()
                .Select(param => DataConverterFactory.Create(param.ParameterType))
                .ToArray();

            string text = System.IO.File.ReadAllText(File);
            if (string.IsNullOrWhiteSpace(text))
                yield break;

            string[] contents = TEST_PATTERN.Split(text)
                .Select(content => content.Trim('\r', '\n'))
                .Where(content => !string.IsNullOrWhiteSpace(content))
                .ToArray();

            foreach (string content in contents)
            {
                string[] parameters = PARAM_PATTERN.Split(content)
                    .Select(section => section.Trim('\r', '\n'))
                    .Where(section => !string.IsNullOrWhiteSpace(section))
                    .ToArray();

                if (parameters.Length != converters.Length)
                    throw new ArgumentOutOfRangeException("Test parameters count different from test arguments count");

                object[] result = new object[parameters.Length];
                for (int index = 0; index < parameters.Length; index++)
                {
                    string param = parameters[index];
                    IDataConverter provider = converters[index];
                    result[index] = provider.Convert(param);
                }

                yield return result;
            }
        }
    }
}
