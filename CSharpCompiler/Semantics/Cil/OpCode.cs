namespace CSharpCompiler.Semantics.Cil
{
    public abstract class OpCode
    {
        public Code Code { get; private set; }
        public OpCodeType OpCodeType { get; private set; }
        public OperandType OperandType { get; private set; }
        public FlowControl FlowControl { get; private set; }
        public StackBehaviour PopBehaviour { get; private set; }
        public StackBehaviour PushBehaviour { get; private set; }

        internal OpCode(
            Code code, OpCodeType opCodeType, OperandType operandType, 
            FlowControl flowControl, StackBehaviour pop, StackBehaviour push)
        {
            Code = code;
            OpCodeType = opCodeType;
            OperandType = operandType;
            FlowControl = flowControl;
            PopBehaviour = pop;
            PushBehaviour = push;
        }

        public abstract int GetSize();
        public abstract void WriteToBuffer(Utility.ByteBuffer buffer);
    }
}