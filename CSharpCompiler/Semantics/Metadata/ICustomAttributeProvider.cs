using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public interface ICustomAttributeProvider : IMetadataEntity
    {
        Collection<CustomAttribute> CustomAttributes { get; }
    }
}
