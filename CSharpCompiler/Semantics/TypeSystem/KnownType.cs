using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public sealed class KnownType : IType
    {
        public KnownTypeCode TypeCode { get; private set; }

        public KnownType(KnownTypeCode typeCode)
        {
            TypeCode = typeCode;
        }
    }
}
