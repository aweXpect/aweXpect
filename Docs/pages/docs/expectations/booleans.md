---
sidebar_position: 1
---

# Booleans

Describes the possible expectations for boolean values.

```csharp
bool subject = false;

await Expect.That(subject).Should().BeFalse().Because("I expect it");
await Expect.That(subject).Should().BeTrue();
await Expect.That(subject).Should().Be(otherBoolean);
await Expect.That(subject).Should().NotBe(false);
```

In addition, nullable booleans have the following expectations:
```csharp
bool? subject = false;

await Expect.That(subject).Should().NotBeFalse();
await Expect.That(subject).Should().NotBeTrue();
```

## Implication
*Find [here](https://mathworld.wolfram.com/Implies.html) a technical explanation*:
```csharp
bool subject = false;
bool anotherBoolean = true;
await Expect.That(subject).Should().Imply(anotherBoolean);
```
