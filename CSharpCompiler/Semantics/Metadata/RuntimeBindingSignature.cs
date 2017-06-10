using CSharpCompiler.Utility;
using System;
using System.Linq;
using System.Text;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class RuntimeBindingSignature : ByteBuffer
    {
        private const int FIELD_SIG = 0x06;
        
        public static RuntimeBindingSignature GetMethodSignature(IMethodInfo method)
        {
            var signature = new RuntimeBindingSignature();
            signature.WriteMethod(method);
            return signature;
        }

        public static RuntimeBindingSignature GetMethodSignature(string name, CallingConventions conventions, params ITypeInfo[] types)
        {
            var signature = new RuntimeBindingSignature();
            signature.WriteMethod(name, conventions, types);
            return signature;
        }

        public static RuntimeBindingSignature GetFieldSignature(IFieldInfo field)
        {
            var signature = new RuntimeBindingSignature();
            signature.WriteField(field);
            return signature;
        }

        public static RuntimeBindingSignature GetFieldSignature(string name)
        {
            var signature = new RuntimeBindingSignature();
            signature.WriteField(name);
            return signature;
        }

        public static RuntimeBindingSignature GetTypeSignature(ITypeInfo type)
        {
            var signature = new RuntimeBindingSignature();
            signature.WriteType(type);
            return signature;
        }

        public static RuntimeBindingSignature GetTypeSignature(string name, string @namespace, IAssemblyInfo assembly)
        {
            var signature = new RuntimeBindingSignature();
            signature.WriteType(name, @namespace, assembly);
            return signature;
        }

        public static RuntimeBindingSignature GetAttributeSignature(CustomAttribute attribute)
        {
            var signature = new RuntimeBindingSignature();
            signature.WriteCustomAttribute(attribute);
            return signature;
        }

        private void WriteMethod(IMethodInfo method)
        {
            int paramCount = method.Parameters.EmptyIfNull().Count();

            WriteBytes(Encoding.UTF8.GetBytes(method.Name));
            WriteByte((byte)method.CallingConventions);
            WriteCompressedUInt32((uint)paramCount);

            foreach (var parameter in method.Parameters.EmptyIfNull())
            {
                WriteType(parameter.Type);
            }
        }

        private void WriteMethod(string name, CallingConventions conventions, params ITypeInfo[] types)
        {
            WriteBytes(Encoding.UTF8.GetBytes(name));
            WriteByte((byte)conventions);
            WriteCompressedUInt32((uint)types.Length);

            foreach (var type in types)
            {
                WriteType(type);
            }
        }

        private void WriteField(IFieldInfo field)
        {
            WriteByte(FIELD_SIG);
            WriteBytes(Encoding.UTF8.GetBytes(field.Name));
        }

        private void WriteField(string name)
        {
            WriteByte(FIELD_SIG);
            WriteBytes(Encoding.UTF8.GetBytes(name));
        }

        private void WriteType(ITypeInfo type)
        {
            var @string = string.Format("{0}.{1}, {2}", type.Namespace, type.Name, type.Assembly);
            WriteBytes(Encoding.UTF8.GetBytes(@string));
        }

        private void WriteType(string name, string @namespace, IAssemblyInfo assembly)
        {
            var @string = string.Format("{0}.{1}, {2}", @namespace, name, assembly);
            WriteBytes(Encoding.UTF8.GetBytes(@string));
        }

        private void WriteCustomAttribute(CustomAttribute attribute)
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
    }
}
