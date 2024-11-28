---
sidebar_position: 3
---

# Number

Describes the possible expectations for numbers.

## Equality

You can verify, that the number is equal to another one or not:
```csharp
int subject = 42;

await Expect.That(subject).Should().Be(42);
```

You can also specify a tolerance:
```csharp
double subject = 42.1;

await Expect.That(subject).Should().Be(42).Within(0.2)
  .Because("we accept values between 41.8 and 42.2 (42 ± 0.2)");
```

## One of

You can verify, that the number is one of many alternatives:
```csharp
int subject = 42;

await Expect.That(subject).Should().BeOneOf(40, 42, 44);
```

You can also specify a tolerance:
```csharp
double subject = 42.1;

await Expect.That(subject).Should().BeOneOf(40, 42, 44).Within(0.2)
  .Because("we accept values between 39.8 and 40.2 or 41.8 and 42.2 or 43.8 and 44.2");
```

## Greater than

You can verify, that the number is greater than (or equal to) another number:
```csharp
int subject = 42;

await Expect.That(subject).Should().BeGreaterThan(41);
await Expect.That(subject).Should().BeGreaterThanOrEqualTo(42);
```

You can also specify a tolerance:
```csharp
double subject = 41.9;

await Expect.That(subject).Should().BeGreaterThan(42).Within(0.2)
  .Because("we accept values greater than 41.8 (42 ± 0.2)");
```

## Less than

You can verify, that the number is less than (or equal to) another number:
```csharp
int subject = 42;

await Expect.That(subject).Should().BeLessThanOrEqualTo(42);
await Expect.That(subject).Should().BeLessThan(43);
```

You can also specify a tolerance:
```csharp
double subject = 42.1;

await Expect.That(subject).Should().BeLessThan(42).Within(0.2)
  .Because("we accept values less than 42.2 (42 ± 0.2)");
```

## Positive / negative

You can verify, that the number is positive or negative:
```csharp
await Expect.That(42).Should().BePositive();
await Expect.That(-3).Should().BeNegative();
```
*Note: these expectations are only available for signed numbers.*

## NaN

For floating point numbers you can verify, that the number is `NaN` or not:

```csharp
await Expect.That(float.NaN).Should().BeNaN();
await Expect.That(42.0).Should().NotBeNaN();
```

## Infinity

For floating point numbers you can verify, that the number is finite or infinite:
```csharp
await Expect.That(float.PositiveInfinity).Should().BeInfinite();
await Expect.That(42.0).Should().BeFinite();
```
