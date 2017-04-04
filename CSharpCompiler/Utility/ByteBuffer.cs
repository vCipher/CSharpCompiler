using CSharpCompiler.Utility;
using System;
using System.Runtime.InteropServices;

namespace CSharpCompiler.Utility
{
    public class ByteBuffer
    {
        protected int _align;

        public byte[] Buffer { get; private set; }
        public int Length { get; private set; }
        public int Position { get; private set; }
        public bool IsEmpty { get { return Length < 1; } }

        public ByteBuffer() : this(Empty<byte>.Array, 1)
        { }

        public ByteBuffer(int length) : this(new byte[length], 1)
        { }

        public ByteBuffer(byte[] buffer) : this(buffer, 1)
        { }

        public ByteBuffer(byte[] buffer, int align)
        {
            _align = align;
            Buffer = buffer ?? Empty<byte>.Array;
            Length = Buffer.Length;
            Position = Buffer.Length;
        }

        public static byte[] ConvertToZeroEndBytes(string @string)
        {
            int size = @string.Length + 1;
            return ConvertToBytes(@string, BitArithmetic.Align((uint)size, 4u));
        }

        public static byte[] ConvertToBytes(string @string)
        {
            return ConvertToBytes(@string, @string.Length);
        }

        public static byte[] ConvertToBytes(string @string, uint length)
        {
            var bytes = new byte[length];
            for (int i = 0; i < @string.Length; i++)
                bytes[i] = (byte)@string[i];

            return bytes;
        }

        public static byte[] ConvertToBytes(string @string, int length)
        {
            var bytes = new byte[length];
            for (int i = 0; i < @string.Length; i++)
                bytes[i] = (byte)@string[i];

            return bytes;
        }

        public static byte[] ConvertToBytes<T>(T value) where T : struct
        {
            int size = Marshal.SizeOf(value);
            byte[] buffer = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(value, ptr, false);
            Marshal.Copy(ptr, buffer, 0, size);
            Marshal.FreeHGlobal(ptr);

            return buffer;
        }

        public static uint SizeOf<T>() where T : struct
        {
            return (uint)Marshal.SizeOf<T>();
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[Length];
            System.Buffer.BlockCopy(Buffer, 0, result, 0, Length);
            return result;
        }

        public void WriteByte(byte value)
        {
            Write(1, () => Buffer[Position++] = value);
        }

        public void WriteSByte(sbyte value)
        {
            WriteByte((byte)value);
        }

        public void WriteUInt16(ushort value)
        {
            Write(2, () =>
            {
                Buffer[Position++] = (byte)value;
                Buffer[Position++] = (byte)(value >> 8);
            });
        }

        public void WriteInt16(short value)
        {
            WriteUInt16((ushort)value);
        }

        public void WriteUInt32(uint value)
        {
            Write(4, () =>
            {
                Buffer[Position++] = (byte)value;
                Buffer[Position++] = (byte)(value >> 8);
                Buffer[Position++] = (byte)(value >> 16);
                Buffer[Position++] = (byte)(value >> 24);
            });
        }

        public void WriteInt32(int value)
        {
            WriteUInt32((uint)value);
        }

        public void WriteUInt64(ulong value)
        {
            Write(8, () =>
            {
                Buffer[Position++] = (byte)value;
                Buffer[Position++] = (byte)(value >> 8);
                Buffer[Position++] = (byte)(value >> 16);
                Buffer[Position++] = (byte)(value >> 24);
                Buffer[Position++] = (byte)(value >> 32);
                Buffer[Position++] = (byte)(value >> 40);
                Buffer[Position++] = (byte)(value >> 48);
                Buffer[Position++] = (byte)(value >> 56);
            });
        }

        public void WriteInt64(long value)
        {
            WriteUInt64((ulong)value);
        }        

        public void WriteBytes(byte[] bytes)
        {
            Write(bytes.Length, () =>
            {
                System.Buffer.BlockCopy(bytes, 0, Buffer, Position, bytes.Length);
                Position += bytes.Length;
            });
        }

        public void WriteBuffer(ByteBuffer buffer)
        {
            Write(buffer.Length, () =>
            {
                System.Buffer.BlockCopy(buffer.Buffer, 0, Buffer, Position, buffer.Length);
                Position += buffer.Length;
            });
        }

        public void WriteBytes(ByteBuffer buffer)
        {
            Write(buffer.Length, () =>
            {
                System.Buffer.BlockCopy(buffer.Buffer, 0, Buffer, Position, buffer.Length);
                Position += buffer.Length;
            });
        }

        public void WriteStruct<T>(T value) where T : struct
        {
            WriteBytes(ConvertToBytes(value));
        }

        public void WriteSingle(float value)
        {
            var bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            WriteBytes(bytes);
        }

        public void WriteDouble(double value)
        {
            var bytes = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            WriteBytes(bytes);
        }

        public void WriteCompressedUInt32(uint value)
        {
            if (value < 0x80)
                WriteByte((byte)value);
            else if (value < 0x4000)
            {
                WriteByte((byte)(0x80 | (value >> 8)));
                WriteByte((byte)(value & 0xff));
            }
            else
            {
                WriteByte((byte)((value >> 24) | 0xc0));
                WriteByte((byte)((value >> 16) & 0xff));
                WriteByte((byte)((value >> 8) & 0xff));
                WriteByte((byte)(value & 0xff));
            }
        }

        private void Write(int length, Action writer)
        {
            EnsureCapacity(length);
            writer();
            Length = BitArithmetic.Align(Math.Max(Position, Length), _align);
        }

        private void EnsureCapacity(int value)
        {
            if (Position + value <= Buffer.Length)
                return;

            int oldLength = Buffer.Length;
            int length = BitArithmetic.Align(Math.Max(oldLength + value, oldLength * 2), _align);

            byte[] oldBuffer = Buffer;
            byte[] buffer = new byte[length];

            System.Buffer.BlockCopy(oldBuffer, 0, buffer, 0, oldLength);
            Buffer = buffer;
        }
    }
}
