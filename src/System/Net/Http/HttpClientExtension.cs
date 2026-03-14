using System.ComponentModel;
using System.Runtime.CompilerServices;

using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace Elysia.Text.Json;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class HttpClientExtension {

    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod)]
    private static extern Task<TValue?> FromJsonAsyncCore<TValue, TJsonOptions>(
        [UnsafeAccessorType("System.Net.Http.Json.HttpClientJsonExtensions, System.Net.Http.Json")] object? c,
        Func<HttpClient, Uri?, CancellationToken, Task<HttpResponseMessage>> getMethod,
        HttpClient client,
        Uri? requestUri,
        Func<Stream, TJsonOptions, CancellationToken, ValueTask<TValue?>> deserializeMethod,
        TJsonOptions jsonOptions,
        CancellationToken cancellationToken
    );

    // ReSharper disable once InconsistentNaming
    private static readonly Func<HttpClient, Uri?, CancellationToken, Task<HttpResponseMessage>> s_getAsync =
        static (client, uri, cancellation) => client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellation);

    // ReSharper disable once InconsistentNaming
    private static readonly Func<Stream, (Type, JsonSerializerOptions?), CancellationToken, ValueTask<object?>> s_deserializeAsync =
        static (stream, pair, cancellation) => JsonSerializer.DeserializeAsync(stream, pair.Item1, pair.Item2, cancellation);

    extension<T>(HttpClient client) {

        /// <summary>Sends a GET request to the specified Uri and returns the value that results from deserializing the response body as JSON in an asynchronous operation.</summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="options">Options to control the behavior during deserialization.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <typeparam name="T">The target type to deserialize to.</typeparam>
        /// <exception cref="T:System.OperationCanceledException">The cancellation token was canceled. This exception is stored into the returned task.</exception>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<T?> GetJsonAsync(string requestUri, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
            var uri = new Uri(requestUri, UriKind.RelativeOrAbsolute);
            return (T?) await FromJsonAsyncCore(null, s_getAsync, client, uri, s_deserializeAsync, (typeof(T), options), cancellationToken);
        }

        public async Task<T?> SendFromJsonAsync(HttpRequestMessage request) {
            using var rsp = await client.SendAsync(request);
            rsp.EnsureSuccessStatusCode();
            return await rsp.Content.ReadFromJsonAsync<T>();
        }

    }

}
