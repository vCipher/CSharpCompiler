using CSharpCompiler.PE.Metadata;
using CSharpCompiler.PE.Metadata.Tokens;
using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System;
using System.Collections.Generic;

namespace CSharpCompiler.PE.Cil
{
    public sealed class ILCodeWriter : ByteBuffer
    {
        private MetadataBuilder _metadata;

        public ILCodeWriter(MetadataBuilder metadata) : base(Empty<byte>.Array, 4)
        {
            _metadata = metadata;
        }

        public uint WriteMethod(MethodDefinition methodDef)
        {
            uint rva = GetBeginMethodRVA();

            WriteHeader(methodDef);
            WriteInstructions(methodDef.Body.Instructions);

            return rva;
        }

        private void WriteHeader(MethodDefinition methodDef)
        {
            if (IsFatHeader(methodDef))
                WriteFatHeader(methodDef);
            else
                WriteTinyHeader(methodDef);
        }

        private void WriteFatHeader(MethodDefinition methodDef)
        {
            WriteStruct(ComputeFatHeader(methodDef));
        }

        private FatMethodHeader ComputeFatHeader(MethodDefinition methodDef)
        {
            return new FatMethodHeader()
            {
                Size = 0x30,
                MaxStack = (ushort)methodDef.Body.MaxStack,
                CodeSize = (uint)methodDef.Body.CodeSize,
                Attributes = GetFatHeaderAttributes(methodDef),
                LocalVarSigTok = _metadata.ResolveToken(
                    StandAloneSignature
                        .GetVariablesSignature(methodDef.Body.Variables, _metadata))
            };
        }

        private static MethodBodyAttributes GetFatHeaderAttributes(MethodDefinition methodDef)
        {
            MethodBodyAttributes attributes = MethodBodyAttributes.FatFormat;
            if (methodDef.Body.InitLocals) attributes |= MethodBodyAttributes.InitLocals;
            if (methodDef.Body.HasExceptionHandlers) attributes |= MethodBodyAttributes.MoreSects;

            return attributes;
        }

        private void WriteTinyHeader(MethodDefinition methodDef)
        {
            WriteByte((byte)((int)MethodBodyAttributes.TinyFormat | (methodDef.Body.CodeSize << 2)));
        }

        private void WriteInstructions(IEnumerable<Instruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                WriteOpCode(instruction.OpCode);
                WriteOperand(instruction);
            }
        }

        private void WriteOpCode(OpCode opCode)
        {
            opCode.WriteToBuffer(this);
        }

        private void WriteOperand(Instruction instruction)
        {
            var opCode = instruction.OpCode;
            var operandType = opCode.OperandType;

            if (operandType == OperandType.InlineNone) return;
            if (instruction.Operand == null) throw new ArgumentException("instruction");

            switch (operandType)
            {
                case OperandType.InlineBrTarget: WriteInlineBrTarget(instruction); return;
                case OperandType.InlineI: WriteInlineI(instruction); return;
                case OperandType.InlineI8: WriteInlineI8(instruction); return;
                case OperandType.InlineR: WriteInlineR(instruction); return;
                case OperandType.InlineSig: WriteInlineSig(instruction); return;
                case OperandType.InlineString: WriteInlineString(instruction); return;
                case OperandType.InlineSwitch: WriteInlineSwitch(instruction); return;
                case OperandType.InlineVar: WriteInlineVar(instruction); return;
                case OperandType.InlineArg: WriteInlineArg(instruction); return;
                case OperandType.ShortInlineBrTarget: WriteShortInlineBrTarget(instruction); return;
                case OperandType.ShortInlineI: WriteShortInlineI(instruction); return;
                case OperandType.ShortInlineR: WriteShortInlineR(instruction); return;
                case OperandType.ShortInlineVar: WriteShortInlineVar(instruction); return;
                case OperandType.ShortInlineArg: WriteShortInlineArg(instruction); return;
                case OperandType.InlineMethod: WriteMetadataToken(instruction); return;
                case OperandType.InlineField: WriteMetadataToken(instruction); return;
                case OperandType.InlineTok: WriteMetadataToken(instruction); return;
                case OperandType.InlineType: WriteMetadataToken(instruction); return;
            }

            throw new ArgumentException("instruction");
        }

        private void WriteShortInlineArg(Instruction instruction)
        {
            throw new NotImplementedException();
        }

        private void WriteShortInlineR(Instruction instruction)
        {
            WriteSingle(Convert.ToSingle(instruction.Operand));
        }

        private void WriteShortInlineI(Instruction instruction)
        {
            WriteByte(Convert.ToByte(instruction.Operand));
        }

        private void WriteShortInlineBrTarget(Instruction instruction)
        {
            var opCode = instruction.OpCode;
            var target = (IInstructionReference)instruction.Operand;
            var currentOffset = (instruction.Offset + opCode.GetSize() + 1);
            
            WriteSByte((sbyte)(target.Offset - currentOffset));
        }

        private void WriteInlineArg(Instruction instruction)
        {
            throw new NotImplementedException();
        }

        private void WriteInlineSwitch(Instruction instruction)
        {
            throw new NotImplementedException();
        }

        private void WriteInlineString(Instruction instruction)
        {
            var rid = RegisterString(instruction);
            var token = new MetadataToken(MetadataTokenType.String, rid);
            WriteStruct(token);
        }

        private void WriteInlineSig(Instruction instruction)
        {
            throw new NotImplementedException();
        }

        private void WriteInlineR(Instruction instruction)
        {
            WriteDouble(Convert.ToDouble(instruction.Operand));
        }

        private void WriteInlineI8(Instruction instruction)
        {
            WriteDouble(Convert.ToInt64(instruction.Operand));
        }

        private void WriteInlineBrTarget(Instruction instruction)
        {
            var opCode = instruction.OpCode;
            var target = (IInstructionReference)instruction.Operand;
            var currentOffset = (instruction.Offset + opCode.GetSize() + 4);
            
            WriteInt32(target.Offset - currentOffset);
        }

        private void WriteInlineI(Instruction instruction)
        {
            WriteInt32(Convert.ToInt32(instruction.Operand));
        }

        private void WriteShortInlineVar(Instruction instruction)
        {
            WriteByte((byte)GetVariableIndex(instruction));
        }

        private void WriteInlineVar(Instruction instruction)
        {
            WriteInt16((short)GetVariableIndex(instruction));
        }

        private void WriteMetadataToken(Instruction instruction)
        {
            var entity = (IMetadataEntity)instruction.Operand;
            WriteStruct(_metadata.ResolveToken(entity));
        }

        private int GetVariableIndex(Instruction instruction)
        {
            return ((VariableDefinition)instruction.Operand).Index;
        }

        private uint RegisterString(Instruction instruction)
        {
            return RegisterString(instruction.Operand as string);
        }

        private uint RegisterString(string @string)
        {
            return _metadata.WriteUserString(@string);
        }

        private uint RegisterBlob(ByteBuffer buffer)
        {
            return _metadata.WriteBlob(buffer);
        }

        private uint GetBeginMethodRVA()
        {
            const uint codeBase = 0x00002050;
            return codeBase + (uint)Position;
        }

        private bool IsFatHeader(MethodDefinition methodDef)
        {
            var body = methodDef.Body;
            return body.CodeSize >= 64
                || body.InitLocals
                || body.HasVariables
                || body.HasExceptionHandlers
                || body.MaxStack > 8;
        }
    }
}
