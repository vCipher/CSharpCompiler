using CSharpCompiler.Syntax;
using CSharpCompiler.Lexica;
using CSharpCompiler.Lexica.Regexp;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpCompiler.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = new FileStream("vocabulary.bin", FileMode.OpenOrCreate))
                new BinaryFormatter().Serialize(stream, TransitionTable.Default);
        }
    }
}
