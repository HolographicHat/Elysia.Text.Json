// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

// src/libraries/Common/src/SourceGenerators/TypeRef.cs

using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace SourceGenerators;

/// <summary>
/// An equatable value representing type identity.
/// </summary>
[DebuggerDisplay("Name = {Name}")]
internal sealed class TypeRef(ITypeSymbol type) : IEquatable<TypeRef> {

    public string Name { get; } = type.Name;

    public string? Namespace { get; } = type.ContainingNamespace.IsGlobalNamespace ? null : type.ContainingNamespace.ToString();

    /// <summary>
    /// Fully qualified assembly name, prefixed with "global::", e.g. global::System.Numerics.BigInteger.
    /// </summary>
    public string FullyQualifiedName { get; } = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

    public ImmutableEquatableArray<TypeRef> ContainingTypes { get; } = GetContainingTypes(type.ContainingType).Reverse().ToImmutableEquatableArray();

    public bool IsRecord { get; } = type.IsRecord;

    public bool IsValueType { get; } = type.IsValueType;

    public TypeKind TypeKind { get; } = type.TypeKind;

    public SpecialType SpecialType { get; } = type.OriginalDefinition.SpecialType;

    public bool CanBeNull => !IsValueType || SpecialType is SpecialType.System_Nullable_T;

    public string Modifier {
        get {
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (IsRecord && IsValueType) {
                return "record struct";
            }
            return IsRecord ? "record" : IsValueType ? "struct" : "class";
        }
    }

    public bool Equals(TypeRef? other) => other != null && FullyQualifiedName == other.FullyQualifiedName;

    public override bool Equals(object? obj) => Equals(obj as TypeRef);

    public override int GetHashCode() => FullyQualifiedName.GetHashCode();

    private static IEnumerable<TypeRef> GetContainingTypes(ITypeSymbol type) {
        while (type != null) {
            yield return new TypeRef(type);
            type = type.ContainingType;
        }
    }

}
