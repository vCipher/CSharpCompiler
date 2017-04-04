using System;

namespace CSharpCompiler.Utility
{
    internal static class BitArithmetic
    {
        internal static int CountBits(int value)
        {
            return CountBits(unchecked((uint)value));
        }

        internal static int CountBits(uint value)
        {
            unchecked
            {
                value = value - ((value >> 1) & 0x55555555u);
                value = (value & 0x33333333u) + ((value >> 2) & 0x33333333u);
                return (int)((value + (value >> 4) & 0xF0F0F0Fu) * 0x1010101u) >> 24;
            }
        }

        internal static int CountBits(ulong value)
        {
            const ulong Mask01010101 = 0x5555555555555555UL;
            const ulong Mask00110011 = 0x3333333333333333UL;
            const ulong Mask00001111 = 0x0F0F0F0F0F0F0F0FUL;
            const ulong Mask00000001 = 0x0101010101010101UL;

            value = value - ((value >> 1) & Mask01010101);
            value = (value & Mask00110011) + ((value >> 2) & Mask00110011);
            return (int)(unchecked(((value + (value >> 4)) & Mask00001111) * Mask00000001) >> 56);
        }

        internal static uint Align(uint size, uint alignment)
        {
            if (alignment == 0) throw new ArgumentOutOfRangeException("alignment");
            if (CountBits(alignment) != 1) throw new ArgumentException("alignment");

            uint result = size & ~(alignment - 1);
            return (result == size) ? size : result + alignment;
        }

        internal static int Align(int size, int alignment)
        {
            if (size < 0) throw new ArgumentOutOfRangeException("position");
            if (alignment <= 0) throw new ArgumentOutOfRangeException("alignment");
            if (CountBits(alignment) != 1) throw new ArgumentException("alignment");

            int result = size & ~(alignment - 1);
            return (result == size) ? size : result + alignment;
        }
    }
}