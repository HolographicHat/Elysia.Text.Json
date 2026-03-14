using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGenerators;

namespace Elysia.Text.Json;

public partial class JsonClassGenerator {

    private static JsonClassInfo? GetSemanticTargetForJsonClassGeneration(
        GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken
    ) {
        var typeDef = (TypeDeclarationSyntax) context.TargetNode;
        var typeSym = (ITypeSymbol) context.TargetSymbol;
        //
        if (typeDef is InterfaceDeclarationSyntax) {
            return JsonClassInfo.CreateNotSupported(typeDef);
        }
        //
        SyntaxNode? parent = typeDef;
        do {
            if (parent is TypeDeclarationSyntax parentTypeDef) {
                if (parentTypeDef.IsStatic || !parentTypeDef.IsPartial) {
                    return JsonClassInfo.CreateNotSupported(parentTypeDef);
                }
            }
            parent = parent.Parent;
        } while (parent != null && parent is not CompilationUnitSyntax);
        //
        foreach (var attribute in typeSym.GetAttributes()) {
            if (attribute.AttributeClass?.Name != "JsonClassAttribute" || attribute.AttributeClass.ToDisplayString() != JsonClassAttribute) {
                continue;
            }

            var withExtensionData = false;
            var extraTypes = ImmutableEquatableArray<TypeRef>.Empty;

            var args = attribute.NamedArguments;
            foreach (var (argn, argv) in args) {
                switch (argn) {
                    case "WithExtensionData":
                        withExtensionData = argv.Value is true;
                        break;
                    case "ExtraTypes":
                        extraTypes = argv.Values
                            .Select(s => s.Value is ITypeSymbol symbol ? new TypeRef(symbol) : null)
                            .OfType<TypeRef>()
                            .ToImmutableEquatableArray();
                        break;
                }
            }

            return new JsonClassInfo(null, withExtensionData, new TypeRef(typeSym), extraTypes);
        }
        return null;
    }

    private static JsonSerializerContextInfo GetSemanticTargetForJsonSerializerContextGeneration(
        GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken
    ) {
        var typeDef = (TypeDeclarationSyntax) context.TargetNode;
        var typeSym = (ITypeSymbol) context.TargetSymbol;
        // STJ analyzer will check the validity of the target type
        return new JsonSerializerContextInfo(new TypeRef(typeSym), typeDef.GetLocation());
    }

}

file static class Ext {

    extension(TypeDeclarationSyntax typeDef) {

        public bool IsStatic => typeDef.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.StaticKeyword));

        public bool IsPartial => typeDef.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword));

    }

    public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> pair, out T1 key, out T2 value) {
        key = pair.Key;
        value = pair.Value;
    }

}
