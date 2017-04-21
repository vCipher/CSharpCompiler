using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System.Linq;

namespace CSharpCompiler.Semantics.Optimization
{
    public static class AssemblyOptimizer
    {
        public static AssemblyDefinition Optimize(AssemblyDefinition assemblyDef)
        {
            assemblyDef.Module.Types
                .SelectMany(type => type.Methods)
                .SelectMany(method => method.Body.Instructions)
                .ForEach(instruction => instruction.Optimize());

            return assemblyDef;
        }
    }
}
