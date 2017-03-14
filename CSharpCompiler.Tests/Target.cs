using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CSharpCompiler
{
    public class Target
    {
        public static void Main()
        {
            int a = 13;
            int b = 31;
            int x = (a % 2) ^ b;
            Console.WriteLine(x);
        }
    }
}
