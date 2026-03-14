using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGenerators;

namespace Elysia.Text.Json;

[Generator]
public partial class JsonClassGenerator : IIncrementalGenerator {

    private const string JsonClassAttribute = "Elysia.Text.Json.JsonClassAttribute";

    private const string GlobalJsonSerializerContextAttribute = "Elysia.Text.Json.GlobalJsonSerializerContextAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context) {
        Debugger.Break();
        var classes = context.SyntaxProvider.ForAttributeWithMetadataName(
            JsonClassAttribute,
            static (node, _) => node is TypeDeclarationSyntax,
            GetSemanticTargetForJsonClassGeneration
        ).Where(static x => x is not null).Collect();
        var serializerContexts = context.SyntaxProvider.ForAttributeWithMetadataName(
            GlobalJsonSerializerContextAttribute,
            static (node, _) => node is ClassDeclarationSyntax,
            GetSemanticTargetForJsonSerializerContextGeneration
        ).Collect();
        var isPublishing = context.ParseOptionsProvider.Select((s, _) => s.PreprocessorSymbolNames.Contains("IS_PUBLISHING"));
        context.RegisterSourceOutput(isPublishing.Combine(serializerContexts.Combine(classes)), EmitSourceFile!);
    }

    private sealed record JsonClassInfo(
        Diagnostic? Diagnostic,
        bool WithExtensionData,
        TypeRef TypeRef,
        ImmutableEquatableArray<TypeRef> ExtraTypes
    ) {

        public static JsonClassInfo CreateNotSupported(TypeDeclarationSyntax typeDef) {
            var diagnostic = Diagnostic.Create(
                DiagnosticDescriptors.TypeNotSupported,
                typeDef.GetLocation(),
                typeDef.Identifier.ValueText
            );
            return new JsonClassInfo(diagnostic, false, null!, null!);
        }
    }

    private sealed record JsonSerializerContextInfo(TypeRef TypeRef, Location Location);

}
