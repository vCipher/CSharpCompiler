﻿using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodInfoComparer : IEqualityComparer<IMethodInfo>
    {
        public static readonly MethodInfoComparer Default = new MethodInfoComparer();

        private MethodInfoComparer()
        { }

        public bool Equals(IMethodInfo x, IMethodInfo y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x.GetType() != y.GetType()) return false;

            return string.Equals(x.Name, y.Name)
                && x.ReturnType.Equals(y.ReturnType)
                && x.DeclaringType.Equals(y.DeclaringType)
                && x.CallingConventions == x.CallingConventions
                && x.Parameters.SequenceEqual(y.Parameters);
        }

        public int GetHashCode(IMethodInfo obj)
        {
            if (obj == null) return 0;

            unchecked
            {
                int res = 31;
                res ^= EqualityComparer<string>.Default.GetHashCode(obj.Name);
                res ^= StandAloneSignature.GetMethodSignature(obj).GetHashCode();                
                return res;
            }
        }
    }
}