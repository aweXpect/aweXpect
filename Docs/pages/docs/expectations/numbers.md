---
sidebar_position: 3
---

# Numbers

Describes the possible expectations for numbers.

## Equality

For asserting whether a number is equal to another one, use:

```csharp
int subject = 42;
await Expect.That(subject).Should().Be(42);
```

You can also specify a tolerance:

```csharp
double subject = 42.1;
await Expect.That(subject).Should().Be(42).Within(0.2);
```

## Greater than / less than

You can also verify that a number is greater than or less than another value

```csharp
int subject = 42;
await Expect.That(subject).Should().BeGreaterThan(41);
await Expect.That(subject).Should().BeGreaterThanOrEqualTo(42);
await Expect.That(subject).Should().BeLessThanOrEqualTo(42);
await Expect.That(subject).Should().BeLessThan(43);
```

Also for these methods you can specify a tolerance, e.g.

```csharp
double subject = 42.0;
await Expect.That(subject).Should().BeLessThan(42).Within(0.1);
```

## Positive / negative

As a special case, to verify, if a number is positive or negative, you can use the following expectations.

```csharp
int subject = 42;
await Expect.That(subject).Should().BePositive();
await Expect.That(subject).Should().BeNegative();
```

*Note: these methods are only available for signed numbers!*

## Special cases for floating numbers

For `double` and `float` the following expectations are additionally available:

```csharp
double subject = double.NaN;
await Expect.That(subject).Should().BeNaN();

subject = double.PositiveInfinity;
await Expect.That(subject).Should().BeInfinite();

subject = 42;
await Expect.That(subject).Should().BeFinite();
```
