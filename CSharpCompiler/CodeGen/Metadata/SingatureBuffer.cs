using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.CodeGen.Metadata
{
    public sealed class SingatureBuffer : ByteBuffer
    {
        public static SingatureBuffer GetMethodSignature(IMethodInfo methodInfo)
        {
            SingatureBuffer signature = new SingatureBuffer();
            signature.WriteMethod(methodInfo);
            return signature;
        }

        public static SingatureBuffer GetAttributeSignature(CustomAttribute attribute)
        {
            SingatureBuffer signature = new SingatureBuffer();
            signature.WriteCustomAttribute(attribute);
            return signature;
        }

        public static SingatureBuffer GetVariablesSignature(Collection<VariableDefinition> variables)
        {
            SingatureBuffer signature = new SingatureBuffer();
            signature.WriteVariablesSignature(variables);
            return signature;
        }

        public void WriteMethod(IMethodInfo methodInfo)
        {
            CallingConventions conventions = methodInfo.CallingConventions;
            int paramCount = methodInfo.Parameters.EmptyIfNull().Count();

            WriteByte((byte)conventions);
            WriteCompressedUInt32((uint)paramCount);
            WriteTypeSignature(methodInfo.ReturnType);

            foreach (ParameterDefinition parameter in methodInfo.Parameters.EmptyIfNull())
            {
                WriteTypeSignature(parameter.Type);
            }
        }

        public void WriteCustomAttribute(CustomAttribute attribute)
        {
            // todo: implement custom argument signature
            switch (attribute.Name)
            {
                case "CompilationRelaxationsAttribute":
                    WriteBytes(new byte[] { 0x01, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00 });
                    break;

                case "RuntimeCompatibilityAttribute":
                    WriteBytes(new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x54, 0x02, 0x16, 0x57,
                        0x72, 0x61, 0x70, 0x4E, 0x6F, 0x6E, 0x45, 0x78,
                        0x63, 0x65, 0x70, 0x74, 0x69, 0x6F, 0x6E, 0x54,
                        0x68, 0x72, 0x6F, 0x77, 0x73, 0x01
                    });
                    break;

                default: throw new NotSupportedException();
            }
        }

        public void WriteVariablesSignature(Collection<VariableDefinition> variables)
        {
            WriteByte(0x7);
            WriteCompressedUInt32((uint)variables.Count);

            foreach (VariableDefinition variable in variables)
                WriteTypeSignature(variable.Type);
        }

        public void WriteTypeSignature(ITypeInfo typeInfo)
        {
            WriteByte((byte)typeInfo.ElementType);
        }
    }
}
