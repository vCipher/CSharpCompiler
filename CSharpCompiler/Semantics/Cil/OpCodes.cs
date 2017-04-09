namespace CSharpCompiler.Semantics.Cil
{
    public static class OpCodes
    {
        public static readonly OpCode Nop = new OpCode(
            Code.Nop, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Break = new OpCode(
            Code.Break, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Break, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Ldarg_0 = new OpCode(
            Code.Ldarg_0, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldarg_1 = new OpCode(
            Code.Ldarg_1, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldarg_2 = new OpCode(
            Code.Ldarg_2, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldarg_3 = new OpCode(
            Code.Ldarg_3, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloc_0 = new OpCode(
            Code.Ldloc_0, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloc_1 = new OpCode(
            Code.Ldloc_1, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloc_2 = new OpCode(
            Code.Ldloc_2, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloc_3 = new OpCode(
            Code.Ldloc_3, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Stloc_0 = new OpCode(
            Code.Stloc_0, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Stloc_1 = new OpCode(
            Code.Stloc_1, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Stloc_2 = new OpCode(
            Code.Stloc_2, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Stloc_3 = new OpCode(
            Code.Stloc_3, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Ldarg_S = new OpCode(
            Code.Ldarg_S, OpCodeType.Macro, OperandType.ShortInlineArg,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldarga_S = new OpCode(
            Code.Ldarga_S, OpCodeType.Macro, OperandType.ShortInlineArg,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Starg_S = new OpCode(
            Code.Starg_S, OpCodeType.Macro, OperandType.ShortInlineArg,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Ldloc_S = new OpCode(
            Code.Ldloc_S, OpCodeType.Macro, OperandType.ShortInlineVar,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloca_S = new OpCode(
            Code.Ldloca_S, OpCodeType.Macro, OperandType.ShortInlineVar,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Stloc_S = new OpCode(
            Code.Stloc_S, OpCodeType.Macro, OperandType.ShortInlineVar,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Ldnull = new OpCode(
            Code.Ldnull, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushref);

        public static readonly OpCode Ldc_I4_M1 = new OpCode(
            Code.Ldc_I4_M1, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_0 = new OpCode(
            Code.Ldc_I4_0, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_1 = new OpCode(
            Code.Ldc_I4_1, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_2 = new OpCode(
            Code.Ldc_I4_2, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_3 = new OpCode(
            Code.Ldc_I4_3, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_4 = new OpCode(
            Code.Ldc_I4_4, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_5 = new OpCode(
            Code.Ldc_I4_5, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_6 = new OpCode(
            Code.Ldc_I4_6, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_7 = new OpCode(
            Code.Ldc_I4_7, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_8 = new OpCode(
            Code.Ldc_I4_8, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_S = new OpCode(
            Code.Ldc_I4_S, OpCodeType.Macro, OperandType.ShortInlineI,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4 = new OpCode(
            Code.Ldc_I4, OpCodeType.Primitive, OperandType.InlineI,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I8 = new OpCode(
            Code.Ldc_I8, OpCodeType.Primitive, OperandType.InlineI8,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi8);

        public static readonly OpCode Ldc_R4 = new OpCode(
            Code.Ldc_R4, OpCodeType.Primitive, OperandType.ShortInlineR,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushr4);

        public static readonly OpCode Ldc_R8 = new OpCode(
            Code.Ldc_R8, OpCodeType.Primitive, OperandType.InlineR,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushr8);

        public static readonly OpCode Dup = new OpCode(
            Code.Dup, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push1_push1);

        public static readonly OpCode Pop = new OpCode(
            Code.Pop, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Jmp = new OpCode(
            Code.Jmp, OpCodeType.Primitive, OperandType.InlineMethod,
            FlowControl.Call, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Call = new OpCode(
            Code.Call, OpCodeType.Primitive, OperandType.InlineMethod,
            FlowControl.Call, StackBehaviour.Varpop, StackBehaviour.Varpush);

        public static readonly OpCode Calli = new OpCode(
            Code.Calli, OpCodeType.Primitive, OperandType.InlineSig,
            FlowControl.Call, StackBehaviour.Varpop, StackBehaviour.Varpush);

        public static readonly OpCode Ret = new OpCode(
            Code.Ret, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Return, StackBehaviour.Varpop, StackBehaviour.Push0);

        public static readonly OpCode Br_S = new OpCode(
            Code.Br_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Branch, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Brfalse_S = new OpCode(
            Code.Brfalse_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Brtrue_S = new OpCode(
            Code.Brtrue_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Beq_S = new OpCode(
            Code.Beq_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bge_S = new OpCode(
            Code.Bge_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bgt_S = new OpCode(
            Code.Bgt_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Ble_S = new OpCode(
            Code.Ble_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Blt_S = new OpCode(
            Code.Blt_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bne_Un_S = new OpCode(
            Code.Bne_Un_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bge_Un_S = new OpCode(
            Code.Bge_Un_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bgt_Un_S = new OpCode(
            Code.Bgt_Un_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Ble_Un_S = new OpCode(
            Code.Ble_Un_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Blt_Un_S = new OpCode(
            Code.Blt_Un_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Br = new OpCode(
            Code.Br, OpCodeType.Primitive, OperandType.InlineBrTarget,
            FlowControl.Branch, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Brfalse = new OpCode(
            Code.Brfalse, OpCodeType.Primitive, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Brtrue = new OpCode(
            Code.Brtrue, OpCodeType.Primitive, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Beq = new OpCode(
            Code.Beq, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bge = new OpCode(
            Code.Bge, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bgt = new OpCode(
            Code.Bgt, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Ble = new OpCode(
            Code.Ble, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Blt = new OpCode(
            Code.Blt, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bne_Un = new OpCode(
            Code.Bne_Un, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bge_Un = new OpCode(
            Code.Bge_Un, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bgt_Un = new OpCode(
            Code.Bgt_Un, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Ble_Un = new OpCode(
            Code.Ble_Un, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Blt_Un = new OpCode(
            Code.Blt_Un, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Switch = new OpCode(
            Code.Switch, OpCodeType.Primitive, OperandType.InlineSwitch,
            FlowControl.Cond_Branch, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Ldind_I1 = new OpCode(
            Code.Ldind_I1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_U1 = new OpCode(
            Code.Ldind_U1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_I2 = new OpCode(
            Code.Ldind_I2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_U2 = new OpCode(
            Code.Ldind_U2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_I4 = new OpCode(
            Code.Ldind_I4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_U4 = new OpCode(
            Code.Ldind_U4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_I8 = new OpCode(
            Code.Ldind_I8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi8);

        public static readonly OpCode Ldind_I = new OpCode(
            Code.Ldind_I, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_R4 = new OpCode(
            Code.Ldind_R4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushr4);

        public static readonly OpCode Ldind_R8 = new OpCode(
            Code.Ldind_R8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushr8);

        public static readonly OpCode Ldind_Ref = new OpCode(
            Code.Ldind_Ref, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushref);

        public static readonly OpCode Stind_Ref = new OpCode(
            Code.Stind_Ref, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stind_I1 = new OpCode(
            Code.Stind_I1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stind_I2 = new OpCode(
            Code.Stind_I2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stind_I4 = new OpCode(
            Code.Stind_I4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stind_I8 = new OpCode(
            Code.Stind_I8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi8, StackBehaviour.Push0);

        public static readonly OpCode Stind_R4 = new OpCode(
            Code.Stind_R4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popr4, StackBehaviour.Push0);

        public static readonly OpCode Stind_R8 = new OpCode(
            Code.Stind_R8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popr8, StackBehaviour.Push0);

        public static readonly OpCode Add = new OpCode(
            Code.Add, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Sub = new OpCode(
            Code.Sub, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Mul = new OpCode(
            Code.Mul, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Div = new OpCode(
            Code.Div, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Div_Un = new OpCode(
            Code.Div_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Rem = new OpCode(
            Code.Rem, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Rem_Un = new OpCode(
            Code.Rem_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode And = new OpCode(
            Code.And, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Or = new OpCode(
            Code.Or, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Xor = new OpCode(
            Code.Xor, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Shl = new OpCode(
            Code.Shl, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Shr = new OpCode(
            Code.Shr, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Shr_Un = new OpCode(
            Code.Shr_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Neg = new OpCode(
            Code.Neg, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push1);

        public static readonly OpCode Not = new OpCode(
            Code.Not, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push1);

        public static readonly OpCode Conv_I1 = new OpCode(
            Code.Conv_I1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_I2 = new OpCode(
            Code.Conv_I2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_I4 = new OpCode(
            Code.Conv_I4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_I8 = new OpCode(
            Code.Conv_I8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Conv_R4 = new OpCode(
            Code.Conv_R4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushr4);

        public static readonly OpCode Conv_R8 = new OpCode(
            Code.Conv_R8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushr8);

        public static readonly OpCode Conv_U4 = new OpCode(
            Code.Conv_U4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_U8 = new OpCode(
            Code.Conv_U8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Callvirt = new OpCode(
            Code.Callvirt, OpCodeType.Objmodel, OperandType.InlineMethod,
            FlowControl.Call, StackBehaviour.Varpop, StackBehaviour.Varpush);

        public static readonly OpCode Cpobj = new OpCode(
            Code.Cpobj, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Ldobj = new OpCode(
            Code.Ldobj, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Push1);

        public static readonly OpCode Ldstr = new OpCode(
            Code.Ldstr, OpCodeType.Objmodel, OperandType.InlineString,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushref);

        public static readonly OpCode Newobj = new OpCode(
            Code.Newobj, OpCodeType.Objmodel, OperandType.InlineMethod,
            FlowControl.Call, StackBehaviour.Varpop, StackBehaviour.Pushref);

        public static readonly OpCode Castclass = new OpCode(
            Code.Castclass, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushref);

        public static readonly OpCode Isinst = new OpCode(
            Code.Isinst, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushi);

        public static readonly OpCode Conv_R_Un = new OpCode(
            Code.Conv_R_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushr8);

        public static readonly OpCode Unbox = new OpCode(
            Code.Unbox, OpCodeType.Primitive, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushi);

        public static readonly OpCode Throw = new OpCode(
            Code.Throw, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Throw, StackBehaviour.Popref, StackBehaviour.Push0);

        public static readonly OpCode Ldfld = new OpCode(
            Code.Ldfld, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Push1);

        public static readonly OpCode Ldflda = new OpCode(
            Code.Ldflda, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushi);

        public static readonly OpCode Stfld = new OpCode(
            Code.Stfld, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Popref_pop1, StackBehaviour.Push0);

        public static readonly OpCode Ldsfld = new OpCode(
            Code.Ldsfld, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldsflda = new OpCode(
            Code.Ldsflda, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Stsfld = new OpCode(
            Code.Stsfld, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Stobj = new OpCode(
            Code.Stobj, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi_pop1, StackBehaviour.Push0);

        public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(
            Code.Conv_Ovf_I1_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(
            Code.Conv_Ovf_I2_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(
            Code.Conv_Ovf_I4_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(
            Code.Conv_Ovf_I8_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(
            Code.Conv_Ovf_U1_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(
            Code.Conv_Ovf_U2_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(
            Code.Conv_Ovf_U4_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(
            Code.Conv_Ovf_U8_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Conv_Ovf_I_Un = new OpCode(
            Code.Conv_Ovf_I_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U_Un = new OpCode(
            Code.Conv_Ovf_U_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Box = new OpCode(
            Code.Box, OpCodeType.Primitive, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushref);

        public static readonly OpCode Newarr = new OpCode(
            Code.Newarr, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushref);

        public static readonly OpCode Ldlen = new OpCode(
            Code.Ldlen, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushi);

        public static readonly OpCode Ldelema = new OpCode(
            Code.Ldelema, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_I1 = new OpCode(
            Code.Ldelem_I1, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_U1 = new OpCode(
            Code.Ldelem_U1, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_I2 = new OpCode(
            Code.Ldelem_I2, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_U2 = new OpCode(
            Code.Ldelem_U2, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_I4 = new OpCode(
            Code.Ldelem_I4, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_U4 = new OpCode(
            Code.Ldelem_U4, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_I8 = new OpCode(
            Code.Ldelem_I8, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi8);

        public static readonly OpCode Ldelem_I = new OpCode(
            Code.Ldelem_I, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_R4 = new OpCode(
            Code.Ldelem_R4, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushr4);

        public static readonly OpCode Ldelem_R8 = new OpCode(
            Code.Ldelem_R8, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushr8);

        public static readonly OpCode Ldelem_Ref = new OpCode(
            Code.Ldelem_Ref, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushref);

        public static readonly OpCode Stelem_I = new OpCode(
            Code.Stelem_I, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stelem_I1 = new OpCode(
            Code.Stelem_I1, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stelem_I2 = new OpCode(
            Code.Stelem_I2, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stelem_I4 = new OpCode(
            Code.Stelem_I4, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stelem_I8 = new OpCode(
            Code.Stelem_I8, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popi8, StackBehaviour.Push0);

        public static readonly OpCode Stelem_R4 = new OpCode(
            Code.Stelem_R4, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popr4, StackBehaviour.Push0);

        public static readonly OpCode Stelem_R8 = new OpCode(
            Code.Stelem_R8, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popr8, StackBehaviour.Push0);

        public static readonly OpCode Stelem_Ref = new OpCode(
            Code.Stelem_Ref, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popref, StackBehaviour.Push0);

        public static readonly OpCode Ldelem_Any = new OpCode(
            Code.Ldelem_Any, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Push1);

        public static readonly OpCode Stelem_Any = new OpCode(
            Code.Stelem_Any, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref_popi_popref, StackBehaviour.Push0);

        public static readonly OpCode Unbox_Any = new OpCode(
            Code.Unbox_Any, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Push1);

        public static readonly OpCode Conv_Ovf_I1 = new OpCode(
            Code.Conv_Ovf_I1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U1 = new OpCode(
            Code.Conv_Ovf_U1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I2 = new OpCode(
            Code.Conv_Ovf_I2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U2 = new OpCode(
            Code.Conv_Ovf_U2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I4 = new OpCode(
            Code.Conv_Ovf_I4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U4 = new OpCode(
            Code.Conv_Ovf_U4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I8 = new OpCode(
            Code.Conv_Ovf_I8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Conv_Ovf_U8 = new OpCode(
            Code.Conv_Ovf_U8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Refanyval = new OpCode(
            Code.Refanyval, OpCodeType.Primitive, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Ckfinite = new OpCode(
            Code.Ckfinite, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushr8);

        public static readonly OpCode Mkrefany = new OpCode(
            Code.Mkrefany, OpCodeType.Primitive, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Push1);

        public static readonly OpCode Ldtoken = new OpCode(
            Code.Ldtoken, OpCodeType.Primitive, OperandType.InlineTok,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Conv_U2 = new OpCode(
            Code.Conv_U2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_U1 = new OpCode(
            Code.Conv_U1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_I = new OpCode(
            Code.Conv_I, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I = new OpCode(
            Code.Conv_Ovf_I, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U = new OpCode(
            Code.Conv_Ovf_U, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Add_Ovf = new OpCode(
            Code.Add_Ovf, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Add_Ovf_Un = new OpCode(
            Code.Add_Ovf_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Mul_Ovf = new OpCode(
            Code.Mul_Ovf, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Mul_Ovf_Un = new OpCode(
            Code.Mul_Ovf_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Sub_Ovf = new OpCode(
            Code.Sub_Ovf, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Sub_Ovf_Un = new OpCode(
            Code.Sub_Ovf_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Endfinally = new OpCode(
            Code.Endfinally, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Return, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Leave = new OpCode(
            Code.Leave, OpCodeType.Primitive, OperandType.InlineBrTarget,
            FlowControl.Branch, StackBehaviour.PopAll, StackBehaviour.Push0);

        public static readonly OpCode Leave_S = new OpCode(
            Code.Leave_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Branch, StackBehaviour.PopAll, StackBehaviour.Push0);

        public static readonly OpCode Stind_I = new OpCode(
            Code.Stind_I, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Conv_U = new OpCode(
            Code.Conv_U, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Arglist = new OpCode(
            Code.Arglist, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ceq = new OpCode(
            Code.Ceq, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Pushi);

        public static readonly OpCode Cgt = new OpCode(
            Code.Cgt, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Pushi);

        public static readonly OpCode Cgt_Un = new OpCode(
            Code.Cgt_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Pushi);

        public static readonly OpCode Clt = new OpCode(
            Code.Clt, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Pushi);

        public static readonly OpCode Clt_Un = new OpCode(
            Code.Clt_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Pushi);

        public static readonly OpCode Ldftn = new OpCode(
            Code.Ldftn, OpCodeType.Primitive, OperandType.InlineMethod,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldvirtftn = new OpCode(
            Code.Ldvirtftn, OpCodeType.Primitive, OperandType.InlineMethod,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushi);

        public static readonly OpCode Ldarg = new OpCode(
            Code.Ldarg, OpCodeType.Primitive, OperandType.InlineArg,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldarga = new OpCode(
            Code.Ldarga, OpCodeType.Primitive, OperandType.InlineArg,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Starg = new OpCode(
            Code.Starg, OpCodeType.Primitive, OperandType.InlineArg,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Ldloc = new OpCode(
            Code.Ldloc, OpCodeType.Primitive, OperandType.InlineVar,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloca = new OpCode(
            Code.Ldloca, OpCodeType.Primitive, OperandType.InlineVar,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Stloc = new OpCode(
            Code.Stloc, OpCodeType.Primitive, OperandType.InlineVar,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Localloc = new OpCode(
            Code.Localloc, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Endfilter = new OpCode(
            Code.Endfilter, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Return, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Unaligned = new OpCode(
            Code.Unaligned, OpCodeType.Prefix, OperandType.ShortInlineI,
            FlowControl.Meta, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Volatile = new OpCode(
            Code.Volatile, OpCodeType.Prefix, OperandType.InlineNone,
            FlowControl.Meta, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Tail = new OpCode(
            Code.Tail, OpCodeType.Prefix, OperandType.InlineNone,
            FlowControl.Meta, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Initobj = new OpCode(
            Code.Initobj, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Constrained = new OpCode(
            Code.Constrained, OpCodeType.Prefix, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Cpblk = new OpCode(
            Code.Cpblk, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Initblk = new OpCode(
            Code.Initblk, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode No = new OpCode(
            Code.No, OpCodeType.Prefix, OperandType.ShortInlineI,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Rethrow = new OpCode(
            Code.Rethrow, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Throw, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Sizeof = new OpCode(
            Code.Sizeof, OpCodeType.Primitive, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Refanytype = new OpCode(
            Code.Refanytype, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Readonly = new OpCode(
            Code.Readonly, OpCodeType.Prefix, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push0);
    }
}
