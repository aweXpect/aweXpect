---
sidebar_position: 13
---

# Collections

Describes the possible expectations for collections.

## Be

You can verify, that a collection matches another collection:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 3);

await Expect.That(values).Should().Be([1, 2, 3]);
await Expect.That(values).Should().Be([3, 2, 1]).InAnyOrder();
await Expect.That(values).Should().Be([1, 1, 2, 2, 3, 3]).IgnoringDuplicates();
await Expect.That(values).Should().Be([3, 3, 2, 2, 1, 1]).InAnyOrder().IgnoringDuplicates();
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


### Subset

You can verify, that a collection matches another collection or has fewer items (it is a subset of the expected items):
```csharp
IEnumerable<int> values = Enumerable.Range(1, 3);

await Expect.That(values).Should().Be([1, 2, 3, 4]).OrLess();
await Expect.That(values).Should().Be([4, 3, 2, 1]).OrLess().InAnyOrder();
await Expect.That(values).Should().Be([1, 1, 2, 2, 3, 3, 4, 4]).OrLess().IgnoringDuplicates();
await Expect.That(values).Should().Be([4, 4, 3, 3, 2, 2, 1, 1]).OrLess().InAnyOrder().IgnoringDuplicates();
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

To check for a proper subset, use `AndLess` instead (which would fail for equal collections).


### Superset

You can verify, that a collection matches another collection or has more items (it is a superset of the expected items):
```csharp
IEnumerable<int> values = Enumerable.Range(1, 3);

await Expect.That(values).Should().Be([1, 2]).OrMore();
await Expect.That(values).Should().Be([3, 2]).OrMore().InAnyOrder();
await Expect.That(values).Should().Be([1, 1, 2, 2]).OrMore().IgnoringDuplicates();
await Expect.That(values).Should().Be([3, 3, 1, 1]).OrMore().InAnyOrder().IgnoringDuplicates();
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

To check for a proper superset, use `AndMore` instead (which would fail for equal collections).


## Contain

You can verify, that the collection contains a specific item or not:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().Contain(13);
await Expect.That(values).Should().NotContain(42);
```

You can also set occurrence constraints on `Contain`:
```csharp
IEnumerable<int> values = [1, 1, 1, 2];

await Expect.That(values).Should().Contain(1).AtLeast(2.Times());
await Expect.That(values).Should().Contain(1).Exactly(3.Times());
await Expect.That(values).Should().Contain(1).AtMost(4.Times());
await Expect.That(values).Should().Contain(1).Between(1).And(5.Times());
```

You can also use a [custom comparer](/docs/expectations/object#custom-comparer) or configure [equivalence](/docs/expectations/object#equivalence):
```csharp
IEnumerable<MyClass> values = //...
MyClass expected = //...

await Expect.That(values).Should().Contain(expected).Equivalent();
await Expect.That(values).Should().Contain(expected).Using(new MyClassComparer());
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


### Predicate
You can verify, that the collection contains an item that satisfies a condition:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().Contain(x => x > 12 && x < 14);
await Expect.That(values).Should().NotContain(x => x >= 42);
```

You can also set occurrence constraints on `Contain`:
```csharp
IEnumerable<int> values = [1, 1, 1, 2];

await Expect.That(values).Should().Contain(x => x == 1).AtLeast(2.Times());
await Expect.That(values).Should().Contain(x => x == 1).Exactly(3.Times());
await Expect.That(values).Should().Contain(x => x == 1).AtMost(4.Times());
await Expect.That(values).Should().Contain(x => x == 1).Between(1).And(5.Times());
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## Have
Specifications that count the elements in a collection.

### All

You can verify, that all items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().HaveAll(x => x.Satisfy(i => i <= 20));
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


### At least

You can verify, that at least `minimum` items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().HaveAtLeast(9, x => x.Satisfy(i => i < 10));
```

You can also verify, that the collection has at least `minimum` items:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 10);

await Expect.That(values).Should().HaveAtLeast(9).Items();
```

*Note: The same expectations works also for `IAsyncEnumerable<T>`.*

### At most

You can verify, that at most `maximum` items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().HaveAtMost(1, x => x.Satisfy(i => i < 2));
```

You can also verify, that the collection has at most `maximum` items:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 10);

await Expect.That(values).Should().HaveAtMost(11).Items();
```

*Note: The same expectations works also for `IAsyncEnumerable<T>`.*


### Between

You can verify, that between `minimum` and `maximum` items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);
await Expect.That(values).Should().HaveBetween(1).And(2, x => x.Satisfy(i => i < 2));
```

You can also verify, that the collection has between `minimum` and `maximum` items:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 10);

await Expect.That(values).Should().HaveBetween(9).And(11).Items();
```

*Note: The same expectations works also for `IAsyncEnumerable<T>`.*


### Exactly

You can verify, that exactly `expected` items in the collection, satisfy an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().HaveExactly(9, x => x.Satisfy(i => i < 10));
```

You can also verify, that the collection has exactly `expected` items:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 10);

await Expect.That(values).Should().HaveExactly(10).Items();
```

*Note: The same expectations works also for `IAsyncEnumerable<T>`.*


### None

You can verify, that not item in the collection, satisfies an expectation:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Should().HaveNone(x => x.Satisfy(i => i > 20));
```

You can also verify, that the collection is empty.
```csharp
IEnumerable<int> values = Array.Empty<int>();

await Expect.That(values).Should().BeEmpty();
```

*Note: The same expectations works also for `IAsyncEnumerable<T>`.*


## All be unique

You can verify, that all items in a collection are unique.
```csharp
IEnumerable<int> values = [1, 2, 3];

await Expect.That(values).Should().AllBeUnique();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## Have single

You can verify, that the collection contains a single element that satisfies an expectation.
```csharp
IEnumerable<int> values = [42];

await Expect.That(values).Should().HaveSingle();
await Expect.That(values).Should().HaveSingle().Which.Should().BeGreaterThan(41);
```

The awaited result is the single element:
```csharp
IEnumerable<int> values = [42];

int result = await Expect.That(values).Should().HaveSingle();
await Expect.That(result).Should().BeGreaterThan(41);
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*
