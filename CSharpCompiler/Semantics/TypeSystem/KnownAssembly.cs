using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public static class KnownAssembly
    {
        public static AssemblyReference System_CorLib
            = new AssemblyReference("System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e");

        public static AssemblyReference System_Console
            = new AssemblyReference("System.Console, Version=4.0.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
    }
}
