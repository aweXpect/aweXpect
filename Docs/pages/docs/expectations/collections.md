---
sidebar_position: 13
---

# Collections

Describes the possible expectations for collections.

## All

You can verify, that all items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().All(x => x.Satisfy(i => i <= 20));
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## At least

You can verify, that at least a fixed number of items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().AtLeast(9, x => x.Satisfy(i => i < 10));
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## At most

You can verify, that at most a fixed number of items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().AtMost(1, x => x.Satisfy(i => i < 2));
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## Between

You can verify, that between `minimum` and `maximum` items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);
await Expect.That(values).Should().Between(1).And(2, x => x.Satisfy(i => i < 2));
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## None

You can verify, that not item in the collection, satisfies an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().None(x => x.Satisfy(i => i > 20));
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

