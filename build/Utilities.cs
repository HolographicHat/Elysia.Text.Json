using System;
using Microsoft.Build.Framework;

namespace Elysia.Text.Json.BuildTask;

internal static class Utilities {

    /// <summary>
    /// Convert a task item metadata to bool. Throw an exception if the string is badly formed and can't
    /// be converted.
    /// 
    /// If the metadata is not found, then set metadataFound to false and then return false.
    /// </summary>
    /// <param name="item">The item that contains the metadata.</param>
    /// <param name="itemMetadataName">The name of the metadata.</param>
    /// <returns>The resulting boolean value.</returns>
    internal static bool TryConvertItemMetadataToBool(ITaskItem item, string itemMetadataName) {
        var metadataValue = item.GetMetadata(itemMetadataName);
        if (string.IsNullOrEmpty(metadataValue)) {
            return false;
        }
        try {
            return ConvertStringToBool(metadataValue);
        } catch (ArgumentException) {
            throw new ArgumentException($"Item \"{item.ItemSpec}\" has attribute \"{itemMetadataName}\" with value \"{metadataValue}\" that could not be converted to \"bool\".");
        }
    }

    /// <summary>
    /// Converts a string to a bool.  We consider "true/false", "on/off", and 
    /// "yes/no" to be valid boolean representations in the XML.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>Boolean true or false, corresponding to the string.</returns>
    internal static bool ConvertStringToBool(string value) {
        if (ValidBooleanTrue(value)) {
            return true;
        }
        if (ValidBooleanFalse(value)) {
            return false;
        }
        // Unsupported boolean representation.
        throw new ArgumentException($"The string \"{value}\" cannot be converted to a boolean (true/false) value.");
    }

    /// <summary>
    /// Returns true if the string can be successfully converted to a bool,
    /// such as "on" or "yes"
    /// </summary>
    internal static bool CanConvertStringToBool(string parameterValue) =>
        ValidBooleanTrue(parameterValue) || ValidBooleanFalse(parameterValue);

    /// <summary>
    /// Returns true if the string represents a valid MSBuild boolean true value,
    /// such as "on", "!false", "yes"
    /// </summary>
    private static bool ValidBooleanTrue(string parameterValue) =>
        string.Compare(parameterValue, "true", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(parameterValue, "on", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(parameterValue, "yes", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(parameterValue, "!false", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(parameterValue, "!off", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(parameterValue, "!no", StringComparison.OrdinalIgnoreCase) == 0;

    /// <summary>
    /// Returns true if the string represents a valid MSBuild boolean false value,
    /// such as "!on" "off" "no" "!true"
    /// </summary>
    private static bool ValidBooleanFalse(string parameterValue) =>
        string.Compare(parameterValue, "false", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(parameterValue, "off", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(parameterValue, "no", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(parameterValue, "!true", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(parameterValue, "!on", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(parameterValue, "!yes", StringComparison.OrdinalIgnoreCase) == 0;

}
