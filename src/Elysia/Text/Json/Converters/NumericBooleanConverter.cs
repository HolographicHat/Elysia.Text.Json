using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elysia.Text.Json;

public sealed class NumericBooleanConverter : JsonConverter<bool> {

    internal static NumericBooleanConverter Instance => field ??= new NumericBooleanConverter();

    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Number) {
            var value = reader.GetInt64();
            return value switch {
                0 => false,
                1 => true,
                _ => throw new JsonException($"Invalid integer value for boolean: {value}")
            };
        }
        return reader.GetBoolean();
    }

    public override void Write(Utf8JsonWriter writer, bool val, JsonSerializerOptions options) {
        writer.WriteNumberValue(val ? 1 : 0);
    }

}
