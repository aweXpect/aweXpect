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

await Expect.That(values).Should().AtLeast(9.Times(), x => x.Satisfy(i => i < 10));
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## At most

You can verify, that at most a fixed number of items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().AtMost(1.Times(), x => x.Satisfy(i => i < 2));
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## Between

You can verify, that between `minimum` and `maximum` items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);
await Expect.That(values).Should().Between(1).And(2.Times(), x => x.Satisfy(i => i < 2));
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## None

You can verify, that not item in the collection, satisfies an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().None(x => x.Satisfy(i => i > 20));
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## Contain

You can verify, that the collection contains a specific item or not:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().Contain(13);
await Expect.That(values).Should().NotContain(42);
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

You can also verify, that the collection contains an item that satisfies a condition:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().Contain(x => x > 12 && x < 14);
await Expect.That(values).Should().NotContain(x => x >= 42);
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*
