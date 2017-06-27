using CSharpCompiler.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class TransitionTable
    {
        public const int UNKNOWN_STATE = -1;
        
        private static readonly Lazy<TransitionTable> _default = new Lazy<TransitionTable>(FromEmbededResources);
        public static TransitionTable Default => _default.Value;

        public ushort Head { get; private set; }
        public ushort[] Accepting { get; private set; }
        public Dictionary<ushort, string> Aliases { get; private set; }
        public Dictionary<char, ushort>[] Transitions { get; private set; }

        public int this[int id, char ch]
        {
            get
            {
                if (id < 0 || id >= Transitions.Length)
                    return UNKNOWN_STATE;

                ushort state;
                if (!Transitions[id].TryGetValue(ch, out state))
                    return UNKNOWN_STATE;

                return state;
            }
        }

        public TransitionTable(ushort head, Dictionary<char, ushort>[] transitions, Dictionary<ushort, string> aliases)
        {
            Head = head;
            Transitions = transitions;
            Aliases = aliases;
            Accepting = aliases.Keys
                .ToArray(aliases.Count);
        }

        public static TransitionTable FromDefaultVocabulary()
        {
            const string resource = "CSharpCompiler.Lexica.vocabulary.txt";
            var assembly = typeof(TransitionTable).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream(resource))
            using (var reader = new VocabularyConverter(stream))
                return reader.Convert();
        }

        public static TransitionTable FromVocabularyFile(string path)
        {
            using (var stream = File.OpenRead(path))
            using (var reader = new VocabularyConverter(stream))
                return reader.Convert();
        }

        public static TransitionTable FromVocabularyString(string content)
        {
            using (var reader = new VocabularyConverter(content))
                return reader.Convert();
        }

        public static TransitionTable FromFile(string path)
        {
            using (var stream = File.OpenRead(path))
            using (var reader = new TransitionTableBinaryReader(stream))
                return reader.Read();
        }

        public static TransitionTable FromEmbededResources()
        {
            const string resource = "CSharpCompiler.Lexica.vocabulary.bin";
            var assembly = typeof(TransitionTable).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream(resource))
            using (var reader = new TransitionTableBinaryReader(stream))
                return reader.Read();
        }

        public bool IsAcceptingState(int state)
        {
            return Array.BinarySearch(Accepting, (ushort)state) >= 0;
        }

        public string GetTokenAlias(int state)
        {
            return Aliases[(ushort)state];
        }
    }
}
