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

await Expect.That(subject).Should().BeValidJson().Which(j => j.Should().Match(new{foo = 2}));
```


## `JsonElement`

### Match

You can verify, that the `JsonElement` matches an expected object:
```csharp
JsonElement subject = JsonDocument.Parse("{\"foo\": 1, \"bar\": \"baz\"}").RootElement;

await Expect.That(subject).Should().Match(new{foo = 1});
await Expect.That(subject).Should().MatchExactly(new{foo = 1, bar = "baz"});
```

You can verify, that the `JsonElement` matches an expected array:
```csharp
JsonElement subject = JsonDocument.Parse("[1,2,3]").RootElement;

await Expect.That(subject).Should().Match([1, 2]);
await Expect.That(subject).Should().MatchExactly([1, 2, 3]);
```

You can also verify, that the `JsonElement` matches a primitive type:
```csharp
await Expect.That(JsonDocument.Parse("\"foo\"").RootElement).Should().Match("foo");
await Expect.That(JsonDocument.Parse("42.3").RootElement).Should().Match(42.3);
await Expect.That(JsonDocument.Parse("true").RootElement).Should().Match(true);
await Expect.That(JsonDocument.Parse("null").RootElement).Should().Match(null);
```

### Be object

You can verify that a `JsonElement` is a JSON object that satisfy some expectations:
```csharp
JsonElement subject = JsonDocument.Parse("{\"foo\": 1, \"bar\": \"baz\"}").RootElement;

await Expect.That(subject).Should().BeObject(o => o
    .With("foo").Matching(1).And
    .With("bar").Matching("baz"));
```

You can also verify that a property is another object recursively:
```csharp
JsonElement subject = JsonDocument.Parse("{\"foo\": {\"bar\": \"baz\"}}").RootElement;

await Expect.That(subject).Should().BeObject(o => o
    .With("foo").AnObject(i => i
        .With("bar").Matching("baz")));
```

You can also verify that a property is an array:
```csharp
JsonElement subject = JsonDocument.Parse("{\"foo\": [1, 2]}").RootElement;

await Expect.That(subject).Should().BeObject(o => o
    .With("foo").AnArray(a => a.WithElements(1, 2)));
```

You can also verify the number of properties in a JSON object:
```csharp
JsonElement subject = JsonDocument.Parse("{\"foo\": 1, \"bar\": \"baz\"}").RootElement;

await Expect.That(subject).Should().BeObject(o => o.With(2).Properties());
```

### Be array

You can verify that a `JsonElement` is a JSON array that satisfy some expectations:
```csharp
JsonElement subject = JsonDocument.Parse("[\"foo\",\"bar\"]").RootElement;

await Expect.That(subject).Should().BeArray(a => a
    .At(0).Matching("foo").And
    .At(1).Matching("bar"));
```

You can also verify the number of elements in a JSON array:
```csharp
JsonElement subject = JsonDocument.Parse("[1, 2, 3]").RootElement;

await Expect.That(subject).Should().BeArray(o => o.With(3).Elements());
```

You can also directly match the expected values of an array:
```csharp
JsonElement subject = JsonDocument.Parse("[\"foo\",\"bar\"]").RootElement;

await Expect.That(subject).Should().BeArray(a => a
    .WithElements("foo", "bar"));
```

You can also match sub-arrays recursively (add `null` to skip an element):
```csharp
JsonElement subject = JsonDocument.Parse("[[0,1,2],[1,2,3],[2,3,4],[3,4,5,6]]").RootElement;

await Expect.That(subject).Should().BeArray(a => a
    .WithArrays(
        i => i.WithElements(0,1,2),
        i => i.At(0).Matching(1).And.At(2).Matching(3),
        null,
        i => i.With(4).Elements()
    ));
```

You can also match objects recursively (add `null` to skip an element):
```csharp
JsonElement subject = JsonDocument.Parse(
	"""
	[
	  {"foo":1},
	  {"bar":2},
	  {"bar": null, "baz": true}
	]
	""").RootElement;
await Expect.That(subject).Should().BeArray(a => a
	.WithObjects(
		i => i.With("foo").Matching(1),
		null,
		i => i.With(2).Properties()
	));
```