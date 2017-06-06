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

        internal static uint Align(uint size, uint alignment)
        {
            if (alignment == 0) throw new ArgumentOutOfRangeException("alignment");
            if (CountBits(alignment) != 1) throw new ArgumentException("alignment");

            uint result = size & ~(alignment - 1);
            return (result == size) ? size : result + alignment;
        }

        internal static int Align(int size, int alignment)
        {
            if (size < 0) throw new ArgumentOutOfRangeException("size");
            if (alignment <= 0) throw new ArgumentOutOfRangeException("alignment");
            if (CountBits(alignment) != 1) throw new ArgumentException("alignment");

            int result = size & ~(alignment - 1);
            return (result == size) ? size : result + alignment;
        }
    }
}