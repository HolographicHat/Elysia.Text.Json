using System.ComponentModel;
using System.Runtime.CompilerServices;

using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace Elysia.Text.Json;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class HttpContentExtension {

    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod)]
    private static extern ValueTask<Stream> GetContentStreamAsync(
        [UnsafeAccessorType("System.Net.Http.Json.HttpContentJsonExtensions, System.Net.Http.Json")] object? c,
        HttpContent content,
        CancellationToken cancellationToken
    );

    extension<T>(HttpContent content) {

        /// <summary>Reads the HTTP content and returns the value that results from deserializing the content as JSON in an asynchronous operation.</summary>
        /// <param name="options">Options to control the behavior during deserialization.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <typeparam name="T">The target type to deserialize to.</typeparam>
        /// <exception cref="T:System.OperationCanceledException">The cancellation token was canceled. This exception is stored into the returned task.</exception>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<T?> ReadFromJsonAsync(JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) {
            await using var contentStream = await GetContentStreamAsync(null, content, cancellationToken);
            return await JsonSerializer.DeserializeAsync<T>(contentStream, options, cancellationToken);
        }

    }

}
