using CSharpCompiler.Cmd.Runtimes;
using CSharpCompiler.CodeGen;
using System;
using System.IO;

namespace CSharpCompiler.Cmd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2) throw new ArgumentException(nameof(args));

            string file = args[0];
            string runtime = args[1];
            string filePath = Path.Combine(AppContext.BaseDirectory, file);

            if (string.IsNullOrWhiteSpace(runtime)) throw new ArgumentException(nameof(runtime));
            if (!File.Exists(filePath)) throw new FileNotFoundException("File to compile did not found.", filePath);

            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string content = File.ReadAllText(filePath);

            var options = new CompilationOptions() { AssemblyName = $"{fileName}.dll" };
            var assemblyDef = Compiler.CompileAssembly(content, options);

            using (Stream stream = File.Open(Path.Combine(AppContext.BaseDirectory, options.AssemblyName), FileMode.Create))
            using (PEWriter pe = new PEWriter(assemblyDef, stream, options))
            {
                pe.Write();
            }

            RuntimeManager.WriteManifest(fileName, runtime);
        }
    }
}
