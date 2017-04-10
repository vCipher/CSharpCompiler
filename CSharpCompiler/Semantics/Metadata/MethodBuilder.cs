using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Syntax.Ast;
using CSharpCompiler.Syntax.Ast.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodBuilder
    {
        private MethodBody _methodBody;
        private Queue<InstructionReference> _references;

        public MethodBuilder(MethodBody methodBody)
        {
            _methodBody = methodBody;
            _references = new Queue<InstructionReference>();
        }

        public VariableDefinition GetVarDefinition(VarAccess varAccess)
        {
            var declaration = varAccess.Resolve();
            return GetVarDefinition(declaration);
        }

        public VariableDefinition GetVarDefinition(VarDeclaration declaration)
        {
            var type = declaration.InferType();
            return new VariableDefinition(declaration.VarName, type, _methodBody);
        }

        public void Resolve(InstructionReference instructionRef)
        {
            _references.Enqueue(instructionRef);
        }

        public void Register(string varName, IType type)
        {
            _methodBody.Variables.Add(new VariableDefinition(varName, type, _methodBody));
        }

        public void Register(VariableDefinition varDef)
        {
            _methodBody.Variables.Add(varDef);
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

        public void Emit(OpCode opCode, MethodReference method)
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

        public void Emit(Instruction instruction)
        {
            var result = instruction.Optimize();

            if (_references.Any())
            {
                var instructionRef = _references.Dequeue();
                instructionRef.Resolve(result);
            }

            _methodBody.Instructions.Add(result);
        }
    }
}
