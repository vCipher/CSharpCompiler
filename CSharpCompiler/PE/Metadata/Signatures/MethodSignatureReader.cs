using CSharpCompiler.PE.Metadata.Tables;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.PE.Metadata.Signatures
{
    public sealed class MethodSignatureReader
    {
        public CallingConventions CallingConventions { get; private set; }
        public uint ParamCount { get; private set; }
        public ITypeInfo ReturnType { get; private set; }
        public ITypeInfo[] ParamTypes { get; private set; }

        public MethodSignatureReader(MethodRow row, MetadataSystem metadata)
        {
            ReadSignature(row, metadata);
        }

        private void ReadSignature(MethodRow row, MetadataSystem metadata)
        {
            var blob = metadata.ResolveBlob(row.Signature);
            var sig = new StandAloneSignature(blob);

            CallingConventions = (CallingConventions)sig.ReadByte();
            ParamCount = sig.ReadCompressedUInt32();
            ReturnType = sig.ReadType(metadata);

            ParamTypes = new ITypeInfo[ParamCount];
            for (int index = 0; index < ParamCount; index++)
            {
                ParamTypes[index] = sig.ReadType(metadata);
            }
        }
    }
}
