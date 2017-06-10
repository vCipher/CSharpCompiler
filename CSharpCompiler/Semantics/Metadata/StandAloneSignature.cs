using CSharpCompiler.PE.Metadata;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class StandAloneSignature : ByteBuffer, IMetadataEntity
    {
        private const int FIELD_SIG = 0x06;
        private const int LOCAL_SIG = 0x07;

        public StandAloneSignature() : base() { }
        public StandAloneSignature(byte[] buffer) : base(buffer) { MoveTo(START_POSITION); }
        public StandAloneSignature(ByteBuffer buffer) : base(buffer) { MoveTo(START_POSITION); }

        public static StandAloneSignature GetMethodSignature(IMethodInfo method, MetadataBuilder metadata)
        {
            return new StandAloneSignature()
                .WriteMethod(method, metadata);
        }

        public static StandAloneSignature GetFieldSignature(IFieldInfo field, MetadataBuilder metadata)
        {
            var sig = new StandAloneSignature();
            sig.WriteField(field, metadata);
            return sig;
        }

        public static StandAloneSignature GetVariablesSignature(Collection<VariableDefinition> variables, MetadataBuilder metadata)
        {
            var sig = new StandAloneSignature();
            sig.WriteVariables(variables, metadata);
            return sig;
        }

        public static StandAloneSignature GetAttributeSignature(CustomAttribute attribute, MetadataBuilder metadata)
        {
            var sig = new StandAloneSignature();
            sig.WriteCustomAttribute(attribute, metadata);
            return sig;
        }

        public static StandAloneSignature GetTypeSignature(ITypeInfo type, MetadataBuilder metadata)
        {
            var sig = new StandAloneSignature();
            sig.WriteType(type, metadata);
            return sig;
        }

        public static StandAloneSignature GetMemberReferenceSignature(IMemberReference memberRef, MetadataBuilder metadata)
        {
            if (memberRef is MethodReference) return GetMethodSignature((MethodReference)memberRef, metadata);
            if (memberRef is FieldReference) return GetFieldSignature((FieldReference)memberRef, metadata);
            throw new InvalidOperationException("Can't compute signature for member reference of type: " + memberRef.GetType());
        }

        public void Accept(IMetadataEntityVisitor visitor)
        {
            visitor.VisitStandAloneSignature(this);
        }

        public bool Equals(IMetadataEntity other)
        {
            return (other is StandAloneSignature) && base.Equals((StandAloneSignature)other);
        }

        private StandAloneSignature WriteMethod(IMethodInfo method, MetadataBuilder metadata)
        {
            var conventions = method.CallingConventions;
            int paramCount = method.Parameters.EmptyIfNull().Count();

            WriteByte((byte)conventions);
            WriteCompressedUInt32((uint)paramCount);
            WriteType(method.ReturnType, metadata);

            foreach (var parameter in method.Parameters.EmptyIfNull())
            {
                WriteType(parameter.Type, metadata);
            }

            return this;
        }

        public ITypeInfo ReadType(MetadataSystem metadata)
        {
            var elementType = ReadElementType();
            var typeCode = KnownType.GetTypeCode(elementType);

            if (typeCode != KnownTypeCode.None)
                return KnownType.GetType(typeCode);

            switch (elementType)
            {
                case ElementType.ValueType: return ReadValueType(metadata);
                case ElementType.Class: return ReadClassType(metadata);
                case ElementType.ByRef: return ReadByReferenceType(metadata);
                case ElementType.SizeArray: return ReadVectorType(metadata);
                case ElementType.Array: return ReadArrayType(metadata);
                case ElementType.Var: return ReadTypeGenericParameter(metadata);
                case ElementType.MVar: return ReadMethodGenericParameter(metadata);
                default: throw new NotSupportedException();
            }
        }

        private ByReferenceType ReadByReferenceType(MetadataSystem metadata)
        {
            var containedType = ReadType(metadata);
            return new ByReferenceType(containedType);
        }

        private ArrayType ReadArrayType(MetadataSystem metadata)
        {
            // todo: implement multidimensional array initialization
            throw new NotImplementedException();
        }

        private ArrayType ReadVectorType(MetadataSystem metadata)
        {
            var containedType = ReadType(metadata);
            return new ArrayType(containedType);
        }

        private GenericParameter ReadMethodGenericParameter(MetadataSystem metadata)
        {
            // todo: implement generic parameter initialization
            return new GenericParameter(
                (int)ReadCompressedUInt32(),
                ElementType.MVar);
        }

        private GenericParameter ReadTypeGenericParameter(MetadataSystem metadata)
        {
            // todo: implement generic parameter instantiation
            return new GenericParameter(
                (int)ReadCompressedUInt32(),
                ElementType.Var);
        }

        private ITypeInfo ReadValueType(MetadataSystem metadata)
        {
            var token = ReadTypeToken();
            return metadata.GetTypeInfo(token);
        }

        private ITypeInfo ReadClassType(MetadataSystem metadata)
        {
            var token = ReadTypeToken();
            return metadata.GetTypeInfo(token);
        }

        private MetadataToken ReadTypeToken()
        {
            var codedToken = new CodedToken(ReadCompressedUInt32());
            return CodedTokenSchema.GetMetadataToken(codedToken, CodedTokenType.TypeDefOrRef);
        }

        private ElementType ReadElementType()
        {
            return (ElementType)ReadByte();
        }

        public void WriteField(IFieldInfo field, MetadataBuilder metadata)
        {
            WriteByte(FIELD_SIG);
            WriteType(field.FieldType, metadata);
        }

        public void WriteCustomAttribute(CustomAttribute attribute, MetadataBuilder metadata)
        {
            // todo: implement custom argument signature
            throw new NotImplementedException();
        }

        public void WriteVariables(Collection<VariableDefinition> variables, MetadataBuilder metadata)
        {
            WriteByte(LOCAL_SIG);
            WriteCompressedUInt32((uint)variables.Count);

            foreach (VariableDefinition variable in variables)
            {
                WriteType(variable.Type, metadata);
            }
        }

        public void WriteType(ITypeInfo type, MetadataBuilder metadata)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            switch (type.ElementType)
            {
                case ElementType.SizeArray:
                    WriteArrayType((ArrayType)type, metadata);
                    break;

                case ElementType.Class:
                    WriteClassType(type, metadata);
                    break;

                default:
                    WriteElementType(type.ElementType);
                    break;
            }
        }

        private void WriteArrayType(ArrayType type, MetadataBuilder metadata)
        {
            WriteElementType(type.ElementType);
            WriteType(type.ContainedType, metadata);
        }

        private void WriteClassType(ITypeInfo type, MetadataBuilder metadata)
        {
            WriteElementType(type.ElementType);
            WriteTypeToken(type, metadata);
        }

        private void WriteElementType(ElementType elementType)
        {
            WriteByte((byte)elementType);
        }

        private void WriteTypeToken(ITypeInfo type, MetadataBuilder metadata)
        {
            var token = metadata.ResolveToken(type);
            var codedToken = CodedTokenSchema.GetCodedToken(token, CodedTokenType.TypeDefOrRef);
            WriteCompressedUInt32(codedToken.Value);
        }
    }
}
