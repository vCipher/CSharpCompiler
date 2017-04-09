using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class SingatureBuilder : ByteBuffer
    {
        public static SingatureBuilder GetMethodSignature(IMethodInfo methodInfo)
        {
            SingatureBuilder signature = new SingatureBuilder();
            signature.WriteMethod(methodInfo);
            return signature;
        }

        public static SingatureBuilder GetAttributeSignature(CustomAttribute attribute)
        {
            SingatureBuilder signature = new SingatureBuilder();
            signature.WriteCustomAttribute(attribute);
            return signature;
        }

        public static SingatureBuilder GetVariablesSignature(Collection<VariableDefinition> variables)
        {
            SingatureBuilder signature = new SingatureBuilder();
            signature.WriteVariablesSignature(variables);
            return signature;
        }

        public SingatureBuilder WriteMethod(IMethodInfo methodInfo)
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

            return this;
        }

        public SingatureBuilder WriteCustomAttribute(CustomAttribute attribute)
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

            return this;
        }

        public SingatureBuilder WriteVariablesSignature(Collection<VariableDefinition> variables)
        {
            WriteByte(0x7);
            WriteCompressedUInt32((uint)variables.Count);

            foreach (VariableDefinition variable in variables)
                WriteTypeSignature(variable.Type);

            return this;
        }

        public SingatureBuilder WriteTypeSignature(ITypeInfo typeInfo)
        {
            return WriteTypeSignature(typeInfo.DeclaringType);
        }

        public SingatureBuilder WriteTypeSignature(IType type)
        {
            WriteByte((byte)type.ElementType);
            return this;
        }
    }
}
