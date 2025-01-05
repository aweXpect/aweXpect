---
sidebar_position: 17
---

# JSON

Describes the possible expectations for working with [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json).


## String comparison as JSON

You can compare two strings for JSON equivalency:

```csharp
string subject = "{\"foo\":{\"bar\":[1,2,3]}}";
string expected = """
                  {
                    "foo": {
                      "bar": [ 1, 2, 3 ]
                    }
                  }
                  """;

await Expect.That(subject).Should().Be(expected).AsJson();
```


## Validation

You can verify, that a string is valid JSON.
```csharp
string subject = "{\"foo\": 2}";

await Expect.That(subject).Should().BeValidJson();
```
This verifies that the string can be parsed by [`JsonDocument.Parse`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsondocument.parse) without exceptions.

You can also specify the [`JsonDocumentOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsondocumentoptions):
```csharp
string subject = "{\"foo\": 2}";

await Expect.That(subject).Should().BeValidJson(o => o with {CommentHandling = JsonCommentHandling.Disallow});
```

You can also add additional expectations on the [`JsonElement`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonelement) created when parsing the subject:
```csharp
string subject = "{\"foo\": 2}";

await Expect.That(subject).Should().BeValidJson().Which(j => j.Should().HaveCount(1));
```


## `JsonElement`

You can verify, that the `JsonElement` has the expected number of items:
```csharp
JsonElement subject = JsonDocument.Parse("[1,2]").RootElement;

await Expect.That(subject).Should().HaveCount(2);
await Expect.That(subject).Should().NotHaveCount(3);
```

This works for both, arrays and objects, but fails for all other JSON types.
