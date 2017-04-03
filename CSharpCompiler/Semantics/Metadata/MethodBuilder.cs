using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Syntax.Ast;
using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Syntax.Ast.Expressions.Conditions;
using CSharpCompiler.Syntax.Ast.Expressions.Relations;
using CSharpCompiler.Syntax.Ast.Statements;
using CSharpCompiler.Syntax.Ast.Variables;
using System;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodBuilder : IArgumentVisitor, IVarDeclarationVisitor, 
        ISyntaxTreeVisitor, IExpressionVisitor, IStatementVisitor
    {
        private MethodBody _methodBody;
        private OpCodeEmitter _emitter;

        public MethodBuilder(MethodBody methodBody)
        {
            _methodBody = methodBody;
            _emitter = new OpCodeEmitter(methodBody);
        }

        #region common
        public void VisitArgument(Argument node)
        {
            node.Value.Accept(this);
        }

        public void VisitSyntaxTree(SyntaxTree node)
        {
            foreach (var statement in node.Statements)
            {
                statement.Accept(this);
            }

            _emitter.Emit(OpCodes.Ret);
        }

        public void VisitVarDeclaration(VarDeclaration node)
        {
            var varDef = GetVarDefinition(node);
            Register(varDef);
            
            var initialier = node.Initializer;
            if (initialier == null) return;

            initialier.Accept(this);
            _emitter.Emit(OpCodes.Stloc, varDef);
        }
        #endregion

        #region conditions
        public void VisitAndOperation(AndOperation node)
        {
            throw new NotImplementedException();
        }
        
        public void VisitOrOperation(OrOperation node)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region relations
        public void VisitEqualOperation(EqualOperation node)
        {
            node.LeftOperand.Accept(this);
            node.RightOperand.Accept(this);
            _emitter.Emit(OpCodes.Ceq);
        }

        public void VisitGreaterOperation(GreaterOperation node)
        {
            node.LeftOperand.Accept(this);
            node.RightOperand.Accept(this);
            _emitter.Emit(OpCodes.Cgt);
        }

        public void VisitGreaterOrEqualOperation(GreaterOrEqualOperation node)
        {
            node.LeftOperand.Accept(this);
            node.RightOperand.Accept(this);
            _emitter.Emit(OpCodes.Clt);
            _emitter.Emit(OpCodes.Ldc_I4_0);
            _emitter.Emit(OpCodes.Ceq);
        }

        public void VisitLessOperation(LessOperation node)
        {
            node.LeftOperand.Accept(this);
            node.RightOperand.Accept(this);
            _emitter.Emit(OpCodes.Clt);
        }

        public void VisitLessOrEqualOperation(LessOrEqualOperation node)
        {
            node.LeftOperand.Accept(this);
            node.RightOperand.Accept(this);
            _emitter.Emit(OpCodes.Clt);
            _emitter.Emit(OpCodes.Ldc_I4_0);
            _emitter.Emit(OpCodes.Ceq);
        }

        public void VisitNotEqualOperation(NotEqualOperation node)
        {
            node.LeftOperand.Accept(this);
            node.RightOperand.Accept(this);
            _emitter.Emit(OpCodes.Ceq);
            _emitter.Emit(OpCodes.Ldc_I4_0);
            _emitter.Emit(OpCodes.Ceq);
        }

        public void VisitArithmeticOperation(ArithmeticOperation node)
        {
            node.LeftOperand.Accept(this);
            node.RightOperand.Accept(this);

            switch (node.Operator.Tag)
            {
                case TokenTag.PLUS: _emitter.Emit(OpCodes.Add); return;
                case TokenTag.MINUS: _emitter.Emit(OpCodes.Sub); return;
                case TokenTag.MULTIPLY: _emitter.Emit(OpCodes.Mul); return;
                case TokenTag.DIVIDE: _emitter.Emit(OpCodes.Div); return;
                case TokenTag.MOD: _emitter.Emit(OpCodes.Rem); return;
                case TokenTag.LEFT_SHIFT: _emitter.Emit(OpCodes.Shl); return;
                case TokenTag.RIGHT_SHIFT: _emitter.Emit(OpCodes.Shr); return;
                case TokenTag.BIT_OR: _emitter.Emit(OpCodes.Or); return;
                case TokenTag.BIT_AND: _emitter.Emit(OpCodes.And); return;
                case TokenTag.BIT_XOR: _emitter.Emit(OpCodes.Xor); return;
            }

            throw new SemanticException("Not supported operator: {0}", node.Operator);
        }

        public void VisitArrayCreation(ArrayCreation node)
        {
            var typeInfo = TypeInference.InferType(node.ContainedType);

            node.Initializer.Accept(this);
            _emitter.Emit(OpCodes.Newarr, typeInfo);

            if (node.IsStatementExpression)
                _emitter.Emit(OpCodes.Pop);
        }

        public void VisitAsOperation(AsOperation node)
        {
            throw new NotImplementedException();
        }

        public void VisitAssignment(Assignment node)
        {
            if (node.LeftOperand is VarAccess)
            {
                BuildAssignment(node, (VarAccess)node.LeftOperand);
                return;
            }

            if (node.LeftOperand is Assignment)
            {
                // todo: make general solution
                BuildAssignment(node, (VarAccess)((Assignment)node.LeftOperand).LeftOperand);
                return;
            }

            throw new SemanticException("The left-hand side of an assignment must be a variable, property or indexer");
        }

        private void BuildAssignment(Assignment node, VarAccess varAccess)
        {
            var varDef = GetVarDefinition(varAccess);
            node.RightOperand.Accept(this);

            if (!node.IsStatementExpression)
                _emitter.Emit(OpCodes.Dup);

            _emitter.Emit(OpCodes.Stloc, varDef);
        }

        public void VisitCastExpression(CastExpression node)
        {
            throw new NotImplementedException();
        }

        public void VisitElementAccess(ElementAccess node)
        {
            node.Array.Accept(this);
            node.Index.Accept(this);
            _emitter.Emit(OpCodes.Ldelem_I4);
        }

        public void VisitElementStore(ElementStore node)
        {
            node.Array.Accept(this);
            node.Index.Accept(this);
            node.Value.Accept(this);

            if (!node.IsStatementExpression)
                _emitter.Emit(OpCodes.Dup);

            _emitter.Emit(OpCodes.Stelem_I4);
        }

        public void VisitEmptyExpression(EmptyExpression node)
        { }

        public void VisitInvokeExpression(InvokeExpression node)
        {
            foreach (var arg in node.Arguments)
            {
                arg.Accept(this);
            }

            _emitter.Emit(OpCodes.Call, node.MethodReference);

            if (NeedStackBalancing(node))
                _emitter.Emit(OpCodes.Pop);
        }

        private bool NeedStackBalancing(InvokeExpression node)
        {
            if (!node.IsStatementExpression) return false;
            if (node.MethodReference.ReturnType.Equals(KnownType.Void)) return false;

            return true;
        }

        public void VisitIsOperation(IsOperation node)
        {
            throw new NotImplementedException();
        }

        public void VisitLiteral(Literal node)
        {
            var lexeme = node.Value.Lexeme;
            switch (node.Value.Tag)
            {
                case TokenTag.INT_LITERAL: _emitter.Emit(OpCodes.Ldc_I4, int.Parse(lexeme)); return;
                case TokenTag.STRING_LITERAL: _emitter.Emit(OpCodes.Ldstr, lexeme); return;
            }

            throw new SemanticException("Not supported literal: {0}", node.Value);
        }

        public void VisitObjectCreation(ObjectCreation node)
        {
            throw new NotImplementedException();
        }

        public void VisitPostfixDecrement(PostfixDecrement node)
        {
            if (node.Operand is VarAccess)
            {
                var varDef = GetVarDefinition((VarAccess)node.Operand);

                _emitter.Emit(OpCodes.Ldloc, varDef);

                if (!node.IsStatementExpression)
                    _emitter.Emit(OpCodes.Dup);

                _emitter.Emit(OpCodes.Ldc_I4_1);
                _emitter.Emit(OpCodes.Sub);
                _emitter.Emit(OpCodes.Stloc, varDef);
                return;
            }

            throw new SemanticException("Posfix decrement is supported only for variables");
        }

        public void VisitPostfixIncrement(PostfixIncrement node)
        {
            if (node.Operand is VarAccess)
            {
                var varDef = GetVarDefinition((VarAccess)node.Operand);

                _emitter.Emit(OpCodes.Ldloc, varDef);

                if (!node.IsStatementExpression)
                    _emitter.Emit(OpCodes.Dup);

                _emitter.Emit(OpCodes.Ldc_I4_1);
                _emitter.Emit(OpCodes.Add);
                _emitter.Emit(OpCodes.Stloc, varDef);
                return;
            }

            throw new SemanticException("Posfix increment is supported only for variables");
        }

        public void VisitPrefixDecrement(PrefixDecrement node)
        {
            if (node.Operand is VarAccess)
            {
                var varDef = GetVarDefinition((VarAccess)node.Operand);

                _emitter.Emit(OpCodes.Ldloc, varDef);
                _emitter.Emit(OpCodes.Ldc_I4_1);
                _emitter.Emit(OpCodes.Sub);

                if (!node.IsStatementExpression)
                    _emitter.Emit(OpCodes.Dup);

                _emitter.Emit(OpCodes.Stloc, varDef);
                return;
            }

            throw new SemanticException("Prefix decrement is supported only for variables");
        }

        public void VisitPrefixIncrement(PrefixIncrement node)
        {
            if (node.Operand is VarAccess)
            {
                var varDef = GetVarDefinition((VarAccess)node.Operand);

                _emitter.Emit(OpCodes.Ldloc, varDef);
                _emitter.Emit(OpCodes.Ldc_I4_1);
                _emitter.Emit(OpCodes.Add);

                if (!node.IsStatementExpression)
                    _emitter.Emit(OpCodes.Dup);

                _emitter.Emit(OpCodes.Stloc, varDef);
                return;
            }

            throw new SemanticException("Prefix increment is supported only for variables");
        }

        public void VisitTernaryOperation(TernaryOperation node)
        {
            throw new NotImplementedException();
        }

        public void VisitUnaryOperation(UnaryOperation node)
        {
            throw new NotImplementedException();
        }

        public void VisitVarAccess(VarAccess node)
        {
            var varDef = GetVarDefinition(node);
            _emitter.Emit(OpCodes.Ldloc, varDef);
        }
        #endregion

        #region statements
        public void VisitBlockStatement(BlockStatement node)
        {
            foreach (var statement in node.Statements)
            {
                statement.Accept(this);
            }
        }

        public void VisitBreakStatement(BreakStatement node)
        {
            _emitter.Emit(OpCodes.Br, node.Enclosure.AfterRefence);
        }

        public void VisitDeclarationStatement(DeclarationStatement node)
        {
            foreach (var declaration in node.Declarations)
            {
                declaration.Accept(this);
            }
        }

        public void VisitExpressionStatement(ExpressionStatement node)
        {
            node.Expression.Accept(this);
        }

        public void VisitForStatement(ForStatement node)
        {
            BuildForDeclarations(node);
            EmitGoto(node.ConditionalReference);

            BuildForBody(node);
            BuildForPostIteration(node);
            BuildForCondition(node);

            _emitter.ResolveOnNextEmit(node.AfterRefence);
        }

        private void BuildForDeclarations(ForStatement node)
        {
            foreach (var declaration in node.Declarations)
            {
                declaration.Accept(this);
            }
        }

        private void BuildForCondition(ForStatement node)
        {
            _emitter.ResolveOnNextEmit(node.ConditionalReference);
            node.Condition.Accept(this);
            _emitter.Emit(OpCodes.Brtrue, node.BodyReference);
        }

        private void BuildForPostIteration(ForStatement node)
        {
            node.PostIteration.Accept(this);
        }

        private void BuildForBody(ForStatement node)
        {
            _emitter.ResolveOnNextEmit(node.BodyReference);
            node.Body.Accept(this);
        }

        public void VisitIfStatement(IfStatement node)
        {
            BuildIfCondition(node);
            BuildIfBody(node);
        }

        private void BuildIfCondition(IfStatement node)
        {
            node.Condition.Accept(this);
            _emitter.Emit(OpCodes.Brfalse, node.AfterRefence);
        }

        private void BuildIfBody(IfStatement node)
        {
            node.Body.Accept(this);
            _emitter.ResolveOnNextEmit(node.AfterRefence);
        }
        #endregion

        #region utils
        private VariableDefinition GetVarDefinition(VarAccess varAccess)
        {
            var declaration = varAccess.GetVarDeclaration();
            return GetVarDefinition(declaration);
        }

        private VariableDefinition GetVarDefinition(VarDeclaration declaration)
        {
            var type = TypeInference.InferType(declaration);
            return new VariableDefinition(declaration.GetUniqueVarName(), type, _methodBody);
        }

        private void Register(VariableDefinition varDef)
        {
            _methodBody.Variables.Add(varDef);
        }

        private void EmitGoto(InstructionReference reference)
        {
            _emitter.Emit(OpCodes.Br, reference);
        }
        #endregion
    }
}
