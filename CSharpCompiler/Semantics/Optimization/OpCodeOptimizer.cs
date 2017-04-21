using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Cil;

namespace CSharpCompiler.Semantics.Optimization
{
    public static class OpCodeOptimizer
    {
        public static OpCode Optimize(Instruction instruction)
        {
            switch (instruction.OpCode.Code)
            {
                case Code.Ldc_I4: return OptimizeLdc_I4(instruction);
                case Code.Ldloc: return OptimizeLdloc(instruction);
                case Code.Stloc: return OptimizeStloc(instruction);
                case Code.Br: return OptimizeBr(instruction);
                case Code.Brtrue: return OptimizeBrtrue(instruction);
                case Code.Brfalse: return OptimizeBrfalse(instruction);
            }

            return instruction.OpCode;
        }

        private static OpCode OptimizeBrfalse(Instruction instruction)
        {
            var reference = (InstructionReference)instruction.Operand;
            var offset = reference.Offset;

            return (offset >= 0 && offset <= 255) ? OpCodes.Brfalse_S : instruction.OpCode;
        }

        private static OpCode OptimizeBrtrue(Instruction instruction)
        {
            var reference = (InstructionReference)instruction.Operand;
            var offset = reference.Offset;

            return (offset >= 0 && offset <= 255) ? OpCodes.Brtrue_S : instruction.OpCode;
        }

        private static OpCode OptimizeBr(Instruction instruction)
        {
            var reference = (InstructionReference)instruction.Operand;
            var offset = reference.Offset;

            return (offset >= 0 && offset <= 255) ? OpCodes.Br_S : instruction.OpCode;
        }

        private static OpCode OptimizeLdloc(Instruction instruction)
        {
            var variable = (VariableDefinition)instruction.Operand;
            var variableIndex = variable.Index;
            switch (variableIndex)
            {
                case 0: return OpCodes.Ldloc_0;
                case 1: return OpCodes.Ldloc_1;
                case 2: return OpCodes.Ldloc_2;
                case 3: return OpCodes.Ldloc_3;
            }

            return (variableIndex >= 4 && variableIndex <= 255) ? OpCodes.Ldloc_S : instruction.OpCode;
        }

        private static OpCode OptimizeStloc(Instruction instruction)
        {
            var variable = (VariableDefinition)instruction.Operand;
            var variableIndex = variable.Index;
            switch (variableIndex)
            {
                case 0: return OpCodes.Stloc_0;
                case 1: return OpCodes.Stloc_1;
                case 2: return OpCodes.Stloc_2;
                case 3: return OpCodes.Stloc_3;
            }

            return (variableIndex >= 4 && variableIndex <= 255) ? OpCodes.Stloc_S : instruction.OpCode;
        }

        private static OpCode OptimizeLdc_I4(Instruction instruction)
        {
            var value = (int)instruction.Operand;
            switch (value)
            {
                case 0: return OpCodes.Ldc_I4_0;
                case 1: return OpCodes.Ldc_I4_1;
                case 2: return OpCodes.Ldc_I4_2;
                case 3: return OpCodes.Ldc_I4_3;
                case 4: return OpCodes.Ldc_I4_4;
                case 5: return OpCodes.Ldc_I4_5;
                case 6: return OpCodes.Ldc_I4_6;
                case 7: return OpCodes.Ldc_I4_7;
                case 8: return OpCodes.Ldc_I4_8;
            }

            return (value >= 9 && value <= 255) ? OpCodes.Ldc_I4_S : instruction.OpCode;
        }
    }
}
