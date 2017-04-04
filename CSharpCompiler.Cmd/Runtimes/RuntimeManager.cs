using System;
using System.IO;
using System.Reflection;

namespace CSharpCompiler.Cmd.Runtimes
{
    public static class RuntimeManager
    {
        public static void WriteManifest(string assemblyName, string runtime)
        {
            string manifest = GetManifest(runtime);
            string manifestPath = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.runtimeconfig.json");

            using (Stream stream = File.Open(manifestPath, FileMode.OpenOrCreate))
            using (StreamWriter writer = new StreamWriter(stream))
                writer.Write(manifest);
        }

        public static string GetManifest(string runtime)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            string manifestName = GetManifestName(runtime);

            using (Stream stream = assembly.GetManifestResourceStream(manifestName))
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        private static string GetManifestName(string runtime)
        {
            return $"CSharpCompiler.Cmd.Runtimes.{runtime}.runtimeconfig.json";
        }
    }
}
