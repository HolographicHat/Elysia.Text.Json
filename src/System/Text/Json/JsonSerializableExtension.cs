using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json.Serialization.Metadata;

using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace Elysia.Text.Json;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class JsonSerializableExtension {

    extension<T>(T value) where T : IJsonSerializable {

        public static T? FromJson(Stream utf8Json, JsonSerializerOptions? options = null) {
            return JsonSerializer.Deserialize<T>(utf8Json, options);
        }

        public static T? FromJson(string json, JsonSerializerOptions? options = null) {
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static async ValueTask<T?> FromJsonAsync(Stream utf8Json, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
            return await JsonSerializer.DeserializeAsync<T>(utf8Json, options, cancellationToken);
        }

        public string ToJson(JsonSerializerOptions? options = null) {
            return JsonSerializer.Serialize(value, options);
        }

        public JsonContent ToJsonContent(MediaTypeHeaderValue? mediaType = null, JsonSerializerOptions? options = null) {
            mediaType ??= new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
            var typeInfo = (JsonTypeInfo<T>) (options ?? JsonSerializer.Options).GetTypeInfo(typeof(T));
            return JsonContent.Create(value, typeInfo, mediaType);
        }

    }

}
