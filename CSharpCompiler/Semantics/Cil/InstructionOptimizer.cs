using CSharpCompiler.Semantics.Metadata;
using System;
using System.Collections.Generic;

namespace CSharpCompiler.Semantics.Cil
{
    public static class InstructionOptimizer
    {
        private static Dictionary<OpCode, Func<Instruction, Instruction>> _optimizations;

        static InstructionOptimizer()
        {
            _optimizations = new Dictionary<OpCode, Func<Instruction, Instruction>>
            {
                { OpCodes.Ldc_I4, OptimizeLdc_I4 },
                { OpCodes.Ldloc, OptimizeLdloc },
                { OpCodes.Stloc, OptimizeStloc }
            };
        }

        public static Instruction Optimize(Instruction instruction)
        {
            Func<Instruction, Instruction> optimization;
            if (_optimizations.TryGetValue(instruction.OpCode, out optimization))
                return optimization(instruction);

            return instruction;
        }

        private static Instruction OptimizeLdloc(Instruction instruction)
        {
            var variable = (VariableDefinition)instruction.Operand;
            var variableIndex = variable.Index;
            switch (variableIndex)
            {
                case 0: return Instruction.Create(OpCodes.Ldloc_0);
                case 1: return Instruction.Create(OpCodes.Ldloc_1);
                case 2: return Instruction.Create(OpCodes.Ldloc_2);
                case 3: return Instruction.Create(OpCodes.Ldloc_3);
            }

            if (variableIndex >= 4 && variableIndex <= 255)
                return Instruction.Create(OpCodes.Ldloc_S, variable);

            return instruction;
        }

        private static Instruction OptimizeStloc(Instruction instruction)
        {
            var variable = (VariableDefinition)instruction.Operand;
            var variableIndex = variable.Index;
            switch (variableIndex)
            {
                case 0: return Instruction.Create(OpCodes.Stloc_0);
                case 1: return Instruction.Create(OpCodes.Stloc_1);
                case 2: return Instruction.Create(OpCodes.Stloc_2);
                case 3: return Instruction.Create(OpCodes.Stloc_3);
            }

            if (variableIndex >= 4 && variableIndex <= 255)
                return Instruction.Create(OpCodes.Stloc_S, variable);

            return instruction;
        }

        private static Instruction OptimizeLdc_I4(Instruction instruction)
        {
            var value = (int)instruction.Operand;
            switch (value)
            {
                case 0: return Instruction.Create(OpCodes.Ldc_I4_0);
                case 1: return Instruction.Create(OpCodes.Ldc_I4_1);
                case 2: return Instruction.Create(OpCodes.Ldc_I4_2);
                case 3: return Instruction.Create(OpCodes.Ldc_I4_3);
                case 4: return Instruction.Create(OpCodes.Ldc_I4_4);
                case 5: return Instruction.Create(OpCodes.Ldc_I4_5);
                case 6: return Instruction.Create(OpCodes.Ldc_I4_6);
                case 7: return Instruction.Create(OpCodes.Ldc_I4_7);
                case 8: return Instruction.Create(OpCodes.Ldc_I4_8);
            }

            if (value >= 9 && value <= 255)
                return Instruction.Create(OpCodes.Ldc_I4_S, (byte)value);

            return instruction;
        }
    }
}
