---
sidebar_position: 17
---

# JSON

Describes the possible expectations for working with [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json).

## JsonElement

You can verify, that the `JsonElement` has the expected number of items:
```csharp
JsonElement subject = JsonDocument.Parse("[1,2]").RootElement;

await Expect.That(subject).Should().HaveCount(2);
```

This works for both, arrays and objects, but fails for all other JSON types.


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
