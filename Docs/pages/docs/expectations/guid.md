---
sidebar_position: 8
---

# Guid

Describes the possible expectations for `Guid` values.

## Equality

You can verify, that the `Guid` is equal to another one or not:
```csharp
Guid subject = Guid.Parse("5c01d9d2-66f7-4782-8c14-e54eae9aaacc");

await Expect.That(subject).Should().Be(Guid.Parse("5c01d9d2-66f7-4782-8c14-e54eae9aaacc"))
  .Because("they are the same");
await Expect.That(subject).Should().NotBe(Guid.Parse("cdd7a485-40a1-4bba-bb8b-d0e903704b02"))
  .Because("they differ");
```

## Empty

You can verify, that the `Guid` is empty or not:
```csharp
await Expect.That(Guid.Empty).Should().BeEmpty();
await Expect.That(Guid.NewGuid()).Should().NotBeEmpty();
```
