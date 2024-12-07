---
sidebar_position: 6
---

# TimeSpan

Describes the possible expectations for `TimeSpan`.


## Equality

You can verify, that the `TimeSpan` is equal to another one or not:
```csharp
TimeSpan subject = TimeSpan.FromSeconds(42);

await Expect.That(subject).Should().Be(TimeSpan.FromSeconds(42));
await Expect.That(subject).Should().NotBe(TimeSpan.FromSeconds(43));
```

You can also specify a tolerance:
```csharp
TimeSpan subject = TimeSpan.FromSeconds(42);

await Expect.That(subject).Should().Be(TimeSpan.FromSeconds(43)).Within(TimeSpan.FromSeconds(1))
  .Because("we accept values between 0:41 and 0:43");
```

## Greater than

You can verify, that the `TimeSpan` is greater than (or equal to) another number:
```csharp
TimeSpan subject = TimeSpan.FromSeconds(42);

await Expect.That(subject).Should().BeGreaterThan(TimeSpan.FromSeconds(41));
await Expect.That(subject).Should().BeGreaterThanOrEqualTo(TimeSpan.FromSeconds(42));
```

You can also specify a tolerance:
```csharp
TimeSpan subject = TimeSpan.FromSeconds(41);

await Expect.That(subject).Should().BeGreaterThan(42).Within(TimeSpan.FromSeconds(2))
  .Because("we accept values greater than 0:40 (0:42 ± 2s)");
```

## Less than

You can verify, that the `TimeSpan` is less than (or equal to) another number:
```csharp
TimeSpan subject = TimeSpan.FromSeconds(42);

await Expect.That(subject).Should().BeLessThan(TimeSpan.FromSeconds(43));
await Expect.That(subject).Should().BeLessThanOrEqualTo(TimeSpan.FromSeconds(42));
```

You can also specify a tolerance:
```csharp
TimeSpan subject = TimeSpan.FromSeconds(43);

await Expect.That(subject).Should().BeLessThan(42).Within(TimeSpan.FromSeconds(2))
  .Because("we accept values less than 0:44 (0:42 ± 2s)");
```

## Positive / negative

You can verify, that the `TimeSpan` is positive or negative:
```csharp
await Expect.That(TimeSpan.FromSeconds(42)).Should().BePositive();
await Expect.That(TimeSpan.FromSeconds(-3)).Should().BeNegative();
```
