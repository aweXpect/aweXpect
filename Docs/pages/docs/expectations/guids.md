---
sidebar_position: 5
---

# Guids

Describes the possible expectations for `Guid` values.

```csharp
Guid subject = Guid.Empty;

await Expect.That(subject).Should().Be(Guid.Empty).Because("I expect it");
await Expect.That(subject).Should().NotBe(Guid.Parse("5c01d9d2-66f7-4782-8c14-e54eae9aaacc"));

await Expect.That(subject).Should().BeEmpty();
await Expect.That(Guid.NewGuid()).Should().NotBeEmpty();
```
