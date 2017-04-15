using CSharpCompiler.Semantics.TypeSystem;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class TypeDefinition : ITypeInfo, ICustomAttributeProvider
    {
        public string Name { get; private set; }
        public string Namespace { get; private set; }
        public ElementType ElementType { get; private set; }
        public IAssemblyInfo Assembly { get; private set; }
        public TypeAttributes Attributes { get; private set; }
        public ITypeInfo BaseType { get; private set; }
        public Collection<MethodDefinition> Methods { get; private set; }
        public Collection<FieldDefinition> Fields { get; private set; }
        public Collection<CustomAttribute> CustomAttributes { get; private set; }

        public TypeDefinition(string name, string @namespace, TypeAttributes attributes, ITypeInfo baseType, IAssemblyInfo assembly)
        {
            Name = name;
            Namespace = @namespace;
            ElementType = ElementType.Class;
            Assembly = assembly;
            Attributes = attributes;
            BaseType = baseType;
            Methods = new Collection<MethodDefinition>();
            Fields = new Collection<FieldDefinition>();
            CustomAttributes = new Collection<CustomAttribute>();
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}, {2}", Namespace, Name, Assembly);
        }

        public override int GetHashCode()
        {
            return TypeInfoComparer.Default.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TypeDefinition)) return false;
            return Equals((TypeDefinition)obj);
        }

        public bool Equals(ITypeInfo other)
        {
            if (!(other is TypeDefinition)) return false;
            return Equals((TypeDefinition)other);
        }

        public bool Equals(TypeDefinition other)
        {
            return TypeInfoComparer.Default.Equals(this, other);
        }
    }
}
