using CSharpCompiler.Semantic.Cil;
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

        private void ComputeStackDelta(Instruction instruction, ref int stack_size)
        {
            if (instruction.OpCode.FlowControl == FlowControl.Call)
            {
                var method = (MethodReference)instruction.Operand;
                // push return value
                if (method.ReturnType.DeclaringType != KnownType.Void || instruction.OpCode.Code == Code.Newobj)
                    stack_size++;
                return;
            }

            ComputePopDelta(instruction.OpCode.PopBehaviour, ref stack_size);
            ComputePushDelta(instruction.OpCode.PushBehaviour, ref stack_size);
        }

        private void ComputePopDelta(StackBehaviour pop_behavior, ref int stack_size)
        {
            switch (pop_behavior)
            {
                case StackBehaviour.Popi:
                case StackBehaviour.Popref:
                case StackBehaviour.Pop1:
                    stack_size--;
                    break;
                case StackBehaviour.Pop1_pop1:
                case StackBehaviour.Popi_pop1:
                case StackBehaviour.Popi_popi:
                case StackBehaviour.Popi_popi8:
                case StackBehaviour.Popi_popr4:
                case StackBehaviour.Popi_popr8:
                case StackBehaviour.Popref_pop1:
                case StackBehaviour.Popref_popi:
                    stack_size -= 2;
                    break;
                case StackBehaviour.Popi_popi_popi:
                case StackBehaviour.Popref_popi_popi:
                case StackBehaviour.Popref_popi_popi8:
                case StackBehaviour.Popref_popi_popr4:
                case StackBehaviour.Popref_popi_popr8:
                case StackBehaviour.Popref_popi_popref:
                    stack_size -= 3;
                    break;
                case StackBehaviour.PopAll:
                    stack_size = 0;
                    break;
            }
        }

        private void ComputePushDelta(StackBehaviour push_behaviour, ref int stack_size)
        {
            switch (push_behaviour)
            {
                case StackBehaviour.Push1:
                case StackBehaviour.Pushi:
                case StackBehaviour.Pushi8:
                case StackBehaviour.Pushr4:
                case StackBehaviour.Pushr8:
                case StackBehaviour.Pushref:
                    stack_size++;
                    break;
                case StackBehaviour.Push1_push1:
                    stack_size += 2;
                    break;
            }
        }
    }
}
