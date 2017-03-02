using CSharpCompiler.Parsers;
using CSharpCompiler.Scanners;
using CSharpCompiler.Scanners.Regexp;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpCompiler.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            //var scanner = new Scanner(TransitionTable.Default);
            //var parser = new Parser(scanner);
            //var parseNode = parser.Parse("int a = 1 + 1;");

            using (var stream = new FileStream("vocabulary.bin", FileMode.OpenOrCreate))
                new BinaryFormatter().Serialize(stream, TransitionTable.Default);
        }
    }
}
