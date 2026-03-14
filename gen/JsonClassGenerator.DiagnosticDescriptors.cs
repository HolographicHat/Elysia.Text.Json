using Microsoft.CodeAnalysis;

namespace Elysia.Text.Json;

public partial class JsonClassGenerator {

    internal static class DiagnosticDescriptors {

        internal static readonly DiagnosticDescriptor TypeNotSupported = new(
            "EJGEN001",
            "Did not generate code for type",
            "Did not generate code for type '{0}'",
            nameof(JsonClassGenerator),
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

        internal static readonly DiagnosticDescriptor MultipleSerializerContexts = new(
            "EJGEN002",
            "More than one type attributed with GlobalJsonSerializerContextAttribute",
            "More than one type attributed with GlobalJsonSerializerContextAttribute",
            nameof(JsonClassGenerator),
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

    }

}
