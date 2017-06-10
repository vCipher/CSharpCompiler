using System;
using System.Runtime.InteropServices;

namespace CSharpCompiler.Utility
{
    public class ByteBuffer : IEquatable<ByteBuffer>
    {
        public const int START_POSITION = 0;

        private int _align;

        public byte[] Buffer { get; private set; }
        public int Length { get; private set; }
        public int Position { get; private set; }
        public bool IsEmpty { get { return Length < 1; } }

        public ByteBuffer() : this(Empty<byte>.Array, 1) { }
        public ByteBuffer(int length) : this(new byte[length], 1) { }
        public ByteBuffer(byte[] buffer) : this(buffer, 1) { }
        public ByteBuffer(ByteBuffer buffer) : this(buffer.Buffer, buffer._align) { }

        public ByteBuffer(byte[] buffer, int align)
        {
            _align = align;
            Buffer = buffer ?? Empty<byte>.Array;
            Length = Buffer.Length;
            Position = Buffer.Length;
        }

        public static byte[] ToZeroEndBytes(string @string)
        {
            int size = @string.Length + 1;
            return ToBytes(@string, BitArithmetic.Align((uint)size, 4u));
        }

        public static byte[] ToBytes(string @string)
        {
            return ToBytes(@string, @string.Length);
        }

        public static byte[] ToBytes(string @string, uint length)
        {
            var bytes = new byte[length];
            for (int i = 0; i < @string.Length; i++)
                bytes[i] = (byte)@string[i];

            return bytes;
        }

        public static byte[] ToBytes(string @string, int length)
        {
            var bytes = new byte[length];
            for (int i = 0; i < @string.Length; i++)
                bytes[i] = (byte)@string[i];

            return bytes;
        }

        public static byte[] ToBytes<T>(T value) where T : struct
        {
            var size = Marshal.SizeOf(value);
            var buffer = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(value, ptr, false);
            Marshal.Copy(ptr, buffer, 0, size);
            Marshal.FreeHGlobal(ptr);

            return buffer;
        }

        public static T FromBytes<T>(byte[] bytes) where T : struct
        {
        	var size = bytes.Length;            
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, ptr, size);

            var str = Marshal.PtrToStructure<T>(ptr);
            Marshal.FreeHGlobal(ptr);

            return str;
        }

        public static int SizeOf<T>() where T : struct
        {
            return Marshal.SizeOf<T>();
        }

        public byte[] ToByteArray()
        {
            var result = new byte[Length];
            System.Buffer.BlockCopy(Buffer, 0, result, 0, Length);
            return result;
        }

        public byte ReadByte()
        {
            return Buffer[Position++];
        }

        public sbyte ReadSByte()
        {
            return (sbyte)ReadByte();
        }

        public byte[] ReadBytes(int size)
        {
            var bytes = new byte[size];
            System.Buffer.BlockCopy(Buffer, Position, bytes, 0, size);
            Position += size;
            return bytes;
        }

        public ushort ReadUInt16()
        {
            return (ushort)(Buffer[Position++] 
                | (Buffer[Position++] << 8));
        }

        public short ReadInt16()
        {
            return (short)ReadUInt16();
        }

        public uint ReadUInt32()
        {
            return (uint)(Buffer[Position++]
                | (Buffer[Position++] << 8)
                | (Buffer[Position++] << 16)
                | (Buffer[Position++] << 24));
        }

        public int ReadInt32()
        {
            return (int)ReadUInt32();
        }

        public ulong ReadUInt64()
        {
            uint low = ReadUInt32();
            uint high = ReadUInt32();

            return (((ulong)high) << 32) | low;
        }

        public long ReadInt64()
        {
            return (long)ReadUInt64();
        }

        public uint ReadCompressedUInt32()
        {
            byte first = ReadByte();
            if ((first & 0x80) == 0)
                return first;

            if ((first & 0x40) == 0)
                return ((uint)(first & ~0x80) << 8)
                    | ReadByte();

            return ((uint)(first & ~0xc0) << 24)
                | (uint)ReadByte() << 16
                | (uint)ReadByte() << 8
                | ReadByte();
        }

