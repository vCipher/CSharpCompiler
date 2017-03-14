using CSharpCompiler.CodeGen.Metadata;
using CSharpCompiler.CodeGen.Metadata.Tables;
using CSharpCompiler.Semantic.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Utility;
using System;
using System.Collections.Generic;

namespace CSharpCompiler.CodeGen.Sections.Text
{
    public sealed class ILCodeBuffer : ByteBuffer
    {
        private Dictionary<OperandType, Action<Instruction>> _writers;
        private MetadataBuilder _metadata;

        public ILCodeBuffer(MetadataBuilder metadata) : base(Empty<byte>.Array, 4)
        {
            _metadata = metadata;
            _writers = new Dictionary<OperandType, Action<Instruction>>();
            _writers.Add(OperandType.InlineBrTarget, WriteInlineBrTarget);
            _writers.Add(OperandType.InlineI, WriteInlineI);
            _writers.Add(OperandType.InlineI8, WriteInlineI8);
            _writers.Add(OperandType.InlineR, WriteInlineR);
            _writers.Add(OperandType.InlineSig, WriteInlineSig);
            _writers.Add(OperandType.InlineString, WriteInlineString);
            _writers.Add(OperandType.InlineSwitch, WriteInlineSwitch);            
            _writers.Add(OperandType.InlineVar, WriteInlineVar);
            _writers.Add(OperandType.InlineArg, WriteInlineArg);
            _writers.Add(OperandType.ShortInlineBrTarget, WriteShortInlineBrTarget);
            _writers.Add(OperandType.ShortInlineI, WriteShortInlineI);
            _writers.Add(OperandType.ShortInlineR, WriteShortInlineR);
            _writers.Add(OperandType.ShortInlineVar, WriteShortInlineVar);
            _writers.Add(OperandType.ShortInlineArg, WriteShortInlineArg);
            _writers.Add(OperandType.InlineMethod, WriteMetadataToken);
            _writers.Add(OperandType.InlineField, WriteMetadataToken);
            _writers.Add(OperandType.InlineTok, WriteMetadataToken);
            _writers.Add(OperandType.InlineType, WriteMetadataToken);
        }

        public ILCodeBuffer(byte[] buffer) : base(buffer) { }
        public ILCodeBuffer(byte[] buffer, int align) : base(buffer, align) { }

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
            var header = new FatMethodHeader();
            header.Size = 0x30;
            header.MaxStack = (ushort)methodDef.Body.MaxStack;
            header.CodeSize = (uint)methodDef.Body.CodeSize;
            header.LocalVarSigTok = _metadata.StandAloneSigTable
                .GetStandAloneSigToken(methodDef.Body.Variables);

            header.Attributes = MethodBodyAttributes.FatFormat;
            if (methodDef.Body.InitLocals) header.Attributes |= MethodBodyAttributes.InitLocals;
            if (methodDef.Body.HasExceptionHandlers) header.Attributes |= MethodBodyAttributes.MoreSects;
            
            return header;
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

        private void WriteOpCode(OpCode opcode)
        {
            if (opcode.Size == 1)
            {
                WriteByte(opcode.Op2);
            }
            else
            {
                WriteByte(opcode.Op1);
                WriteByte(opcode.Op2);
            }
        }

        private void WriteOperand(Instruction instruction)
        {
            var opcode = instruction.OpCode;
            var operandType = opcode.OperandType;
            if (operandType == OperandType.InlineNone)
                return;

            var operand = instruction.Operand;
            if (operand == null)
                throw new ArgumentException("instruction");

            Action<Instruction> writer;
            if (!_writers.TryGetValue(operandType, out writer))
                throw new ArgumentException("instruction");

            writer(instruction);
        }

        private void WriteShortInlineArg(Instruction instruction)
        {
            throw new NotImplementedException();
        }

        private void WriteShortInlineR(Instruction instruction)
        {
            WriteSingle((float)instruction.Operand);
        }

        private void WriteShortInlineI(Instruction instruction)
        {
            WriteByte((byte)instruction.Operand);
        }

        private void WriteShortInlineBrTarget(Instruction instruction)
        {
            throw new NotImplementedException();
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
            WriteDouble((double)instruction.Operand);
        }

        private void WriteInlineI8(Instruction instruction)
        {
            WriteDouble((long)instruction.Operand);
        }

        private void WriteInlineBrTarget(Instruction instruction)
        {
            throw new NotImplementedException();
        }

        private void WriteInlineI(Instruction instruction)
        {
            WriteInt32((int)instruction.Operand);
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
            return _metadata.RegisterUserString(@string);
        }

        private ushort RegisterBlob(ByteBuffer buffer)
        {
            return _metadata.RegisterBlob(buffer);
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
