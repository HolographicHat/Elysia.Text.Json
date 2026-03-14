using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Elysia.Text.Json;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class JsonTypeInfoExtension {

    extension(JsonTypeInfo typeInfo) {

        [UnsafeAccessor(UnsafeAccessorKind.Method)]
        private extern void EnsureConfigured();

        private extern JsonPropertyInfo? ExtensionDataProperty {
            [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_ExtensionDataProperty")] get;
        }

        public Dictionary<string, JsonElement>? GetExtensionData(object inst) {
            typeInfo.EnsureConfigured();
            return (Dictionary<string, JsonElement>?) typeInfo.ExtensionDataProperty?.Get?.Invoke(inst);
        }

    }

}
