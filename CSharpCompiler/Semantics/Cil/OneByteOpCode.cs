using System;

namespace CSharpCompiler.Semantics.Cil
{
    public sealed class OneByteOpCode : OpCode, IEquatable<OneByteOpCode>
    {
        internal OneByteOpCode(
            Code code, OpCodeType opCodeType, OperandType operandType, 
            FlowControl flowControl, StackBehaviour pop, StackBehaviour push) 
            : base(code, opCodeType, operandType, flowControl, pop, push)
        { }

        public override int GetSize()
        {
            return 1;
        }

        public override void WriteToBuffer(Utility.ByteBuffer buffer)
        {
            buffer.WriteByte((byte)(((ushort)Code) & 0xff));
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is OneByteOpCode) && Equals((OneByteOpCode)obj);
        }

        public bool Equals(OneByteOpCode other)
        {
            return other != null && Code == other.Code;
        }

        public static bool operator ==(OneByteOpCode @this, OneByteOpCode other)
        {
            if (ReferenceEquals(@this, other)) return true;
            return @this != null && @this.Equals(other);
        }

        public static bool operator !=(OneByteOpCode @this, OneByteOpCode other)
        {
            return !(@this == other);
        }

        public override string ToString()
        {
            return Code.ToString();
        }
    }
}