        public int ReadCompressedInt32()
        {
            var value = (int)(ReadCompressedUInt32() >> 1);
            if ((value & 1) == 0)
                return value;
            if (value < 0x40)
                return value - 0x40;
            if (value < 0x2000)
                return value - 0x2000;
            if (value < 0x10000000)
                return value - 0x10000000;
            return value - 0x20000000;
        }

        public float ReadSingle()
        {
            if (!BitConverter.IsLittleEndian)
            {
                var bytes = ReadBytes(4);
                Array.Reverse(bytes);
                return BitConverter.ToSingle(bytes, 0);
            }

            float value = BitConverter.ToSingle(Buffer, Position);
            Position += 4;
            return value;
        }

        public double ReadDouble()
        {
            if (!BitConverter.IsLittleEndian)
            {
                var bytes = ReadBytes(8);
                Array.Reverse(bytes);
                return BitConverter.ToDouble(bytes, 0);
            }

            double value = BitConverter.ToDouble(Buffer, Position);
            Position += 8;
            return value;
        }

        public T ReadStruct<T>() where T : struct
        {
            var size = Marshal.SizeOf<T>();
            var bytes = ReadBytes(size);
            return FromBytes<T>(bytes);
        }

        public void WriteByte(byte value)
        {
            EnsureCapacity(1);
            Buffer[Position++] = value;
            Length = BitArithmetic.Align(Math.Max(Position, Length), _align);
        }

        public void WriteSByte(sbyte value)
        {
            WriteByte((byte)value);
        }

        public void WriteUInt16(ushort value)
        {
            EnsureCapacity(2);

            Buffer[Position++] = (byte)value;
            Buffer[Position++] = (byte)(value >> 8);

            Length = BitArithmetic.Align(Math.Max(Position, Length), _align);
        }

        public void WriteInt16(short value)
        {
            WriteUInt16((ushort)value);
        }

        public void WriteUInt32(uint value)
        {
            EnsureCapacity(4);

            Buffer[Position++] = (byte)value;
            Buffer[Position++] = (byte)(value >> 8);
            Buffer[Position++] = (byte)(value >> 16);
            Buffer[Position++] = (byte)(value >> 24);

            Length = BitArithmetic.Align(Math.Max(Position, Length), _align);
        }

        public void WriteInt32(int value)
        {
            WriteUInt32((uint)value);
        }

        public void WriteUInt64(ulong value)
        {
            EnsureCapacity(8);

            Buffer[Position++] = (byte)value;
            Buffer[Position++] = (byte)(value >> 8);
            Buffer[Position++] = (byte)(value >> 16);
            Buffer[Position++] = (byte)(value >> 24);
            Buffer[Position++] = (byte)(value >> 32);
            Buffer[Position++] = (byte)(value >> 40);
            Buffer[Position++] = (byte)(value >> 48);
            Buffer[Position++] = (byte)(value >> 56);

            Length = BitArithmetic.Align(Math.Max(Position, Length), _align);
        }

        public void WriteInt64(long value)
        {
            WriteUInt64((ulong)value);
        }

        public void WriteBytes(byte[] bytes)
        {
            EnsureCapacity(bytes.Length);

            System.Buffer.BlockCopy(bytes, 0, Buffer, Position, bytes.Length);
            Position += bytes.Length;

            Length = BitArithmetic.Align(Math.Max(Position, Length), _align);
        }

        public void WriteBuffer(ByteBuffer buffer)
        {
            EnsureCapacity(buffer.Length);

            System.Buffer.BlockCopy(buffer.Buffer, 0, Buffer, Position, buffer.Length);
            Position += buffer.Length;

            Length = BitArithmetic.Align(Math.Max(Position, Length), _align);
        }

        public void WriteStruct<T>(T value) where T : struct
        {
            WriteBytes(ToBytes(value));
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
            {
                WriteByte((byte)value);
            }
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

        public void MoveTo(int position)
        {
            Position = position;
        }

        public override int GetHashCode()
        {
            return ByteBufferComparer.Default.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return (obj is ByteBuffer) && Equals((ByteBuffer)obj);
        }

        public bool Equals(ByteBuffer other)
        {
            return ByteBufferComparer.Default.Equals(this, (ByteBuffer)other);
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
