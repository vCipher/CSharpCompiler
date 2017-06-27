using CSharpCompiler.Cmd.Runtimes;
using CSharpCompiler.Compilation;
using CSharpCompiler.PE;
using System;
using System.IO;

namespace CSharpCompiler.Cmd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using (var result = File.OpenWrite("TransitionTableSource.cs"))
            //using (var writer = new Lexica.Regexp.TransitionTableBinaryWriter(result))
            //{
            //    var table = Lexica.Regexp.TransitionTable.FromVocabularyFile("vocabulary.txt");
            //    writer.Write(table);
            //}

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
            using (AssemblyWriter pe = new AssemblyWriter(assemblyDef, stream, options))
            {
                pe.WriteAssembly();
            }

            RuntimeManager.WriteManifest(fileName, runtime);
        }
    }
}
