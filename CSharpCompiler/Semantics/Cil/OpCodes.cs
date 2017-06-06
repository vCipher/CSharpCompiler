namespace CSharpCompiler.Semantics.Cil
{
    public static class OpCodes
    {
        public static readonly OpCode Nop = Create(
            Code.Nop, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Break = Create(
            Code.Break, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Break, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Ldarg_0 = Create(
            Code.Ldarg_0, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldarg_1 = Create(
            Code.Ldarg_1, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldarg_2 = Create(
            Code.Ldarg_2, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldarg_3 = Create(
            Code.Ldarg_3, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloc_0 = Create(
            Code.Ldloc_0, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloc_1 = Create(
            Code.Ldloc_1, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloc_2 = Create(
            Code.Ldloc_2, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloc_3 = Create(
            Code.Ldloc_3, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Stloc_0 = Create(
            Code.Stloc_0, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Stloc_1 = Create(
            Code.Stloc_1, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Stloc_2 = Create(
            Code.Stloc_2, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Stloc_3 = Create(
            Code.Stloc_3, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Ldarg_S = Create(
            Code.Ldarg_S, OpCodeType.Macro, OperandType.ShortInlineArg,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldarga_S = Create(
            Code.Ldarga_S, OpCodeType.Macro, OperandType.ShortInlineArg,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Starg_S = Create(
            Code.Starg_S, OpCodeType.Macro, OperandType.ShortInlineArg,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Ldloc_S = Create(
            Code.Ldloc_S, OpCodeType.Macro, OperandType.ShortInlineVar,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloca_S = Create(
            Code.Ldloca_S, OpCodeType.Macro, OperandType.ShortInlineVar,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Stloc_S = Create(
            Code.Stloc_S, OpCodeType.Macro, OperandType.ShortInlineVar,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Ldnull = Create(
            Code.Ldnull, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushref);

        public static readonly OpCode Ldc_I4_M1 = Create(
            Code.Ldc_I4_M1, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_0 = Create(
            Code.Ldc_I4_0, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_1 = Create(
            Code.Ldc_I4_1, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_2 = Create(
            Code.Ldc_I4_2, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_3 = Create(
            Code.Ldc_I4_3, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_4 = Create(
            Code.Ldc_I4_4, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_5 = Create(
            Code.Ldc_I4_5, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_6 = Create(
            Code.Ldc_I4_6, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_7 = Create(
            Code.Ldc_I4_7, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_8 = Create(
            Code.Ldc_I4_8, OpCodeType.Macro, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4_S = Create(
            Code.Ldc_I4_S, OpCodeType.Macro, OperandType.ShortInlineI,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I4 = Create(
            Code.Ldc_I4, OpCodeType.Primitive, OperandType.InlineI,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldc_I8 = Create(
            Code.Ldc_I8, OpCodeType.Primitive, OperandType.InlineI8,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi8);

        public static readonly OpCode Ldc_R4 = Create(
            Code.Ldc_R4, OpCodeType.Primitive, OperandType.ShortInlineR,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushr4);

        public static readonly OpCode Ldc_R8 = Create(
            Code.Ldc_R8, OpCodeType.Primitive, OperandType.InlineR,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushr8);

        public static readonly OpCode Dup = Create(
            Code.Dup, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push1_push1);

        public static readonly OpCode Pop = Create(
            Code.Pop, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Jmp = Create(
            Code.Jmp, OpCodeType.Primitive, OperandType.InlineMethod,
            FlowControl.Call, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Call = Create(
            Code.Call, OpCodeType.Primitive, OperandType.InlineMethod,
            FlowControl.Call, StackBehaviour.Varpop, StackBehaviour.Varpush);

        public static readonly OpCode Calli = Create(
            Code.Calli, OpCodeType.Primitive, OperandType.InlineSig,
            FlowControl.Call, StackBehaviour.Varpop, StackBehaviour.Varpush);

        public static readonly OpCode Ret = Create(
            Code.Ret, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Return, StackBehaviour.Varpop, StackBehaviour.Push0);

        public static readonly OpCode Br_S = Create(
            Code.Br_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Branch, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Brfalse_S = Create(
            Code.Brfalse_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Brtrue_S = Create(
            Code.Brtrue_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Beq_S = Create(
            Code.Beq_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bge_S = Create(
            Code.Bge_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bgt_S = Create(
            Code.Bgt_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Ble_S = Create(
            Code.Ble_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Blt_S = Create(
            Code.Blt_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bne_Un_S = Create(
            Code.Bne_Un_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bge_Un_S = Create(
            Code.Bge_Un_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bgt_Un_S = Create(
            Code.Bgt_Un_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Ble_Un_S = Create(
            Code.Ble_Un_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Blt_Un_S = Create(
            Code.Blt_Un_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Br = Create(
            Code.Br, OpCodeType.Primitive, OperandType.InlineBrTarget,
            FlowControl.Branch, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Brfalse = Create(
            Code.Brfalse, OpCodeType.Primitive, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Brtrue = Create(
            Code.Brtrue, OpCodeType.Primitive, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Beq = Create(
            Code.Beq, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bge = Create(
            Code.Bge, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bgt = Create(
            Code.Bgt, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Ble = Create(
            Code.Ble, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Blt = Create(
            Code.Blt, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bne_Un = Create(
            Code.Bne_Un, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bge_Un = Create(
            Code.Bge_Un, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Bgt_Un = Create(
            Code.Bgt_Un, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Ble_Un = Create(
            Code.Ble_Un, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Blt_Un = Create(
            Code.Blt_Un, OpCodeType.Macro, OperandType.InlineBrTarget,
            FlowControl.Cond_Branch, StackBehaviour.Pop1_pop1, StackBehaviour.Push0);

        public static readonly OpCode Switch = Create(
            Code.Switch, OpCodeType.Primitive, OperandType.InlineSwitch,
            FlowControl.Cond_Branch, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Ldind_I1 = Create(
            Code.Ldind_I1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_U1 = Create(
            Code.Ldind_U1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_I2 = Create(
            Code.Ldind_I2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_U2 = Create(
            Code.Ldind_U2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_I4 = Create(
            Code.Ldind_I4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_U4 = Create(
            Code.Ldind_U4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_I8 = Create(
            Code.Ldind_I8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi8);

        public static readonly OpCode Ldind_I = Create(
            Code.Ldind_I, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldind_R4 = Create(
            Code.Ldind_R4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushr4);

        public static readonly OpCode Ldind_R8 = Create(
            Code.Ldind_R8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushr8);

        public static readonly OpCode Ldind_Ref = Create(
            Code.Ldind_Ref, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushref);

        public static readonly OpCode Stind_Ref = Create(
            Code.Stind_Ref, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stind_I1 = Create(
            Code.Stind_I1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stind_I2 = Create(
            Code.Stind_I2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stind_I4 = Create(
            Code.Stind_I4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stind_I8 = Create(
            Code.Stind_I8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi8, StackBehaviour.Push0);

        public static readonly OpCode Stind_R4 = Create(
            Code.Stind_R4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popr4, StackBehaviour.Push0);

        public static readonly OpCode Stind_R8 = Create(
            Code.Stind_R8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popr8, StackBehaviour.Push0);

        public static readonly OpCode Add = Create(
            Code.Add, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Sub = Create(
            Code.Sub, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Mul = Create(
            Code.Mul, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Div = Create(
            Code.Div, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Div_Un = Create(
            Code.Div_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Rem = Create(
            Code.Rem, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Rem_Un = Create(
            Code.Rem_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode And = Create(
            Code.And, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Or = Create(
            Code.Or, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Xor = Create(
            Code.Xor, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Shl = Create(
            Code.Shl, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Shr = Create(
            Code.Shr, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Shr_Un = Create(
            Code.Shr_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Neg = Create(
            Code.Neg, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push1);

        public static readonly OpCode Not = Create(
            Code.Not, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push1);

        public static readonly OpCode Conv_I1 = Create(
            Code.Conv_I1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_I2 = Create(
            Code.Conv_I2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_I4 = Create(
            Code.Conv_I4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_I8 = Create(
            Code.Conv_I8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Conv_R4 = Create(
            Code.Conv_R4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushr4);

        public static readonly OpCode Conv_R8 = Create(
            Code.Conv_R8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushr8);

        public static readonly OpCode Conv_U4 = Create(
            Code.Conv_U4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_U8 = Create(
            Code.Conv_U8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Callvirt = Create(
            Code.Callvirt, OpCodeType.Objmodel, OperandType.InlineMethod,
            FlowControl.Call, StackBehaviour.Varpop, StackBehaviour.Varpush);

        public static readonly OpCode Cpobj = Create(
            Code.Cpobj, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Ldobj = Create(
            Code.Ldobj, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Push1);

        public static readonly OpCode Ldstr = Create(
            Code.Ldstr, OpCodeType.Objmodel, OperandType.InlineString,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushref);

        public static readonly OpCode Newobj = Create(
            Code.Newobj, OpCodeType.Objmodel, OperandType.InlineMethod,
            FlowControl.Call, StackBehaviour.Varpop, StackBehaviour.Pushref);

        public static readonly OpCode Castclass = Create(
            Code.Castclass, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushref);

        public static readonly OpCode Isinst = Create(
            Code.Isinst, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushi);

        public static readonly OpCode Conv_R_Un = Create(
            Code.Conv_R_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushr8);

        public static readonly OpCode Unbox = Create(
            Code.Unbox, OpCodeType.Primitive, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushi);

        public static readonly OpCode Throw = Create(
            Code.Throw, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Throw, StackBehaviour.Popref, StackBehaviour.Push0);

        public static readonly OpCode Ldfld = Create(
            Code.Ldfld, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Push1);

        public static readonly OpCode Ldflda = Create(
            Code.Ldflda, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushi);

        public static readonly OpCode Stfld = Create(
            Code.Stfld, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Popref_pop1, StackBehaviour.Push0);

        public static readonly OpCode Ldsfld = Create(
            Code.Ldsfld, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldsflda = Create(
            Code.Ldsflda, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Stsfld = Create(
            Code.Stsfld, OpCodeType.Objmodel, OperandType.InlineField,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Stobj = Create(
            Code.Stobj, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi_pop1, StackBehaviour.Push0);

        public static readonly OpCode Conv_Ovf_I1_Un = Create(
            Code.Conv_Ovf_I1_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I2_Un = Create(
            Code.Conv_Ovf_I2_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I4_Un = Create(
            Code.Conv_Ovf_I4_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I8_Un = Create(
            Code.Conv_Ovf_I8_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Conv_Ovf_U1_Un = Create(
            Code.Conv_Ovf_U1_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U2_Un = Create(
            Code.Conv_Ovf_U2_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U4_Un = Create(
            Code.Conv_Ovf_U4_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U8_Un = Create(
            Code.Conv_Ovf_U8_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Conv_Ovf_I_Un = Create(
            Code.Conv_Ovf_I_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U_Un = Create(
            Code.Conv_Ovf_U_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Box = Create(
            Code.Box, OpCodeType.Primitive, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushref);

        public static readonly OpCode Newarr = Create(
            Code.Newarr, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushref);

        public static readonly OpCode Ldlen = Create(
            Code.Ldlen, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushi);

        public static readonly OpCode Ldelema = Create(
            Code.Ldelema, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_I1 = Create(
            Code.Ldelem_I1, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_U1 = Create(
            Code.Ldelem_U1, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_I2 = Create(
            Code.Ldelem_I2, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_U2 = Create(
            Code.Ldelem_U2, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_I4 = Create(
            Code.Ldelem_I4, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_U4 = Create(
            Code.Ldelem_U4, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_I8 = Create(
            Code.Ldelem_I8, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi8);

        public static readonly OpCode Ldelem_I = Create(
            Code.Ldelem_I, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushi);

        public static readonly OpCode Ldelem_R4 = Create(
            Code.Ldelem_R4, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushr4);

        public static readonly OpCode Ldelem_R8 = Create(
            Code.Ldelem_R8, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushr8);

        public static readonly OpCode Ldelem_Ref = Create(
            Code.Ldelem_Ref, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Pushref);

        public static readonly OpCode Stelem_I = Create(
            Code.Stelem_I, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stelem_I1 = Create(
            Code.Stelem_I1, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stelem_I2 = Create(
            Code.Stelem_I2, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stelem_I4 = Create(
            Code.Stelem_I4, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Stelem_I8 = Create(
            Code.Stelem_I8, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popi8, StackBehaviour.Push0);

        public static readonly OpCode Stelem_R4 = Create(
            Code.Stelem_R4, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popr4, StackBehaviour.Push0);

        public static readonly OpCode Stelem_R8 = Create(
            Code.Stelem_R8, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popr8, StackBehaviour.Push0);

        public static readonly OpCode Stelem_Ref = Create(
            Code.Stelem_Ref, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popref_popi_popref, StackBehaviour.Push0);

        public static readonly OpCode Ldelem_Any = Create(
            Code.Ldelem_Any, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref_popi, StackBehaviour.Push1);

        public static readonly OpCode Stelem_Any = Create(
            Code.Stelem_Any, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref_popi_popref, StackBehaviour.Push0);

        public static readonly OpCode Unbox_Any = Create(
            Code.Unbox_Any, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Push1);

        public static readonly OpCode Conv_Ovf_I1 = Create(
            Code.Conv_Ovf_I1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U1 = Create(
            Code.Conv_Ovf_U1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I2 = Create(
            Code.Conv_Ovf_I2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U2 = Create(
            Code.Conv_Ovf_U2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I4 = Create(
            Code.Conv_Ovf_I4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U4 = Create(
            Code.Conv_Ovf_U4, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I8 = Create(
            Code.Conv_Ovf_I8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Conv_Ovf_U8 = Create(
            Code.Conv_Ovf_U8, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi8);

        public static readonly OpCode Refanyval = Create(
            Code.Refanyval, OpCodeType.Primitive, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Ckfinite = Create(
            Code.Ckfinite, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushr8);

        public static readonly OpCode Mkrefany = Create(
            Code.Mkrefany, OpCodeType.Primitive, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Push1);

        public static readonly OpCode Ldtoken = Create(
            Code.Ldtoken, OpCodeType.Primitive, OperandType.InlineTok,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Conv_U2 = Create(
            Code.Conv_U2, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_U1 = Create(
            Code.Conv_U1, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_I = Create(
            Code.Conv_I, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_I = Create(
            Code.Conv_Ovf_I, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Conv_Ovf_U = Create(
            Code.Conv_Ovf_U, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Add_Ovf = Create(
            Code.Add_Ovf, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Add_Ovf_Un = Create(
            Code.Add_Ovf_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Mul_Ovf = Create(
            Code.Mul_Ovf, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Mul_Ovf_Un = Create(
            Code.Mul_Ovf_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Sub_Ovf = Create(
            Code.Sub_Ovf, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Sub_Ovf_Un = Create(
            Code.Sub_Ovf_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Push1);

        public static readonly OpCode Endfinally = Create(
            Code.Endfinally, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Return, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Leave = Create(
            Code.Leave, OpCodeType.Primitive, OperandType.InlineBrTarget,
            FlowControl.Branch, StackBehaviour.PopAll, StackBehaviour.Push0);

        public static readonly OpCode Leave_S = Create(
            Code.Leave_S, OpCodeType.Macro, OperandType.ShortInlineBrTarget,
            FlowControl.Branch, StackBehaviour.PopAll, StackBehaviour.Push0);

        public static readonly OpCode Stind_I = Create(
            Code.Stind_I, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Conv_U = Create(
            Code.Conv_U, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Arglist = Create(
            Code.Arglist, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ceq = Create(
            Code.Ceq, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Pushi);

        public static readonly OpCode Cgt = Create(
            Code.Cgt, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Pushi);

        public static readonly OpCode Cgt_Un = Create(
            Code.Cgt_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Pushi);

        public static readonly OpCode Clt = Create(
            Code.Clt, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Pushi);

        public static readonly OpCode Clt_Un = Create(
            Code.Clt_Un, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1_pop1, StackBehaviour.Pushi);

        public static readonly OpCode Ldftn = Create(
            Code.Ldftn, OpCodeType.Primitive, OperandType.InlineMethod,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Ldvirtftn = Create(
            Code.Ldvirtftn, OpCodeType.Primitive, OperandType.InlineMethod,
            FlowControl.Next, StackBehaviour.Popref, StackBehaviour.Pushi);

        public static readonly OpCode Ldarg = Create(
            Code.Ldarg, OpCodeType.Primitive, OperandType.InlineArg,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldarga = Create(
            Code.Ldarga, OpCodeType.Primitive, OperandType.InlineArg,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Starg = Create(
            Code.Starg, OpCodeType.Primitive, OperandType.InlineArg,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Ldloc = Create(
            Code.Ldloc, OpCodeType.Primitive, OperandType.InlineVar,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push1);

        public static readonly OpCode Ldloca = Create(
            Code.Ldloca, OpCodeType.Primitive, OperandType.InlineVar,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Stloc = Create(
            Code.Stloc, OpCodeType.Primitive, OperandType.InlineVar,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Push0);

        public static readonly OpCode Localloc = Create(
            Code.Localloc, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Pushi);

        public static readonly OpCode Endfilter = Create(
            Code.Endfilter, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Return, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Unaligned = Create(
            Code.Unaligned, OpCodeType.Prefix, OperandType.ShortInlineI,
            FlowControl.Meta, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Volatile = Create(
            Code.Volatile, OpCodeType.Prefix, OperandType.InlineNone,
            FlowControl.Meta, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Tail = Create(
            Code.Tail, OpCodeType.Prefix, OperandType.InlineNone,
            FlowControl.Meta, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Initobj = Create(
            Code.Initobj, OpCodeType.Objmodel, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Popi, StackBehaviour.Push0);

        public static readonly OpCode Constrained = Create(
            Code.Constrained, OpCodeType.Prefix, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Cpblk = Create(
            Code.Cpblk, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode Initblk = Create(
            Code.Initblk, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Popi_popi_popi, StackBehaviour.Push0);

        public static readonly OpCode No = Create(
            Code.No, OpCodeType.Prefix, OperandType.ShortInlineI,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Rethrow = Create(
            Code.Rethrow, OpCodeType.Objmodel, OperandType.InlineNone,
            FlowControl.Throw, StackBehaviour.Pop0, StackBehaviour.Push0);

        public static readonly OpCode Sizeof = Create(
            Code.Sizeof, OpCodeType.Primitive, OperandType.InlineType,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Pushi);

        public static readonly OpCode Refanytype = Create(
            Code.Refanytype, OpCodeType.Primitive, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop1, StackBehaviour.Pushi);

        public static readonly OpCode Readonly = Create(
            Code.Readonly, OpCodeType.Prefix, OperandType.InlineNone,
            FlowControl.Next, StackBehaviour.Pop0, StackBehaviour.Push0);

        private static OpCode Create(
            Code code, OpCodeType opCodeType, OperandType operandType,
            FlowControl flowControl, StackBehaviour pop, StackBehaviour push)
        {
            return (((ushort)code >> 8) == 0x00)
                ? (OpCode)new OneByteOpCode(code, opCodeType, operandType, flowControl, pop, push)
                : (OpCode)new TwoByteOpCode(code, opCodeType, operandType, flowControl, pop, push);
        }
    }
}
