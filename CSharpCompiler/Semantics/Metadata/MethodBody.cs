using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.TypeSystem;
using System;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodBody
    {
        public int CodeSize { get { return ComputeCodeSize(); } }
        public int MaxStack { get { return ComputeMaxStack(); } }
        public bool InitLocals { get { return Variables.Count > 0; } }
        public bool HasVariables { get { return Variables.Count > 0; } }
        public bool HasExceptionHandlers { get { return false /*todo: implement*/; } }


        public Collection<Instruction> Instructions { get; private set; }
        public Collection<VariableDefinition> Variables { get; private set; }
        

        public MethodBody()
        {
            Instructions = new Collection<Instruction>();
            Variables = new Collection<VariableDefinition>();
        }

        public MethodBuilder GetBuilder()
        {
            return new MethodBuilder(this);
        }

        private int ComputeCodeSize()
        {
            int offset = 0;
            foreach (var instruction in Instructions)
            {
                instruction.Offset = offset;
                offset += instruction.GetSize();
            }

            return offset;
        }

        private int ComputeMaxStack()
        {
            int stackSize = 0;
            int maxStack = 0;            
            foreach (var instruction in Instructions)
            {
                ComputeStackDelta(instruction, ref stackSize);
                maxStack = Math.Max(maxStack, stackSize);
            }

            return maxStack;
        }

        private void ComputeStackDelta(Instruction instruction, ref int stackSize)
        {
            var opCode = instruction.OpCode;
            if (opCode.FlowControl == FlowControl.Call)
            {
                var method = (MethodReference)instruction.Operand;
                // push return value
                if (!method.ReturnType.Equals(KnownType.Void) || opCode.Code == Code.Newobj)
                    stackSize++;
                return;
            }

            ComputePopDelta(opCode.PopBehaviour, ref stackSize);
            ComputePushDelta(opCode.PushBehaviour, ref stackSize);
        }

        private void ComputePopDelta(StackBehaviour popBehavior, ref int stackSize)
        {
            switch (popBehavior)
            {
                case StackBehaviour.Popi:
                case StackBehaviour.Popref:
                case StackBehaviour.Pop1:
                    stackSize--;
                    break;
                case StackBehaviour.Pop1_pop1:
                case StackBehaviour.Popi_pop1:
                case StackBehaviour.Popi_popi:
                case StackBehaviour.Popi_popi8:
                case StackBehaviour.Popi_popr4:
                case StackBehaviour.Popi_popr8:
                case StackBehaviour.Popref_pop1:
                case StackBehaviour.Popref_popi:
                    stackSize -= 2;
                    break;
                case StackBehaviour.Popi_popi_popi:
                case StackBehaviour.Popref_popi_popi:
                case StackBehaviour.Popref_popi_popi8:
                case StackBehaviour.Popref_popi_popr4:
                case StackBehaviour.Popref_popi_popr8:
                case StackBehaviour.Popref_popi_popref:
                    stackSize -= 3;
                    break;
                case StackBehaviour.PopAll:
                    stackSize = 0;
                    break;
            }
        }

        private void ComputePushDelta(StackBehaviour pushBehaviour, ref int stackSize)
        {
            switch (pushBehaviour)
            {
                case StackBehaviour.Push1:
                case StackBehaviour.Pushi:
                case StackBehaviour.Pushi8:
                case StackBehaviour.Pushr4:
                case StackBehaviour.Pushr8:
                case StackBehaviour.Pushref:
                    stackSize++;
                    break;
                case StackBehaviour.Push1_push1:
                    stackSize += 2;
                    break;
            }
        }
    }
}
