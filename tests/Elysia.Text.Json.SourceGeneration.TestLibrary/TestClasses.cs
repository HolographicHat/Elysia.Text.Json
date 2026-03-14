using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Elysia.Text.Json;

using JsonSerializer = Elysia.Text.Json.JsonSerializer;

[JsonClass]
public sealed partial class TestRoot;

namespace Elysia.Text.Json.SourceGeneration.TestLibrary {

    [JsonClass]
    public sealed partial class TestClasses;

    [JsonClass]
    public sealed partial record TestRecord;

    namespace TestNamespace {
        
        public partial class TestClasses {

            [JsonClass(ExtraTypes = [typeof(Dictionary<string, TestStruct>)])]
            public partial struct TestStruct;

            [JsonClass]
            public partial record struct TestRecordStruct {

                public const string Test = "1234";
                
                [JsonClass(WithExtensionData = true)]
                public partial struct TestStruct2;

            }

        }
        
    }

}

namespace Elysia.Text.Json.SourceGeneration.TestLibrary {

    partial class TestClasses;

    partial record TestRecord;

    namespace TestNamespace {
        
        partial class TestClasses {
    
            partial struct TestStruct;

            partial record struct TestRecordStruct;

        }

    }

}

#if !IS_PUBLISHING
[JsonSerializable(typeof(int))]
#endif
[GlobalJsonSerializerContext]
[JsonSourceGenerationOptions(
    NumberHandling = JsonNumberHandling.AllowReadingFromString,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
    PropertyNameCaseInsensitive = true
)]
internal partial class TestJsonSerializerContext : JsonSerializerContext {

    static TestJsonSerializerContext() {
        Default = new TestJsonSerializerContext(new JsonSerializerOptions(Default.GeneratedSerializerOptions!) {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        });
    }

#pragma warning disable CA2255
    [ModuleInitializer]
#pragma warning restore CA2255
    internal static void Init() {
#if IS_PUBLISHING
        var options = Default.Options;
#else
        var options = new JsonSerializerOptions(Default.Options) {
#pragma warning disable IL2026, IL3050
            TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
#pragma warning restore IL2026, IL3050
        };
#endif
        JsonSerializer.Options = options;
    }

}
