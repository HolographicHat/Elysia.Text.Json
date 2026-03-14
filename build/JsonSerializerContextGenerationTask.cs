using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Elysia.Text.Json.BuildTask;

public sealed class JsonSerializerContextGenerationTask : Task {

    [Required]
    public string DefineConstants { get; set; } = null!;

    [Required]
    public string IntermediateOutputPath { get; set; } = null!;

    [Required]
    public string? LangVersion { get; set; }

    [Required]
    public ITaskItem[]? References { get; set; }

    [Required]
    public string[] Sources { get; set; } = null!;

    [Output]
    public string[] GeneratedFiles { get; private set; } = null!;

    public override bool Execute() {
        _ = LanguageVersionFacts.TryParse(LangVersion, out var langVersion);
        var parseOptions = new CSharpParseOptions(
            langVersion,
            DocumentationMode.None,
            SourceCodeKind.Regular,
            GetDefines()
        );
        var syntaxTrees = Sources.Select(path => {
            var text = File.ReadAllText(path);
            return CSharpSyntaxTree.ParseText(text, parseOptions, path);
        });
        if (!TryGetMetadataReferences(out var metadataReferences)) {
            return false;
        }
        var compOptions = new CSharpCompilationOptions(outputKind: OutputKind.DynamicallyLinkedLibrary);
        var comp = CSharpCompilation.Create("__Generated", syntaxTrees, metadataReferences, compOptions);
        var gen = new JsonClassGenerator();
        var driver = CSharpGeneratorDriver.Create(gen).WithUpdatedParseOptions(parseOptions).RunGenerators(comp);
        var sources = driver.GetRunResult().Results[0].GeneratedSources;
        GeneratedFiles = new string[sources.Length];
        for (var i = 0; i < sources.Length; i++) {
            var source = sources[i];
            var outputPath = $"{IntermediateOutputPath}/{source.HintName}";
            File.WriteAllText(outputPath, source.SourceText.ToString());
            GeneratedFiles[i] = outputPath;
        }
        return true;
    }

    private bool TryGetMetadataReferences(out IEnumerable<MetadataReference>? references) {
        references = null;
        if (References == null) {
            return true;    // No references
        }
        var refs = new MetadataReference[References.Length];
        for (var i = 0; i < References.Length; i++) {
            var reference = References[i];
            if (!File.Exists(reference.ItemSpec)) {
                Log.LogError($"MSB3104: The referenced assembly \"{reference.ItemSpec}\" was not found.");
                return false;
            }
            var aliasString = reference.GetMetadata("Aliases");
            var aliases = default(ImmutableArray<string>);
            if (!string.IsNullOrEmpty(aliasString)) {
                aliases = [..aliasString.Split(',').Select(s => s.Trim()).Where(s => s.Length != 0)];
                if (aliases.Any(alias => !SyntaxFacts.IsValidIdentifier(alias))) {
                    Log.LogError($"MSB3053: The assembly alias \"{aliasString}\" on reference \"{reference.ItemSpec}\" contains illegal characters.");
                    return false;
                }
            }
            var embedInteropTypes = Utilities.TryConvertItemMetadataToBool(reference, "EmbedInteropTypes");
            var properties = new MetadataReferenceProperties(aliases: aliases, embedInteropTypes: embedInteropTypes);
            refs[i] = MetadataReference.CreateFromFile(reference.ItemSpec, properties);
        }
        references = refs;
        return true;
    }

    private IEnumerable<string>? GetDefines() {
        var defs = DefineConstants.Split([',', ';', ' '], StringSplitOptions.RemoveEmptyEntries).Where(s => {
            if (!SyntaxFacts.IsValidIdentifier(s)) {
                Log.LogWarning($"CS2029: '{s}' is not a valid identifier");
                return false;
            }
            return true;
        }).ToArray();
        return defs.Length == 0 ? null : defs;
    }

}
