using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elysia.Text.Json;

public sealed class NumericBooleanValueDictionaryConverter<TKey> : JsonConverterFactory where TKey : notnull {

    public override bool CanConvert(Type type) => type == typeof(Dictionary<TKey, bool>);

    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options) => new Converter(options);

    private sealed class Converter : JsonConverter<Dictionary<TKey, bool>> {

        private readonly Type _keyType;
        private readonly JsonConverter<TKey> _keyConverter;

        public Converter(JsonSerializerOptions options) {
            _keyType = typeof(TKey);
            _keyConverter = (JsonConverter<TKey>) options.GetTypeInfo(_keyType).Converter;
        }

        public override Dictionary<TKey, bool> Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) {
            if (reader.TokenType != JsonTokenType.StartObject) {
                // SR.DeserializeUnableToConvertValue
                throw new JsonException($"The JSON value could not be converted to {Type}.");
            }
            var dict = new Dictionary<TKey, bool>();
            while (reader.Read()) {
                if (reader.TokenType == JsonTokenType.EndObject) {
                    return dict;
                }
                //
                if (reader.TokenType != JsonTokenType.PropertyName) {
                    throw new JsonException($"Expected property name but got {reader.TokenType}");
                }
                var key = _keyConverter.Read(ref reader, _keyType, options)!;
                //
                reader.Read();
                var value = NumericBooleanConverter.Instance.Read(ref reader, typeof(bool), options);
                //
                dict.Add(key, value);
            }
            throw new JsonException("Unexpected end of JSON.");
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<TKey, bool> val, JsonSerializerOptions options) {
            writer.WriteStartObject();
            foreach (var (k, v) in val) {
                _keyConverter.WriteAsPropertyName(writer, k, options);
                NumericBooleanConverter.Instance.Write(writer, v, options);
            }
            writer.WriteEndObject();
        }

    }

}
