---
sidebar_position: 1
---

# Boolean

Describes the possible expectations for boolean values.

## Equality

You can verify, that the `bool` is equal to another one or not:
```csharp
bool subject = false;

await Expect.That(subject).Should().Be(false);
await Expect.That(subject).Should().NotBe(true);
```

## True / False

You can verify, that the `bool` is `true` or `false`
```csharp
await Expect.That(false).Should().BeFalse();
await Expect.That(true).Should().BeTrue();
```

The negation is only available for nullable booleans:
```csharp
bool? subject = null;

await Expect.That(subject).Should().NotBeFalse()
  .Because("it could be true or null");
await Expect.That(subject).Should().NotBeTrue()
  .Because("it could be false or null");
```

## Implication

You can verify, that `a` implies `b` (*find [here](https://mathworld.wolfram.com/Implies.html) a mathematical explanation*):
```csharp
bool a = false;
bool b = true;

await Expect.That(a).Should().Imply(b);
```
