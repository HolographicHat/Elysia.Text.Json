using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elysia.Text.Json;

public sealed class NumericBooleanListConverter : JsonConverter<List<bool>> {

    public override List<bool> Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) {
        if (reader.TokenType != JsonTokenType.StartArray) {
            // SR.DeserializeUnableToConvertValue
            throw new JsonException($"The JSON value could not be converted to {Type}.");
        }
        var list = new List<bool>();
        while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndArray) {
                return list;
            }
            list.Add(NumericBooleanConverter.Instance.Read(ref reader, typeof(bool), options));
        }
        throw new JsonException("Unexpected end of JSON.");
    }

    public override void Write(Utf8JsonWriter writer, List<bool> val, JsonSerializerOptions options) {
        writer.WriteStartArray();
        foreach (var v in val) {
            NumericBooleanConverter.Instance.Write(writer, v, options);
        }
        writer.WriteEndObject();
    }

}
