using CSharpCompiler.Semantics.Metadata;
using System.Collections.Generic;

namespace CSharpCompiler.Semantics.Cil
{
    public sealed class OpCodeEmitter
    {
        private MethodBody _methodBody;
        private Queue<InstructionReference> _references;

        public OpCodeEmitter(MethodBody methodBody)
        {
            _methodBody = methodBody;
            _references = new Queue<InstructionReference>();
        }

        public void Emit(OpCode opCode)
        {
            Emit(Instruction.Create(opCode));
        }

        public void Emit(OpCode opCode, string value)
        {
            Emit(Instruction.Create(opCode, value));
        }

        public void Emit(OpCode opCode, sbyte value)
        {
            Emit(Instruction.Create(opCode, value));
        }

        public void Emit(OpCode opCode, byte value)
        {
            Emit(Instruction.Create(opCode, value));
        }

        public void Emit(OpCode opCode, int value)
        {
            Emit(Instruction.Create(opCode, value));
        }

        public void Emit(OpCode opCode, long value)
        {
            Emit(Instruction.Create(opCode, value));
        }

        public void Emit(OpCode opCode, float value)
        {
            Emit(Instruction.Create(opCode, value));
        }

        public void Emit(OpCode opCode, double value)
        {
            Emit(Instruction.Create(opCode, value));
        }

        public void Emit(OpCode opCode, IMethodInfo method)
        {
            Emit(Instruction.Create(opCode, method));
        }

        public void Emit(OpCode opCode, VariableDefinition variable)
        {
            Emit(Instruction.Create(opCode, variable));
        }

        public void Emit(OpCode opCode, IInstructionReference instructionRef)
        {
            Emit(Instruction.Create(opCode, instructionRef));
        }

        public void Emit(OpCode opCode, ITypeInfo typrInfo)
        {
            Emit(Instruction.Create(opCode, typrInfo));
        }

        public void Emit(Instruction instruction)
        {
            ResolveInstructionReference(instruction);
            _methodBody.Instructions.Add(instruction);
        }

        public void ResolveOnNextEmit(InstructionReference instructionRef)
        {
            _references.Enqueue(instructionRef);
        }

        private void ResolveInstructionReference(Instruction instruction)
        {
            if (_references.Count == 0) return;

            var instructionRef = _references.Dequeue();
            instructionRef.Resolve(instruction);
        }
    }
}
