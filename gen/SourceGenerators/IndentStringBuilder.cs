using System.Diagnostics.CodeAnalysis;
using System.Text;

// ReSharper disable UnusedParameter.Global

namespace Elysia.Utilities;

internal class IndentStringBuilder {

    private readonly StringBuilder _builder = new ();

    private static readonly Dictionary<int, string> IndentCache = [];

    private const int SpacePerLevel = 4;

    private int _currentIndentLevel;

    private string Indent {
        get {
            if (!IndentCache.TryGetValue(_currentIndentLevel, out var indent)) {
                indent = new string(' ', _currentIndentLevel * SpacePerLevel);
                IndentCache[_currentIndentLevel] = indent;
            }
            return indent;
        }
    }

    public void AddIndentLevel() => _currentIndentLevel += 1;

    public void SubIndentLevel() => _currentIndentLevel -= 1;

    public void AppendLine(string text = "") {
        _builder.Append(Indent);
        _builder.AppendLine(text);
    }

    public void AppendLine(
        [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format,
        params object?[] args
    ) {
        _builder.Append(Indent);
        _builder.AppendFormat(format, args);
        _builder.AppendLine();
    }

    public override string ToString() => _builder.ToString();

}
