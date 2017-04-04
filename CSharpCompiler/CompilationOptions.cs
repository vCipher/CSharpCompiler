using System;

namespace CSharpCompiler
{
    public sealed class CompilationOptions
    {
        public uint TimeStamp { get; set; }
        public Guid Mvid { get; set; }
        public string AssemblyName { get; set; }
        public string Namespace { get; set; }
        public string TypeName { get; set; }

        public CompilationOptions()
        {
            TimeStamp = (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            Mvid = Guid.NewGuid();
            AssemblyName = "Target.dll";
            Namespace = "CSharpCompiler";
            TypeName = "Target"; 
        }
    }
}
