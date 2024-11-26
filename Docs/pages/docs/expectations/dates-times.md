---
sidebar_position: 4
---

# Dates and times

Describes the possible expectations for `TimeSpan`, `DateTime`, `DateTimeOffset`, `DateOnly` and `TimeOnly`.

## `TimeSpan`

### Equality

For asserting whether a `TimeSpan` is equal to another one, use:

```csharp
TimeSpan subject = TimeSpan.FromSeconds(42);
await Expect.That(subject).Should().Be(TimeSpan.FromSeconds(42));
```

You can also specify a tolerance:

```csharp
TimeSpan subject = TimeSpan.FromSeconds(42);
await Expect.That(subject).Should().Be(TimeSpan.FromSeconds(43))
  .Within(TimeSpan.FromSeconds(1));
```

### Greater than / less than

You can also verify that a `TimeSpan` is greater than or less than another value

```csharp
TimeSpan subject = TimeSpan.FromSeconds(42);
await Expect.That(subject).Should().BeGreaterThan(TimeSpan.FromSeconds(41));
await Expect.That(subject).Should().BeGreaterThanOrEqualTo(TimeSpan.FromSeconds(42));
await Expect.That(subject).Should().BeLessThanOrEqualTo(TimeSpan.FromSeconds(42));
await Expect.That(subject).Should().BeLessThan(TimeSpan.FromSeconds(43));
```

Also for these methods you can specify a tolerance, e.g.

```csharp
TimeSpan subject = TimeSpan.FromSeconds(42);
await Expect.That(subject).Should().BeLessThanOrEqualTo(TimeSpan.FromSeconds(41))
  .Within(TimeSpan.FromSeconds(1));
```

### Positive / negative

As a special case, to verify, if a number is positive or negative, you can use the following expectations.

```csharp
TimeSpan subject = TimeSpan.FromSeconds(42);
await Expect.That(subject).Should().BePositive();
await Expect.That(subject).Should().NotBeNegative();

subject = TimeSpan.FromSeconds(-42);
await Expect.That(subject).Should().BeNegative();
await Expect.That(subject).Should().NotBePositive();
```


## `DateTime`

### Equality

For asserting whether a `DateTime` is equal to another one, use:

```csharp
DateTime subject = new DateTime(2024, 12, 24);
await Expect.That(subject).Should().Be(new DateTime(2024, 12, 24));
```

You can also specify a tolerance:

```csharp
DateTime subject = new DateTime(2024, 12, 24);
await Expect.That(subject).Should().Be(new DateTime(2024, 12, 23))
  .Within(TimeSpan.FromDays(1));
```

### After / before

You can also verify that a `DateTime` is after or before another value

```csharp
DateTime subject = DateTime.Now;
await Expect.That(subject).Should().BeAfter(new DateTime(2024, 1, 1));
await Expect.That(subject).Should().BeOnOrAfter(new DateTime(2024, 1, 1));
await Expect.That(subject).Should().BeBefore(new DateTime(2124, 12, 31));
await Expect.That(subject).Should().BeOnOrBefore(new DateTime(2124, 12, 31));
```

Also for these methods you can specify a tolerance, e.g.

```csharp
DateTime subject = DateTime.Now;
await Expect.That(subject).Should().BeOnOrBefore(DateTime.Now)
  .Within(TimeSpan.FromSeconds(1));
```

### Property validation

You can also verify the value of each individual property:

```csharp
DateTime subject = new DateTime(2024, 12, 31, 15, 16, 17, 189, DateTimeKind.Utc);
await Expect.That(subject).Should().HaveYear(2024);
await Expect.That(subject).Should().HaveMonth(12);
await Expect.That(subject).Should().HaveDay(31);
await Expect.That(subject).Should().HaveHour(15);
await Expect.That(subject).Should().HaveMinute(16);
await Expect.That(subject).Should().HaveSecond(17);
await Expect.That(subject).Should().HaveMillisecond(189);
await Expect.That(subject).Should().HaveKind(DateTimeKind.Utc);
```


## `DateTimeOffset`

### Equality

For asserting whether a `DateTimeOffset` is equal to another one, use:

```csharp
DateTimeOffset subject = new DateTimeOffset(2024, 12, 24, 13, 15, 0, TimeSpan.FromHours(2));
await Expect.That(subject).Should().Be(new DateTimeOffset(2024, 12, 24, 13, 15, 0, TimeSpan.FromHours(2)));
```

You can also specify a tolerance:

```csharp
DateTimeOffset subject = new DateTimeOffset(2024, 12, 24, 13, 15, 0, TimeSpan.FromHours(2));
await Expect.That(subject).Should().Be(new DateTimeOffset(2024, 12, 24, 13, 5, 0, TimeSpan.FromHours(2)))
  .Within(TimeSpan.FromMinutes(10));
```

