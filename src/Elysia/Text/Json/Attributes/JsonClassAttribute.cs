using JetBrains.Annotations;

namespace Elysia.Text.Json;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
public sealed class JsonClassAttribute : Attribute {

    public bool WithExtensionData { get; set; }

    public Type[] ExtraTypes { get; set; } = [];

}
