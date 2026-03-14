// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

// src/libraries/Common/src/SourceGenerators/ImmutableEquatableArray.cs

using System.Collections;

namespace SourceGenerators;

/// <summary>
/// Provides an immutable list implementation which implements sequence equality.
/// </summary>
internal sealed class ImmutableEquatableArray<T> : IEquatable<ImmutableEquatableArray<T>>, IReadOnlyList<T> where T : IEquatable<T> {

    public static ImmutableEquatableArray<T> Empty { get; } = new([]);

    private readonly T[] _values;
    public T this[int index] => _values[index];
    public int Count => _values.Length;

    // ReSharper disable once ConvertToPrimaryConstructor
    public ImmutableEquatableArray(IEnumerable<T> values) {
        _values = values.ToArray();
    }

    public bool Equals(ImmutableEquatableArray<T>? other) {
        return other != null && ((ReadOnlySpan<T>) _values).SequenceEqual(other._values);
    }

    public override bool Equals(object? obj) {
        return obj is ImmutableEquatableArray<T> other && Equals(other);
    }

    public override int GetHashCode() {
        var hash = 0;
        foreach (var value in _values) {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            hash = HashHelpers.Combine(hash, value is null ? 0 : value.GetHashCode());
        }
        return hash;
    }

    public Enumerator GetEnumerator() => new(_values);
    
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)_values).GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();

    public struct Enumerator {
        
        private readonly T[] _values;
        private int _index;

        internal Enumerator(T[] values) {
            _values = values;
            _index = -1;
        }

        public bool MoveNext() {
            var newIndex = _index + 1;
            if ((uint)newIndex < (uint)_values.Length) {
                _index = newIndex;
                return true;
            }
            return false;
        }

        public readonly T Current => _values[_index];

    }

}

internal static class ImmutableEquatableArray {

    public static ImmutableEquatableArray<T> ToImmutableEquatableArray<T>(this IEnumerable<T> values) where T : IEquatable<T> => new(values);

}

// src/libraries/System.Private.CoreLib/src/System/Numerics/Hashing/HashHelpers.cs

file static class HashHelpers {

    public static int Combine(int h1, int h2) {
        // RyuJIT optimizes this to use the ROL instruction
        // Related GitHub pull request: https://github.com/dotnet/coreclr/pull/1830
        var rol5 = ((uint)h1 << 5) | ((uint)h1 >> 27);
        return ((int)rol5 + h1) ^ h2;
    }

}
