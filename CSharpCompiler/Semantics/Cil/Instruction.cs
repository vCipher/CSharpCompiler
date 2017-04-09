using CSharpCompiler.Semantics.Metadata;
using System;

namespace CSharpCompiler.Semantics.Cil
{
    public sealed class Instruction
    {
        public int Offset { get; set; }
        public OpCode OpCode { get; private set; }
        public object Operand { get; private set; }
        
        private Instruction(OpCode opCode, object operand)
        {
            Offset = 0;
            OpCode = opCode;
            Operand = operand;
        }
        
        public static Instruction Create(OpCode opCode)
        {
            if (opCode.OperandType != OperandType.InlineNone)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, null);
        }

        public static Instruction Create(OpCode opCode, string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (opCode.OperandType != OperandType.InlineString)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, value);
        }

        public static Instruction Create(OpCode opCode, sbyte value)
        {
            if (opCode.OperandType != OperandType.ShortInlineI)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, value);
        }

        public static Instruction Create(OpCode opCode, byte value)
        {
            if (opCode.OperandType != OperandType.ShortInlineI)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, value);
        }

        public static Instruction Create(OpCode opCode, int value)
        {
            if (opCode.OperandType != OperandType.InlineI)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, value);
        }

        public static Instruction Create(OpCode opCode, long value)
        {
            if (opCode.OperandType != OperandType.InlineI8)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, value);
        }

        public static Instruction Create(OpCode opCode, float value)
        {
            if (opCode.OperandType != OperandType.ShortInlineR)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, value);
        }

        public static Instruction Create(OpCode opCode, double value)
        {
            if (opCode.OperandType != OperandType.InlineR)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, value);
        }

        public static Instruction Create(OpCode opCode, MethodReference method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            if (opCode.OperandType != OperandType.InlineMethod && 
                opCode.OperandType != OperandType.InlineTok)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, method);
        }

        public static Instruction Create(OpCode opCode, VariableDefinition variable)
        {
            if (variable == null)
                throw new ArgumentNullException("variable");

            if (opCode.OperandType != OperandType.InlineVar &&
                opCode.OperandType != OperandType.ShortInlineVar)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, variable);
        }

        public static Instruction Create(OpCode opCode, IInstructionReference instructionRef)
        {
            if (instructionRef == null)
                throw new ArgumentNullException("instructionRef");

            if (opCode.OperandType != OperandType.InlineBrTarget &&
                opCode.OperandType != OperandType.ShortInlineBrTarget)
                throw new ArgumentException("opCode");

            return new Instruction(opCode, instructionRef);
        }

        public int GetSize()
        {
            int size = OpCode.Size;

            switch (OpCode.OperandType)
            {
                case OperandType.InlineSwitch:
                    return size + (1 + ((Instruction[])Operand).Length) * 4;
                case OperandType.InlineI8:
                case OperandType.InlineR:
                    return size + 8;
                case OperandType.InlineBrTarget:
                case OperandType.InlineField:
                case OperandType.InlineI:
                case OperandType.InlineMethod:
                case OperandType.InlineString:
                case OperandType.InlineTok:
                case OperandType.InlineType:
                case OperandType.ShortInlineR:
                case OperandType.InlineSig:
                    return size + 4;
                case OperandType.InlineArg:
                case OperandType.InlineVar:
                    return size + 2;
                case OperandType.ShortInlineBrTarget:
                case OperandType.ShortInlineI:
                case OperandType.ShortInlineArg:
                case OperandType.ShortInlineVar:
                    return size + 1;
                default:
                    return size;
            }
        }

        public Instruction Optimize()
        {
            return InstructionOptimizer.Optimize(this);
        }
    }
}
