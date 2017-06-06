using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class StandAloneSignature : ByteBuffer, IMetadataEntity
    {
        public static StandAloneSignature GetMethodSignature(IMethodInfo methodInfo)
        {
            StandAloneSignature signature = new StandAloneSignature();
            signature.WriteMethod(methodInfo);
            return signature;
        }

        public static StandAloneSignature GetAttributeSignature(CustomAttribute attribute)
        {
            StandAloneSignature signature = new StandAloneSignature();
            signature.WriteCustomAttribute(attribute);
            return signature;
        }

        public static StandAloneSignature GetVariablesSignature(Collection<VariableDefinition> variables)
        {
            StandAloneSignature signature = new StandAloneSignature();
            signature.WriteVariablesSignature(variables);
            return signature;
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitStandAloneSignature(this);
        }

        public StandAloneSignature WriteMethod(IMethodInfo methodInfo)
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

        public StandAloneSignature WriteCustomAttribute(CustomAttribute attribute)
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

        public StandAloneSignature WriteVariablesSignature(Collection<VariableDefinition> variables)
        {
            WriteByte(0x7);
            WriteCompressedUInt32((uint)variables.Count);

            foreach (VariableDefinition variable in variables)
                WriteTypeSignature(variable.Type);

            return this;
        }

        public StandAloneSignature WriteTypeSignature(ITypeInfo typeInfo)
        {
            WriteByte((byte)typeInfo.ElementType);
            return this;
        }
    }
}
