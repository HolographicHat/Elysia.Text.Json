using System.Diagnostics.CodeAnalysis;
using System.IO.Pipelines;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using JetBrains.Annotations;

using SystemJsonSerializer = System.Text.Json.JsonSerializer;

// ReSharper disable ConvertToExtensionBlock, InvokeAsExtensionMember

namespace Elysia.Text.Json;

public static class JsonSerializer {

    [UsedImplicitly]
    public static JsonSerializerOptions Options { get; set; } = new();

    #region Read.Document

    /// <summary>
    /// Converts the <see cref="JsonDocument"/> representing a single JSON value into a <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
    /// <param name="document">The <see cref="JsonDocument"/> to convert.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="document"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// <typeparamref name="TValue" /> is not compatible with the JSON.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static TValue? Deserialize<TValue>(this JsonDocument document, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(document, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Converts the <see cref="JsonDocument"/> representing a single JSON value into a <paramref name="returnType"/>.
    /// </summary>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <param name="document">The <see cref="JsonDocument"/> to convert.</param>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="document"/> or <paramref name="returnType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// <paramref name="returnType"/> is not compatible with the JSON.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    public static object? Deserialize(this JsonDocument document, Type returnType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(document, (options ?? Options).GetTypeInfo(returnType));
    }

    #endregion

    #region Read.Element

    /// <summary>
    /// Converts the <see cref="JsonElement"/> representing a single JSON value into a <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
    /// <param name="element">The <see cref="JsonElement"/> to convert.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="JsonException">
    /// <typeparamref name="TValue" /> is not compatible with the JSON.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static TValue? Deserialize<TValue>(this JsonElement element, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(element, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Converts the <see cref="JsonElement"/> representing a single JSON value into a <paramref name="returnType"/>.
    /// </summary>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <param name="element">The <see cref="JsonElement"/> to convert.</param>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="returnType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// <paramref name="returnType"/> is not compatible with the JSON.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    public static object? Deserialize(this JsonElement element, Type returnType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(element, (options ?? Options).GetTypeInfo(returnType));
    }

    #endregion

    #region Read.Node

    /// <summary>
    /// Converts the <see cref="JsonNode"/> representing a single JSON value into a <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
    /// <param name="node">The <see cref="JsonNode"/> to convert.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="JsonException">
    /// <typeparamref name="TValue" /> is not compatible with the JSON.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static TValue? Deserialize<TValue>(this JsonNode? node, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(node, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Converts the <see cref="JsonNode"/> representing a single JSON value into a <paramref name="returnType"/>.
    /// </summary>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <param name="node">The <see cref="JsonNode"/> to convert.</param>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="JsonException">
    /// <paramref name="returnType"/> is not compatible with the JSON.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    public static object? Deserialize(this JsonNode? node, Type returnType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(node, (options ?? Options).GetTypeInfo(returnType));
    }

    #endregion

    #region Read.Pipe

    /// <summary>
    /// Reads the UTF-8 encoded text representing a single JSON value into a <typeparamref name="TValue"/>.
    /// The PipeReader will be read to completion.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <param name="cancellationToken">
    /// The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the read operation.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="utf8Json"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid,
    /// <typeparamref name="TValue"/> is not compatible with the JSON,
    /// or when there is remaining data in the PipeReader.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static ValueTask<TValue?> DeserializeAsync<TValue>(PipeReader utf8Json, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.DeserializeAsync(utf8Json, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)), cancellationToken);
    }

    /// <summary>
    /// Reads the UTF-8 encoded text representing a single JSON value into a <paramref name="returnType"/>.
    /// The PipeReader will be read to completion.
    /// </summary>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <param name="cancellationToken">
    /// The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the read operation.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="utf8Json"/> or <paramref name="returnType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid,
    /// the <paramref name="returnType"/> is not compatible with the JSON,
    /// or when there is remaining data in the PipeReader.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    public static ValueTask<object?> DeserializeAsync(PipeReader utf8Json, Type returnType, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.DeserializeAsync(utf8Json, (options ?? Options).GetTypeInfo(returnType), cancellationToken);
    }

    /// <summary>
    /// Wraps the UTF-8 encoded text into an <see cref="IAsyncEnumerable{TValue}" />
    /// that can be used to deserialize root-level JSON arrays in a streaming manner.
    /// </summary>
    /// <typeparam name="TValue">The element type to deserialize asynchronously.</typeparam>
    /// <returns>An <see cref="IAsyncEnumerable{TValue}" /> representation of the provided JSON array.</returns>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the read operation.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="utf8Json"/> is <see langword="null"/>.
    /// </exception>
    public static IAsyncEnumerable<TValue?> DeserializeAsyncEnumerable<TValue>(PipeReader utf8Json, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.DeserializeAsyncEnumerable(utf8Json, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)), cancellationToken);
    }

    /// <summary>
    /// Wraps the UTF-8 encoded text into an <see cref="IAsyncEnumerable{TValue}" />
    /// that can be used to deserialize sequences of JSON values in a streaming manner.
    /// </summary>
    /// <typeparam name="TValue">The element type to deserialize asynchronously.</typeparam>
    /// <returns>An <see cref="IAsyncEnumerable{TValue}" /> representation of the provided JSON sequence.</returns>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="topLevelValues"><see langword="true"/> to deserialize from a sequence of top-level JSON values, or <see langword="false"/> to deserialize from a single top-level array.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the read operation.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="utf8Json"/> is <see langword="null"/>.
    /// </exception>
    /// <remarks>
    /// When <paramref name="topLevelValues"/> is set to <see langword="true" />, treats the PipeReader as a sequence of
    /// whitespace separated top-level JSON values and attempts to deserialize each value into <typeparamref name="TValue"/>.
    /// When <paramref name="topLevelValues"/> is set to <see langword="false" />, treats the PipeReader as a JSON array and
    /// attempts to serialize each element into <typeparamref name="TValue"/>.
    /// </remarks>
    public static IAsyncEnumerable<TValue?> DeserializeAsyncEnumerable<TValue>(PipeReader utf8Json, bool topLevelValues, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.DeserializeAsyncEnumerable(utf8Json, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)), topLevelValues, cancellationToken);
    }

    #endregion

    #region Read.Span

    /// <summary>
    /// Parses the UTF-8 encoded text representing a single JSON value into a <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
    /// <param name="utf8Json">JSON text to parse.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="JsonException">
    /// The JSON is invalid,
    /// <typeparamref name="TValue"/> is not compatible with the JSON,
    /// or when there is remaining data in the Stream.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static TValue? Deserialize<TValue>(ReadOnlySpan<byte> utf8Json, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(utf8Json, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Parses the UTF-8 encoded text representing a single JSON value into a <paramref name="returnType"/>.
    /// </summary>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <param name="utf8Json">JSON text to parse.</param>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="returnType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid,
    /// <paramref name="returnType"/> is not compatible with the JSON,
    /// or when there is remaining data in the Stream.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    public static object? Deserialize(ReadOnlySpan<byte> utf8Json, Type returnType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(utf8Json, (options ?? Options).GetTypeInfo(returnType));
    }

    #endregion

    #region Read.Stream

    /// <summary>
    /// Reads the UTF-8 encoded text representing a single JSON value into a <typeparamref name="TValue"/>.
    /// The Stream will be read to completion.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <param name="cancellationToken">
    /// The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the read operation.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="utf8Json"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid,
    /// <typeparamref name="TValue"/> is not compatible with the JSON,
    /// or when there is remaining data in the Stream.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static ValueTask<TValue?> DeserializeAsync<TValue>(Stream utf8Json, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.DeserializeAsync(utf8Json, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)), cancellationToken);
    }

    /// <summary>
    /// Reads the UTF-8 encoded text representing a single JSON value into a <typeparamref name="TValue"/>.
    /// The Stream will be read to completion.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="utf8Json"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid,
    /// <typeparamref name="TValue"/> is not compatible with the JSON,
    /// or when there is remaining data in the Stream.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static TValue? Deserialize<TValue>(Stream utf8Json, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(utf8Json, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Reads the UTF-8 encoded text representing a single JSON value into a <paramref name="returnType"/>.
    /// The Stream will be read to completion.
    /// </summary>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <param name="cancellationToken">
    /// The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the read operation.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="utf8Json"/> or <paramref name="returnType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid,
    /// the <paramref name="returnType"/> is not compatible with the JSON,
    /// or when there is remaining data in the Stream.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    public static ValueTask<object?> DeserializeAsync(Stream utf8Json, Type returnType, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.DeserializeAsync(utf8Json, (options ?? Options).GetTypeInfo(returnType), cancellationToken);
    }

    /// <summary>
    /// Reads the UTF-8 encoded text representing a single JSON value into a <paramref name="returnType"/>.
    /// The Stream will be read to completion.
    /// </summary>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="utf8Json"/> or <paramref name="returnType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid,
    /// the <paramref name="returnType"/> is not compatible with the JSON,
    /// or when there is remaining data in the Stream.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    public static object? Deserialize(Stream utf8Json, Type returnType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(utf8Json, (options ?? Options).GetTypeInfo(returnType));
    }

    /// <summary>
    /// Wraps the UTF-8 encoded text into an <see cref="IAsyncEnumerable{TValue}" />
    /// that can be used to deserialize root-level JSON arrays in a streaming manner.
    /// </summary>
    /// <typeparam name="TValue">The element type to deserialize asynchronously.</typeparam>
    /// <returns>An <see cref="IAsyncEnumerable{TValue}" /> representation of the provided JSON array.</returns>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the read operation.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="utf8Json"/> is <see langword="null"/>.
    /// </exception>
    public static IAsyncEnumerable<TValue?> DeserializeAsyncEnumerable<TValue>(Stream utf8Json, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.DeserializeAsyncEnumerable(utf8Json, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)), cancellationToken);
    }

    /// <summary>
    /// Wraps the UTF-8 encoded text into an <see cref="IAsyncEnumerable{TValue}" />
    /// that can be used to deserialize sequences of JSON values in a streaming manner.
    /// </summary>
    /// <typeparam name="TValue">The element type to deserialize asynchronously.</typeparam>
    /// <returns>An <see cref="IAsyncEnumerable{TValue}" /> representation of the provided JSON sequence.</returns>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="topLevelValues"><see langword="true"/> to deserialize from a sequence of top-level JSON values, or <see langword="false"/> to deserialize from a single top-level array.</param>
    /// <param name="options">Options to control the behavior during reading.</param>
    /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the read operation.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="utf8Json"/> is <see langword="null"/>.
    /// </exception>
    /// <remarks>
    /// When <paramref name="topLevelValues"/> is set to <see langword="true" />, treats the stream as a sequence of
    /// whitespace separated top-level JSON values and attempts to deserialize each value into <typeparamref name="TValue"/>.
    /// When <paramref name="topLevelValues"/> is set to <see langword="false" />, treats the stream as a JSON array and
    /// attempts to serialize each element into <typeparamref name="TValue"/>.
    /// </remarks>
    public static IAsyncEnumerable<TValue?> DeserializeAsyncEnumerable<TValue>(Stream utf8Json, bool topLevelValues, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.DeserializeAsyncEnumerable(utf8Json, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)), topLevelValues, cancellationToken);
    }

    #endregion

    #region Read.String

    /// <summary>
    /// Parses the text representing a single JSON value into a <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
    /// <param name="json">JSON text to parse.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="json"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid.
    ///
    /// -or-
    ///
    /// <typeparamref name="TValue" /> is not compatible with the JSON.
    ///
    /// -or-
    ///
    /// There is remaining data in the string beyond a single JSON value.</exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    /// <remarks>Using a <see cref="string"/> is not as efficient as using the
    /// UTF-8 methods since the implementation natively uses UTF-8.
    /// </remarks>
    public static TValue? Deserialize<TValue>([StringSyntax("Json")] string json, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(json, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Parses the text representing a single JSON value into an instance of the type specified by a generic type parameter.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
    /// <param name="json">The JSON text to parse.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="JsonException">
    /// The JSON is invalid.
    ///
    /// -or-
    ///
    /// <typeparamref name="TValue" /> is not compatible with the JSON.
    ///
    /// -or-
    ///
    /// There is remaining data in the span beyond a single JSON value.</exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    /// <remarks>Using a UTF-16 span is not as efficient as using the
    /// UTF-8 methods since the implementation natively uses UTF-8.
    /// </remarks>
    public static TValue? Deserialize<TValue>([StringSyntax("Json")] ReadOnlySpan<char> json, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(json, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Parses the text representing a single JSON value into a <paramref name="returnType"/>.
    /// </summary>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <param name="json">JSON text to parse.</param>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="json"/> or <paramref name="returnType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid.
    ///
    /// -or-
    ///
    /// <paramref name="returnType"/> is not compatible with the JSON.
    ///
    /// -or-
    ///
    /// There is remaining data in the string beyond a single JSON value.</exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    /// <remarks>Using a <see cref="string"/> is not as efficient as using the
    /// UTF-8 methods since the implementation natively uses UTF-8.
    /// </remarks>
    public static object? Deserialize([StringSyntax("Json")] string json, Type returnType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(json, (options ?? Options).GetTypeInfo(returnType));
    }

    /// <summary>
    /// Parses the text representing a single JSON value into an instance of a specified type.
    /// </summary>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <param name="json">The JSON text to parse.</param>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="returnType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid.
    ///
    /// -or-
    ///
    /// <paramref name="returnType"/> is not compatible with the JSON.
    ///
    /// -or-
    ///
    /// There is remaining data in the span beyond a single JSON value.</exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    /// <remarks>Using a UTF-16 span is not as efficient as using the
    /// UTF-8 methods since the implementation natively uses UTF-8.
    /// </remarks>
    public static object? Deserialize([StringSyntax("Json")] ReadOnlySpan<char> json, Type returnType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(json, (options ?? Options).GetTypeInfo(returnType));
    }

    #endregion

    #region Read.Utf8JsonReader

    /// <summary>
    /// Reads one JSON value (including objects or arrays) from the provided reader into a <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
    /// <param name="reader">The reader to read.</param>
    /// <param name="options">Options to control the serializer behavior during reading.</param>
    /// <exception cref="JsonException">
    /// The JSON is invalid,
    /// <typeparamref name="TValue"/> is not compatible with the JSON,
    /// or a value could not be read from the reader.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///   <paramref name="reader"/> is using unsupported options.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    /// <remarks>
    ///   <para>
    ///     If the <see cref="Utf8JsonReader.TokenType"/> property of <paramref name="reader"/>
    ///     is <see cref="JsonTokenType.PropertyName"/> or <see cref="JsonTokenType.None"/>, the
    ///     reader will be advanced by one call to <see cref="Utf8JsonReader.Read"/> to determine
    ///     the start of the value.
    ///   </para>
    ///
    ///   <para>
    ///     Upon completion of this method, <paramref name="reader"/> will be positioned at the
    ///     final token in the JSON value. If an exception is thrown, the reader is reset to
    ///     the state it was in when the method was called.
    ///   </para>
    ///
    ///   <para>
    ///     This method makes a copy of the data the reader acted on, so there is no caller
    ///     requirement to maintain data integrity beyond the return of this method.
    ///   </para>
    ///
    ///   <para>
    ///     The <see cref="JsonReaderOptions"/> used to create the instance of the <see cref="Utf8JsonReader"/> take precedence over the <see cref="JsonSerializerOptions"/> when they conflict.
    ///     Hence, <see cref="JsonReaderOptions.AllowTrailingCommas"/>, <see cref="JsonReaderOptions.MaxDepth"/>, and <see cref="JsonReaderOptions.CommentHandling"/> are used while reading.
    ///   </para>
    /// </remarks>
    public static TValue? Deserialize<TValue>(ref Utf8JsonReader reader, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(ref reader, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Reads one JSON value (including objects or arrays) from the provided reader into a <paramref name="returnType"/>.
    /// </summary>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <param name="reader">The reader to read.</param>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="options">Options to control the serializer behavior during reading.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="returnType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid,
    /// <paramref name="returnType"/> is not compatible with the JSON,
    /// or a value could not be read from the reader.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///   <paramref name="reader"/> is using unsupported options.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    /// <remarks>
    ///   <para>
    ///     If the <see cref="Utf8JsonReader.TokenType"/> property of <paramref name="reader"/>
    ///     is <see cref="JsonTokenType.PropertyName"/> or <see cref="JsonTokenType.None"/>, the
    ///     reader will be advanced by one call to <see cref="Utf8JsonReader.Read"/> to determine
    ///     the start of the value.
    ///   </para>
    ///
    ///   <para>
    ///     Upon completion of this method, <paramref name="reader"/> will be positioned at the
    ///     final token in the JSON value. If an exception is thrown, the reader is reset to
    ///     the state it was in when the method was called.
    ///   </para>
    ///
    ///   <para>
    ///     This method makes a copy of the data the reader acted on, so there is no caller
    ///     requirement to maintain data integrity beyond the return of this method.
    ///   </para>
    ///   <para>
    ///     The <see cref="JsonReaderOptions"/> used to create the instance of the <see cref="Utf8JsonReader"/> take precedence over the <see cref="JsonSerializerOptions"/> when they conflict.
    ///     Hence, <see cref="JsonReaderOptions.AllowTrailingCommas"/>, <see cref="JsonReaderOptions.MaxDepth"/>, and <see cref="JsonReaderOptions.CommentHandling"/> are used while reading.
    ///   </para>
    /// </remarks>
    public static object? Deserialize(ref Utf8JsonReader reader, Type returnType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Deserialize(ref reader, (options ?? Options).GetTypeInfo(returnType));
    }

    #endregion

    #region Write.ByteArray

    /// <summary>
    /// Converts the provided value into a <see cref="byte"/> array.
    /// </summary>
    /// <returns>A UTF-8 representation of the value.</returns>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static byte[] SerializeToUtf8Bytes<TValue>(TValue value, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.SerializeToUtf8Bytes(value, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Converts the provided value into a <see cref="byte"/> array.
    /// </summary>
    /// <returns>A UTF-8 representation of the value.</returns>
    /// <param name="value">The value to convert.</param>
    /// <param name="inputType">The type of the <paramref name="value"/> to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="inputType"/> is not compatible with <paramref name="value"/>.
    /// </exception>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="inputType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="inputType"/>  or its serializable members.
    /// </exception>
    public static byte[] SerializeToUtf8Bytes(object? value, Type inputType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.SerializeToUtf8Bytes(value,(options ?? Options).GetTypeInfo(inputType));
    }

    #endregion

    #region Write.Document

    /// <summary>
    /// Converts the provided value into a <see cref="JsonDocument"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <returns>A <see cref="JsonDocument"/> representation of the JSON value.</returns>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static JsonDocument SerializeToDocument<TValue>(TValue value, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.SerializeToDocument(value, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Converts the provided value into a <see cref="JsonDocument"/>.
    /// </summary>
    /// <returns>A <see cref="JsonDocument"/> representation of the value.</returns>
    /// <param name="value">The value to convert.</param>
    /// <param name="inputType">The type of the <paramref name="value"/> to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="inputType"/> is not compatible with <paramref name="value"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// <exception cref="ArgumentNullException">
    /// <paramref name="inputType"/> is <see langword="null"/>.
    /// </exception>
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="inputType"/>  or its serializable members.
    /// </exception>
    public static JsonDocument SerializeToDocument(object? value, Type inputType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.SerializeToDocument(value, (options ?? Options).GetTypeInfo(inputType));
    }

    #endregion

    #region Write.Element

    /// <summary>
    /// Converts the provided value into a <see cref="JsonElement"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <returns>A <see cref="JsonElement"/> representation of the JSON value.</returns>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static JsonElement SerializeToElement<TValue>(TValue value, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.SerializeToElement(value, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Converts the provided value into a <see cref="JsonElement"/>.
    /// </summary>
    /// <returns>A <see cref="JsonElement"/> representation of the value.</returns>
    /// <param name="value">The value to convert.</param>
    /// <param name="inputType">The type of the <paramref name="value"/> to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="inputType"/> is not compatible with <paramref name="value"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// <exception cref="ArgumentNullException">
    /// <paramref name="inputType"/> is <see langword="null"/>.
    /// </exception>
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="inputType"/>  or its serializable members.
    /// </exception>
    public static JsonElement SerializeToElement(object? value, Type inputType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.SerializeToElement(value, (options ?? Options).GetTypeInfo(inputType));
    }

    #endregion

    #region Write.Node

    /// <summary>
    /// Converts the provided value into a <see cref="JsonNode"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <returns>A <see cref="JsonNode"/> representation of the JSON value.</returns>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static JsonNode? SerializeToNode<TValue>(TValue value, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.SerializeToNode(value, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Converts the provided value into a <see cref="JsonNode"/>.
    /// </summary>
    /// <returns>A <see cref="JsonNode"/> representation of the value.</returns>
    /// <param name="value">The value to convert.</param>
    /// <param name="inputType">The type of the <paramref name="value"/> to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="inputType"/> is not compatible with <paramref name="value"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// <exception cref="ArgumentNullException">
    /// <paramref name="inputType"/> is <see langword="null"/>.
    /// </exception>
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="inputType"/>  or its serializable members.
    /// </exception>
    public static JsonNode? SerializeToNode(object? value, Type inputType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.SerializeToNode(value, (options ?? Options).GetTypeInfo(inputType));
    }

    #endregion

    #region Write.Pipe

    /// <summary>
    /// Converts the provided value to UTF-8 encoded JSON text and write it to the <see cref="System.IO.Pipelines.PipeWriter"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <param name="utf8Json">The UTF-8 <see cref="System.IO.Pipelines.PipeWriter"/> to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the write operation.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="utf8Json"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static Task SerializeAsync<TValue>(PipeWriter utf8Json, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.SerializeAsync(utf8Json, value, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)), cancellationToken);
    }

    /// <summary>
    /// Converts the provided value to UTF-8 encoded JSON text and write it to the <see cref="System.IO.Pipelines.PipeWriter"/>.
    /// </summary>
    /// <param name="utf8Json">The UTF-8 <see cref="System.IO.Pipelines.PipeWriter"/> to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="inputType">The type of the <paramref name="value"/> to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the write operation.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="inputType"/> is not compatible with <paramref name="value"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="utf8Json"/> or <paramref name="inputType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="inputType"/>  or its serializable members.
    /// </exception>
    public static Task SerializeAsync(PipeWriter utf8Json, object? value, Type inputType, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.SerializeAsync(utf8Json, value, (options ?? Options).GetTypeInfo(inputType), cancellationToken);
    }

    #endregion

    #region Write.Stream

    /// <summary>
    /// Converts the provided value to UTF-8 encoded JSON text and write it to the <see cref="System.IO.Stream"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// <param name="utf8Json">The UTF-8 <see cref="System.IO.Stream"/> to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the write operation.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="utf8Json"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static Task SerializeAsync<TValue>(Stream utf8Json, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.SerializeAsync(utf8Json, value, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)), cancellationToken);
    }

    /// <summary>
    /// Converts the provided value to UTF-8 encoded JSON text and write it to the <see cref="System.IO.Stream"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <param name="utf8Json">The UTF-8 <see cref="System.IO.Stream"/> to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="utf8Json"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static void Serialize<TValue>(Stream utf8Json, TValue value, JsonSerializerOptions? options = null) {
        SystemJsonSerializer.Serialize(utf8Json, value, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Converts the provided value to UTF-8 encoded JSON text and write it to the <see cref="System.IO.Stream"/>.
    /// </summary>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// <param name="utf8Json">The UTF-8 <see cref="System.IO.Stream"/> to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="inputType">The type of the <paramref name="value"/> to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <param name="cancellationToken">The <see cref="System.Threading.CancellationToken"/> that can be used to cancel the write operation.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="inputType"/> is not compatible with <paramref name="value"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="utf8Json"/> or <paramref name="inputType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="inputType"/>  or its serializable members.
    /// </exception>
    public static Task SerializeAsync(Stream utf8Json, object? value, Type inputType, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
        return SystemJsonSerializer.SerializeAsync(utf8Json, value, (options ?? Options).GetTypeInfo(inputType), cancellationToken);
    }

    /// <summary>
    /// Converts the provided value to UTF-8 encoded JSON text and write it to the <see cref="System.IO.Stream"/>.
    /// </summary>
    /// <param name="utf8Json">The UTF-8 <see cref="System.IO.Stream"/> to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="inputType">The type of the <paramref name="value"/> to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="inputType"/> is not compatible with <paramref name="value"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="utf8Json"/> or <paramref name="inputType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="inputType"/>  or its serializable members.
    /// </exception>
    public static void Serialize(Stream utf8Json, object? value, Type inputType, JsonSerializerOptions? options = null) {
        SystemJsonSerializer.Serialize(utf8Json, value, (options ?? Options).GetTypeInfo(inputType));
    }

    #endregion

    #region Write.String

    /// <summary>
    /// Converts the provided value into a <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <returns>A <see cref="string"/> representation of the value.</returns>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    /// <remarks>Using a <see cref="string"/> is not as efficient as using UTF-8
    /// encoding since the implementation internally uses UTF-8. See also <see cref="SerializeToUtf8Bytes{TValue}(TValue, JsonSerializerOptions?)"/>
    /// and <see cref="SerializeAsync{TValue}(Stream, TValue, JsonSerializerOptions?, CancellationToken)"/>.
    /// </remarks>
    public static string Serialize<TValue>(TValue value, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Serialize(value, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Converts the provided value into a <see cref="string"/>.
    /// </summary>
    /// <returns>A <see cref="string"/> representation of the value.</returns>
    /// <param name="value">The value to convert.</param>
    /// <param name="inputType">The type of the <paramref name="value"/> to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="inputType"/> is not compatible with <paramref name="value"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="inputType"/>  or its serializable members.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="inputType"/> is <see langword="null"/>.
    /// </exception>
    /// <remarks>Using a <see cref="string"/> is not as efficient as using UTF-8
    /// encoding since the implementation internally uses UTF-8. See also <see cref="SerializeToUtf8Bytes(object?, Type, JsonSerializerOptions?)"/>
    /// and <see cref="SerializeAsync(Stream, object?, Type, JsonSerializerOptions?, CancellationToken)"/>.
    /// </remarks>
    public static string Serialize(object? value, Type inputType, JsonSerializerOptions? options = null) {
        return SystemJsonSerializer.Serialize(value, (options ?? Options).GetTypeInfo(inputType));
    }

    #endregion

    #region Write.Utf8JsonWriter

    /// <summary>
    /// Writes one JSON value (including objects or arrays) to the provided writer.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <param name="writer">The writer to write.</param>
    /// <param name="value">The value to convert and write.</param>
    /// <param name="options">Options to control the behavior.</param>
    /// <exception cref="ArgumentNullException">
    ///   <paramref name="writer"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public static void Serialize<TValue>(Utf8JsonWriter writer, TValue value, JsonSerializerOptions? options = null) {
        SystemJsonSerializer.Serialize(writer, value, (JsonTypeInfo<TValue>) (options ?? Options).GetTypeInfo(typeof(TValue)));
    }

    /// <summary>
    /// Writes one JSON value (including objects or arrays) to the provided writer.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value">The value to convert and write.</param>
    /// <param name="inputType">The type of the <paramref name="value"/> to convert.</param>
    /// <param name="options">Options to control the behavior.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="inputType"/> is not compatible with <paramref name="value"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="writer"/> or <paramref name="inputType"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="inputType"/> or its serializable members.
    /// </exception>
    public static void Serialize(Utf8JsonWriter writer, object? value, Type inputType, JsonSerializerOptions? options = null) {
        SystemJsonSerializer.Serialize(writer, value, (options ?? Options).GetTypeInfo(inputType));
    }

    #endregion

}
