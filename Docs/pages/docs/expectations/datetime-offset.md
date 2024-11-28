---
sidebar_position: 6
---

# DateTime / DateTimeOffset

Describes the possible expectations for `DateTime` and `DateTimeOffset`.

## Equality

You can verify, that the `DateTime` or `DateTimeOffset` is equal to another one or not:
```csharp
DateTime subject1 = new DateTime(2024, 12, 24);

await Expect.That(subject1).Should().Be(new DateTime(2024, 12, 24));

DateTimeOffset subject2 = new DateTimeOffset(2024, 12, 24, 13, 15, 0, TimeSpan.FromHours(2));

await Expect.That(subject2).Should().Be(new DateTimeOffset(2024, 12, 24, 13, 15, 0, TimeSpan.FromHours(2)));
```

You can also specify a tolerance:
```csharp
DateTime subject1 = new DateTime(2024, 12, 24);

await Expect.That(subject1).Should().Be(new DateTime(2024, 12, 23)).Within(TimeSpan.FromDays(1))
  .Because("we accept values between 2024-12-23 and 2024-12-25");

DateTimeOffset subject2 = new DateTimeOffset(2024, 12, 24, 13, 15, 0, TimeSpan.FromHours(2));

await Expect.That(subject2).Should().Be(new DateTimeOffset(2024, 12, 24, 13, 5, 0, TimeSpan.FromHours(2))).Within(TimeSpan.FromMinutes(10))
  .Because("we accept values between 2024-12-24T12:55:00+2:00 and 2024-12-24T13:15:00+2:00");
```

## After

You can verify, that the `DateTime` or `DateTimeOffset` is (on or) after another value:
```csharp
DateTime subject1 = DateTime.Now;

await Expect.That(subject1).Should().BeAfter(new DateTime(2024, 1, 1));
await Expect.That(subject1).Should().BeOnOrAfter(new DateTime(2024, 1, 1));

DateTimeOffset subject2 = DateTimeOffset.Now;

await Expect.That(subject2).Should().BeAfter(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(2)));
await Expect.That(subject2).Should().BeOnOrAfter(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(2)));
```

You can also specify a tolerance:
```csharp
DateTime subject = DateTime.Now;

await Expect.That(subject).Should().BeAfter(DateTime.Now).Within(TimeSpan.FromSeconds(1))
  .Because("it should have taken less than one second");
```

## Before

You can verify, that the `DateTime` or `DateTimeOffset` is (on or) before another value:
```csharp
DateTime subject1 = DateTime.Now;

await Expect.That(subject1).Should().BeBefore(new DateTime(2124, 12, 31));
await Expect.That(subject1).Should().BeOnOrBefore(new DateTime(2124, 12, 31));

DateTimeOffset subject2 = DateTimeOffset.Now;

await Expect.That(subject2).Should().BeBefore(new DateTimeOffset(2124, 12, 31, 23, 59, 59, TimeSpan.FromHours(2)));
await Expect.That(subject2).Should().BeOnOrBefore(new DateTimeOffset(2124, 12, 31, 23, 59, 59, TimeSpan.FromHours(2)));
```

You can also specify a tolerance:
```csharp
DateTime subject = DateTime.Now;

await Expect.That(subject).Should().BeOnOrBefore(DateTime.Now).Within(TimeSpan.FromSeconds(1))
  .Because("it should have taken less than one second");
```

## Properties

You can verify, the properties of `DateTime` or `DateTimeOffset`:
```csharp
DateTime subject = new DateTime(2024, 12, 31, 15, 16, 17, 189, DateTimeKind.Utc);
// or
DateTimeOffset subject = new DateTimeOffset(2024, 12, 31, 15, 16, 17, 189, TimeSpan.FromMinutes(90));

await Expect.That(subject).Should().HaveYear(2024);
await Expect.That(subject).Should().HaveMonth(12);
await Expect.That(subject).Should().HaveDay(31);
await Expect.That(subject).Should().HaveHour(15);
await Expect.That(subject).Should().HaveMinute(16);
await Expect.That(subject).Should().HaveSecond(17);
await Expect.That(subject).Should().HaveMillisecond(189);
```

For `DateTime` you can also verify the `Kind` property:
```csharp
await Expect.That(subject).Should().HaveKind(DateTimeKind.Utc);
```

For `DateTimeOffset` you can also verify the `Offset` property:
```csharp
await Expect.That(subject).Should().HaveOffset(TimeSpan.FromMinutes(90));
```
