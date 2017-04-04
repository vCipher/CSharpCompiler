using CSharpCompiler.Utility;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.CodeGen.Sections.Text
{
    public sealed class TextMap : IReadOnlyList<DataDirectory>
    {
        private const int NUMBER_OF_SEGMENTS = 16;
        private const uint TEXT_SECTION_BASE = 0x00002000;

        private readonly DataDirectory[] _map = new DataDirectory[NUMBER_OF_SEGMENTS];
        
        public int Count
        {
            get { return _map.Length; }
        }

        public DataDirectory this[int index]
        {
            get { return _map[index]; }
        }

        public DataDirectory this[TextSegment segment]
        {
            get { return _map[(int)segment]; }
        }

        public void Add(TextSegment segment, uint size, uint align)
        {
            Add(segment, BitArithmetic.Align(size, align));
        }

        public void Add(TextSegment segment, uint size)
        {
            _map[(int)segment] = new DataDirectory(GetVirtualAddress(segment), size);
        }

        public void Add(TextSegment segment, DataDirectory dir)
        {
            _map[(int)segment] = dir;
        }

        public uint GetNextVirtualAddress(TextSegment segment)
        {
            var index = (int)segment;
            return _map[index].VirtualAddress + _map[index].Size;
        }

        public uint GetSize(TextSegment segment)
        {
            return _map[(int)segment].Size;
        }

        public uint GetSize()
        {
            DataDirectory dir = _map[(int)TextSegment.StartupStub];
            return dir.VirtualAddress - TEXT_SECTION_BASE + dir.Size;
        }

        public uint GetOffset(TextSegment begin, TextSegment end)
        {
            return _map[(int)end].VirtualAddress - _map[(int)begin].VirtualAddress;
        }

        private uint GetVirtualAddress(TextSegment segment)
        {
            return segment == TextSegment.ImportAddressTable 
                ? TEXT_SECTION_BASE 
                : ComputeVirtualAddress((int)segment);
        }

        private uint ComputeVirtualAddress(int index)
        {
            DataDirectory prevDirectory = _map[index - 1];
            return prevDirectory.VirtualAddress + prevDirectory.Size;
        }

        public IEnumerator<DataDirectory> GetEnumerator()
        {
            return ((IEnumerable<DataDirectory>)_map).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.GetEnumerator();
        }
    }
}
