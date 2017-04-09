using System;

namespace CSharpCompiler.Semantics.Cil
{
    public sealed class OpCode : IEquatable<OpCode>
    {
        public byte Op1 { get; private set; }
        public byte Op2 { get; private set; }
        public Code Code { get; private set; }
        public OpCodeType OpCodeType { get; private set; }
        public OperandType OperandType { get; private set; }
        public FlowControl FlowControl { get; private set; }
        public StackBehaviour PopBehaviour { get; private set; }
        public StackBehaviour PushBehaviour { get; private set; }

        public int Size
        {
            get { return Op1 == 0x00 ? 1 : 2; }
        }

        internal OpCode(
            Code code, OpCodeType opCodeType, OperandType operandType, 
            FlowControl flowControl, StackBehaviour pop, StackBehaviour push)
        {
            Op1 = (byte)(((ushort)code >> 8) & 0xff);
            Op2 = (byte)(((ushort)code >> 0) & 0xff);
            Code = code;
            OpCodeType = opCodeType;
            OperandType = operandType;
            FlowControl = flowControl;
            PopBehaviour = pop;
            PushBehaviour = push;
        }

        public override int GetHashCode()
        {
            return unchecked((Op1 << 8) | Op2);
        }

        public override bool Equals(object obj)
        {
            if (obj is OpCode)
                return Equals((OpCode)obj);
            
            return false;
        }

        public bool Equals(OpCode opcode)
        {
            return Op1 == opcode.Op1 && Op2 == opcode.Op2;
        }

        public static bool operator ==(OpCode one, OpCode other)
        {
            return one.Op1 == other.Op1 && one.Op2 == other.Op2;
        }

        public static bool operator !=(OpCode one, OpCode other)
        {
            return one.Op1 != other.Op1 || one.Op2 != other.Op2;
        }

        public override string ToString()
        {
            return Code.ToString();
        }
    }
}