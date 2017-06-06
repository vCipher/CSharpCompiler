namespace CSharpCompiler.Semantics.Metadata
{
    public interface IMetadataEntityVisitor
    {
        void VisitAssemblyDefinition(AssemblyDefinition entity);
        void VisitModuleDefinition(ModuleDefinition entity);
        void VisitTypeDefinition(TypeDefinition entity);
        void VisitMethodDefinition(MethodDefinition entity);
        void VisitFieldDefinition(FieldDefinition entity);
        void VisitParameterDefinition(ParameterDefinition entity);
        void VisitCustomAttribute(CustomAttribute entity);
        void VisitStandAloneSignature(StandAloneSignature entity);
        void VisitAssemblyReference(AssemblyReference entity);
        void VisitTypeReference(TypeReference entity);
        void VisitMethodReference(MethodReference entity);
        void VisitTypeSpecification(TypeSpecification entity);
    }
}