### After / before

You can also verify that a `DateTimeOffset` is after or before another value

```csharp
DateTimeOffset subject = DateTimeOffset.Now;
await Expect.That(subject).Should().BeAfter(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(2)));
await Expect.That(subject).Should().BeOnOrAfter(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(2)));
await Expect.That(subject).Should().BeBefore(new DateTimeOffset(2124, 12, 31, 23, 59, 59, TimeSpan.FromHours(2)));
await Expect.That(subject).Should().BeOnOrBefore(new DateTimeOffset(2124, 12, 31, 23, 59, 59, TimeSpan.FromHours(2)));
```

Also for these methods you can specify a tolerance, e.g.

```csharp
DateTimeOffset subject = DateTimeOffset.Now;
await Expect.That(subject).Should().BeOnOrBefore(DateTimeOffset.Now)
  .Within(TimeSpan.FromSeconds(1));
```

### Property validation

You can also verify the value of each individual property:

```csharp
DateTimeOffset subject = new DateTimeOffset(2024, 12, 31, 15, 16, 17, 189, TimeSpan.FromMinutes(90));
await Expect.That(subject).Should().HaveYear(2024);
await Expect.That(subject).Should().HaveMonth(12);
await Expect.That(subject).Should().HaveDay(31);
await Expect.That(subject).Should().HaveHour(15);
await Expect.That(subject).Should().HaveMinute(16);
await Expect.That(subject).Should().HaveSecond(17);
await Expect.That(subject).Should().HaveMillisecond(189);
await Expect.That(subject).Should().HaveOffset(TimeSpan.FromMinutes(90));
```


## `DateOnly`

### Equality

For asserting whether a `DateOnly` is equal to another one, use:

```csharp
DateOnly subject = new DateOnly(2024, 12, 24);
await Expect.That(subject).Should().Be(new DateOnly(2024, 12, 24));
```

You can also specify a tolerance:

```csharp
DateOnly subject = new DateOnly(2024, 12, 24);
await Expect.That(subject).Should().Be(new DateOnly(2024, 12, 23))
  .Within(TimeSpan.FromDays(1));
```

### After / before

You can also verify that a `DateOnly` is after or before another value

```csharp
DateOnly subject = DateOnly.FromDateTime(DateTime.Now);
await Expect.That(subject).Should().BeAfter(new DateOnly(2024, 1, 1));
await Expect.That(subject).Should().BeOnOrAfter(new DateOnly(2024, 1, 1));
await Expect.That(subject).Should().BeBefore(new DateOnly(2124, 12, 31));
await Expect.That(subject).Should().BeOnOrBefore(new DateOnly(2124, 12, 31));
```

Also for these methods you can specify a tolerance, e.g.

```csharp
DateOnly subject = DateOnly.FromDateTime(DateTime.Now);
await Expect.That(subject).Should().BeBefore(DateOnly.FromDateTime(DateTime.Now))
  .Within(TimeSpan.FromDays(1));
```

### Property validation

You can also verify the value of each individual property:

```csharp
DateOnly subject = new DateOnly(2024, 12, 31);
await Expect.That(subject).Should().HaveYear(2024);
await Expect.That(subject).Should().HaveMonth(12);
await Expect.That(subject).Should().HaveDay(31);
```


## `TimeOnly`

### Equality

For asserting whether a `TimeOnly` is equal to another one, use:

```csharp
TimeOnly subject = new TimeOnly(14, 15, 16);
await Expect.That(subject).Should().Be(new TimeOnly(14, 15, 16));
```

### After / before

You can also verify that a `TimeOnly` is after or before another value

```csharp
TimeOnly subject = TimeOnly.FromDateTime(DateTime.Now);
await Expect.That(subject).Should().BeAfter(new TimeOnly(0, 0, 0));
await Expect.That(subject).Should().BeOnOrAfter(new TimeOnly(0, 0, 0));
await Expect.That(subject).Should().BeBefore(new TimeOnly(23, 59, 59));
await Expect.That(subject).Should().BeOnOrBefore(new TimeOnly(23, 59, 59));
```

Also for these methods you can specify a tolerance, e.g.

```csharp
TimeOnly subject = TimeOnly.FromDateTime(DateTime.Now);
await Expect.That(subject).Should().BeBefore(TimeOnly.FromDateTime(DateTime.Now))
  .Within(TimeSpan.FromSeconds(1));
```

### Property validation

You can also verify the value of each individual property:

```csharp
TimeOnly subject = new TimeOnly(15, 16, 17, 189);
await Expect.That(subject).Should().HaveHour(15);
await Expect.That(subject).Should().HaveMinute(16);
await Expect.That(subject).Should().HaveSecond(17);
await Expect.That(subject).Should().HaveMillisecond(189);
```
