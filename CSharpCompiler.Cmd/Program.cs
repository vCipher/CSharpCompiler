using CSharpCompiler.Cmd.Runtimes;
using CSharpCompiler.CodeGen;
using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax;
using CSharpCompiler.Syntax.Ast;
using System;
using System.IO;

namespace CSharpCompiler.Cmd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1) throw new ArgumentException(nameof(args));

            string file = args[0];
            string runtime = args[1];
            string filePath = Path.Combine(AppContext.BaseDirectory, file);

            if (string.IsNullOrWhiteSpace(runtime)) throw new ArgumentException(nameof(runtime));
            if (!File.Exists(filePath)) throw new FileNotFoundException("File to compile not found.", filePath);

            string fileName = Path.GetFileNameWithoutExtension(filePath);
            CompilationOptions options = new CompilationOptions() { AssemblyName = $"{fileName}.dll" };

            TokenEnumerable tokens = Scanner.Scan(File.ReadAllText(filePath));
            ParseTree parseTree = Parser.Parse(tokens);
            SyntaxTree syntaxTree = AstBuilder.Build(parseTree);
            AssemblyDefinition assemblyDef = AssemblyBuilder.Build(syntaxTree, options);

            using (Stream stream = File.Open(Path.Combine(AppContext.BaseDirectory, options.AssemblyName), FileMode.Create))
            using (PEWriter pe = new PEWriter(assemblyDef, stream, options))
                pe.Write();

            RuntimeManager.WriteManifest(fileName, runtime);
        }
    }
}
