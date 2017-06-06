using System;

namespace CSharpCompiler.Semantics.Cil
{
    public sealed class TwoByteOpCode : OpCode, IEquatable<TwoByteOpCode>
    {
        internal TwoByteOpCode(
            Code code, OpCodeType opCodeType, OperandType operandType,
            FlowControl flowControl, StackBehaviour pop, StackBehaviour push) 
            : base(code, opCodeType, operandType, flowControl, pop, push)
        { }

        public override int GetSize()
        {
            return 2;
        }

        public override void WriteToBuffer(Utility.ByteBuffer buffer)
        {
            buffer.WriteByte((byte)(((ushort)Code >> 8) & 0xff));
            buffer.WriteByte((byte)(((ushort)Code >> 0) & 0xff));            
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is TwoByteOpCode) && Equals((TwoByteOpCode)obj);
        }

        public bool Equals(TwoByteOpCode other)
        {
            return other != null && Code == other.Code;
        }

        public static bool operator ==(TwoByteOpCode @this, TwoByteOpCode other)
        {
            if (ReferenceEquals(@this, other)) return true;
            return @this != null && @this.Equals(other);
        }

        public static bool operator !=(TwoByteOpCode @this, TwoByteOpCode other)
        {
            return !(@this == other);
        }

        public override string ToString()
        {
            return Code.ToString();
        }
    }
}
