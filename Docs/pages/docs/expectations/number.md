---
sidebar_position: 4
---

# Number

Describes the possible expectations for numbers.

## Equality

You can verify, that the number is equal to another one or not:
```csharp
int subject = 42;

await Expect.That(subject).Is(42);
```

You can also specify a tolerance:
```csharp
double subject = 42.1;

await Expect.That(subject).Is(42).Within(0.2)
  .Because("we accept values between 41.8 and 42.2 (42 ± 0.2)");
```

## One of

You can verify, that the number is one of many alternatives:
```csharp
int subject = 42;

await Expect.That(subject).IsOneOf(40, 42, 44);
```

You can also specify a tolerance:
```csharp
double subject = 42.1;

await Expect.That(subject).IsOneOf(40, 42, 44).Within(0.2)
  .Because("we accept values between 39.8 and 40.2 or 41.8 and 42.2 or 43.8 and 44.2");
```

## Greater than

You can verify, that the number is greater than (or equal to) another number:
```csharp
int subject = 42;

await Expect.That(subject).IsGreaterThan(41);
await Expect.That(subject).IsGreaterThanOrEqualTo(42);
```

You can also specify a tolerance:
```csharp
double subject = 41.9;

await Expect.That(subject).IsGreaterThan(42).Within(0.2)
  .Because("we accept values greater than 41.8 (42 ± 0.2)");
```

## Less than

You can verify, that the number is less than (or equal to) another number:
```csharp
int subject = 42;

await Expect.That(subject).IsLessThanOrEqualTo(42);
await Expect.That(subject).IsLessThan(43);
```

You can also specify a tolerance:
```csharp
double subject = 42.1;

await Expect.That(subject).IsLessThan(42).Within(0.2)
  .Because("we accept values less than 42.2 (42 ± 0.2)");
```

## Positive / negative

You can verify, that the number is positive or negative:
```csharp
await Expect.That(42).IsPositive();
await Expect.That(-3).IsNegative();
```
*Note: these expectations are only available for signed numbers.*

## NaN

For floating point numbers you can verify, that the number is `NaN` or not:

```csharp
await Expect.That(float.NaN).IsNaN();
await Expect.That(42.0).IsNotNaN();
```

## Infinity

For floating point numbers you can verify, that the number is finite or infinite:
```csharp
await Expect.That(float.PositiveInfinity).IsInfinite();
await Expect.That(42.0).IsFinite();
```
