---
sidebar_position: 8
---

# DateOnly / TimeOnly

Describes the possible expectations for `DateOnly` and `TimeOnly`.

## Equality

You can verify, that the `DateOnly` or `TimeOnly` is equal to another one or not:

```csharp
DateOnly subjectA = new DateOnly(2024, 12, 24);

await Expect.That(subjectA).Is(new DateOnly(2024, 12, 24));
await Expect.That(subjectA).IsNot(new DateOnly(2024, 12, 23));

TimeOnly subjectB = new TimeOnly(14, 15, 16);

await Expect.That(subjectB).Is(new TimeOnly(14, 15, 16));
await Expect.That(subjectB).IsNot(new TimeOnly(13, 15, 16));
```

You can also specify a tolerance:

```csharp
DateOnly subjectA = new DateOnly(2024, 12, 24);

await Expect.That(subjectA).Is(new DateOnly(2024, 12, 23)).Within(TimeSpan.FromDays(1))
  .Because("we accept values between 2024-12-22 and 2024-12-24");

TimeOnly subjectB = new TimeOnly(14, 15, 16);

await Expect.That(subjectB).Is(new TimeOnly(14, 15, 17)).Within(TimeSpan.FromSeconds(1))
  .Because("we accept values between 14:15:16 and 14:15:18");
```

## After

You can verify, that the `DateOnly` or `TimeOnly` is (on or) after another value

```csharp
DateOnly subjectA = DateOnly.FromDateTime(DateTime.Now);

await Expect.That(subjectA).IsAfter(new DateOnly(2024, 1, 1));
await Expect.That(subjectA).IsOnOrAfter(new DateOnly(2024, 1, 1));

TimeOnly subjectB = TimeOnly.FromDateTime(DateTime.Now);

await Expect.That(subjectB).IsAfter(new TimeOnly(0, 0, 0));
await Expect.That(subjectB).IsOnOrAfter(new TimeOnly(0, 0, 0));
```

You can also specify a tolerance:

```csharp
DateOnly subjectA = DateOnly.FromDateTime(DateTime.Now);

await Expect.That(subjectA).IsAfter(DateOnly.FromDateTime(DateTime.Now)).Within(TimeSpan.FromDays(1));

TimeOnly subjectB = TimeOnly.FromDateTime(DateTime.Now);

await Expect.That(subjectB).IsAfter(TimeOnly.FromDateTime(DateTime.Now)).Within(TimeSpan.FromSeconds(1));
```

## Before

You can verify, that the `DateOnly` or `TimeOnly` is (on or) before another value

```csharp
DateOnly subjectA = DateOnly.FromDateTime(DateTime.Now);

await Expect.That(subjectA).IsBefore(new DateOnly(2124, 12, 31));
await Expect.That(subjectA).IsOnOrBefore(new DateOnly(2124, 12, 31));

TimeOnly subjectB = TimeOnly.FromDateTime(DateTime.Now);

await Expect.That(subjectB).IsBefore(new TimeOnly(23, 59, 59));
await Expect.That(subjectB).IsOnOrBefore(new TimeOnly(23, 59, 59));
```

You can also specify a tolerance:

```csharp
DateOnly subjectA = DateOnly.FromDateTime(DateTime.Now);

await Expect.That(subjectA).IsBefore(DateOnly.FromDateTime(DateTime.Now)).Within(TimeSpan.FromDays(1));

TimeOnly subjectB = TimeOnly.FromDateTime(DateTime.Now);

await Expect.That(subjectB).IsBefore(TimeOnly.FromDateTime(DateTime.Now)).Within(TimeSpan.FromSeconds(1));
```

## Properties

You can verify, the properties of the `DateTime`:

```csharp
DateOnly subject = new DateOnly(2024, 12, 31);

await Expect.That(subject).HasYear(2024);
await Expect.That(subject).HasMonth(12);
await Expect.That(subject).HasDay(31);
```

You can verify, the properties of the `TimeOnly`:

```csharp
TimeOnly subject = new TimeOnly(15, 16, 17, 189);

await Expect.That(subject).HasHour(15);
await Expect.That(subject).HasMinute(16);
await Expect.That(subject).HasSecond(17);
await Expect.That(subject).HasMillisecond(189);
```
