using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantic.Cil;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Syntax.Ast;
using CSharpCompiler.Syntax.Ast.Expressions;
using System;
using System.Linq;
using System.Reflection;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodBuilder
    {
        private MethodBody _methodBody;

        public MethodBuilder(MethodBody methodBody)
        {
            _methodBody = methodBody;
        }

        public void Build(Literal literal)
        {
            switch (literal.Value.Tag)
            {
                case TokenTag.INT_LITERAL:
                    int value = int.Parse(literal.Value.Lexeme);
                    Emit(OpCodes.Ldc_I4, value);
                    break;

                case TokenTag.STRING_LITERAL:
                    Emit(OpCodes.Ldstr, literal.Value.Lexeme);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        public void Build(VarDeclaration declaration)
        {
            var varDef = new VariableDefinition(
                declaration.VarName, 
                TypeInference.InferType(declaration),
                _methodBody
            );
            Register(varDef);

            var initialier = declaration.Initializer;
            if (initialier == null)
                return;

            initialier.Build(this);
            Emit(OpCodes.Stloc, varDef);
        }

        public void Build(UnaryOperation unaryOperation)
        {
            throw new NotImplementedException();
        }

        public void Build(IsOperation isOperation)
        {
            throw new NotImplementedException();
        }

        public void Build(VarAccess varAccess)
        {
            var declaration = varAccess.Resolve();
            var varDef = new VariableDefinition(
                declaration.VarName,
                TypeInference.InferType(declaration),
                _methodBody
            );

            Emit(OpCodes.Ldloc, varDef);
        }

        public void Build(CastExpression castExpression)
        {
            throw new NotImplementedException();
        }

        public void Build(TernaryOperation ternaryOperation)
        {
            throw new NotImplementedException();
        }

        public void Build(InvokeExpression invokeExpression)
        {
            foreach (var arg in invokeExpression.Arguments)
            {
                arg.Value.Build(this);
            }

            Emit(OpCodes.Call, GetMethodReference(invokeExpression));
        }

        private MethodReference GetMethodReference(InvokeExpression invokeExpression)
        {
            if (invokeExpression.MethodName != "writeLine")
                throw new NotImplementedException();

            if (invokeExpression.Arguments.Count == 1)
            {
                Expression arg = invokeExpression.Arguments.First().Value;
                switch (arg.InferType().ElementType)
                {
                    case ElementType.Int32:
                        return new MethodReference(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }));

                    case ElementType.String:
                        return new MethodReference(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
                }
            }

            throw new NotImplementedException();            
        }

        public void Build(AsOperation asOperation)
        {
            throw new NotImplementedException();
        }

        public void Build(BinaryOperation binaryOperation)
        {
            binaryOperation.LeftOperand.Build(this);
            binaryOperation.RightOperand.Build(this);

            switch (binaryOperation.Operator.Tag)
            {
                case TokenTag.PLUS: Emit(OpCodes.Add); break;
                case TokenTag.MINUS: Emit(OpCodes.Sub); break;
                case TokenTag.MULTIPLY: Emit(OpCodes.Mul); break;
                case TokenTag.DIVIDE: Emit(OpCodes.Div); break;
                case TokenTag.MOD: Emit(OpCodes.Rem); break;
                case TokenTag.LEFT_SHIFT: Emit(OpCodes.Shl); break;
                case TokenTag.RIGHT_SHIFT: Emit(OpCodes.Shr); break;
                case TokenTag.BIT_OR: Emit(OpCodes.Or); break;
                case TokenTag.BIT_AND: Emit(OpCodes.And); break;
                case TokenTag.BIT_XOR: Emit(OpCodes.Xor); break;
                default: throw new NotSupportedException();
            }
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

        public void Emit(Instruction instruction)
        {
            _methodBody.Instructions.Add(instruction.Optimize());
        }
    }
}
