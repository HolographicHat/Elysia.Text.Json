using System.ComponentModel;
using System.Text.Json;

namespace Elysia.Text.Json;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class JsonPropertyExtension {

    public static void Deconstruct(this JsonProperty prop, out string name, out JsonElement value) {
        name = prop.Name;
        value = prop.Value;
    }

}
