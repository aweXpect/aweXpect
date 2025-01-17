---
sidebar_position: 7
---

# DateTime / DateTimeOffset

Describes the possible expectations for `DateTime` and `DateTimeOffset`.

## Equality

You can verify, that the `DateTime` or `DateTimeOffset` is equal to another one or not:
```csharp
DateTime subject1 = new DateTime(2024, 12, 24);

await Expect.That(subject1).Is(new DateTime(2024, 12, 24));

DateTimeOffset subject2 = new DateTimeOffset(2024, 12, 24, 13, 15, 0, TimeSpan.FromHours(2));

await Expect.That(subject2).Is(new DateTimeOffset(2024, 12, 24, 13, 15, 0, TimeSpan.FromHours(2)));
```

You can also specify a tolerance:
```csharp
DateTime subject1 = new DateTime(2024, 12, 24);

await Expect.That(subject1).Is(new DateTime(2024, 12, 23)).Within(TimeSpan.FromDays(1))
  .Because("we accept values between 2024-12-23 and 2024-12-25");

DateTimeOffset subject2 = new DateTimeOffset(2024, 12, 24, 13, 15, 0, TimeSpan.FromHours(2));

await Expect.That(subject2).Is(new DateTimeOffset(2024, 12, 24, 13, 5, 0, TimeSpan.FromHours(2))).Within(TimeSpan.FromMinutes(10))
  .Because("we accept values between 2024-12-24T12:55:00+2:00 and 2024-12-24T13:15:00+2:00");
```


## After

You can verify, that the `DateTime` or `DateTimeOffset` is (on or) after another value:
```csharp
DateTime subject1 = DateTime.Now;

await Expect.That(subject1).IsAfter(new DateTime(2024, 1, 1));
await Expect.That(subject1).IsOnOrAfter(new DateTime(2024, 1, 1));

DateTimeOffset subject2 = DateTimeOffset.Now;

await Expect.That(subject2).IsAfter(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(2)));
await Expect.That(subject2).IsOnOrAfter(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(2)));
```

You can also specify a tolerance:
```csharp
DateTime subject = DateTime.Now;

await Expect.That(subject).IsAfter(DateTime.Now).Within(TimeSpan.FromSeconds(1))
  .Because("it should have taken less than one second");
```

## Before

You can verify, that the `DateTime` or `DateTimeOffset` is (on or) before another value:
```csharp
DateTime subject1 = DateTime.Now;

await Expect.That(subject1).IsBefore(new DateTime(2124, 12, 31));
await Expect.That(subject1).IsOnOrBefore(new DateTime(2124, 12, 31));

DateTimeOffset subject2 = DateTimeOffset.Now;

await Expect.That(subject2).IsBefore(new DateTimeOffset(2124, 12, 31, 23, 59, 59, TimeSpan.FromHours(2)));
await Expect.That(subject2).IsOnOrBefore(new DateTimeOffset(2124, 12, 31, 23, 59, 59, TimeSpan.FromHours(2)));
```

You can also specify a tolerance:
```csharp
DateTime subject = DateTime.Now;

await Expect.That(subject).IsOnOrBefore(DateTime.Now).Within(TimeSpan.FromSeconds(1))
  .Because("it should have taken less than one second");
```

## Properties

You can verify, the properties of `DateTime` or `DateTimeOffset`:
```csharp
DateTime subject = new DateTime(2024, 12, 31, 15, 16, 17, 189, DateTimeKind.Utc);
// or
DateTimeOffset subject = new DateTimeOffset(2024, 12, 31, 15, 16, 17, 189, TimeSpan.FromMinutes(90));

await Expect.That(subject).HasYear(2024);
await Expect.That(subject).HasMonth(12);
await Expect.That(subject).HasDay(31);
await Expect.That(subject).HasHour(15);
await Expect.That(subject).HasMinute(16);
await Expect.That(subject).HasSecond(17);
await Expect.That(subject).HasMillisecond(189);
```

For `DateTime` you can also verify the `Kind` property:
```csharp
await Expect.That(subject).HasKind(DateTimeKind.Utc);
```

For `DateTimeOffset` you can also verify the `Offset` property:
```csharp
await Expect.That(subject).HasOffset(TimeSpan.FromMinutes(90));
```


## Default Tolerance

In Windows the `DateTime` resolution is [about 10 to 15 milliseconds](https://stackoverflow.com/q/3140826/4003370), so comparing them as exact values might result in brittle tests.
Therefore, it is possible to specify a default tolerance that is used for all `DateTime`, `DateTimeOffset`, `DateOnly`, `TimeOnly` and `TimeSpan` comparisons (unless an explicit tolerance is given):
```csharp
IDisposable lifetime = Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Set(15.Milliseconds());
```
