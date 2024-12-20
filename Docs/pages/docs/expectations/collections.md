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


## All be

You can verify, that all items in the collection are equal to the `expected` value
```csharp
await Expect.That([1, 1, 1]).Should().AllBe(1);
```

You can also use a [custom comparer](/docs/expectations/object#custom-comparer) or configure [equivalence](/docs/expectations/object#equivalence):
```csharp
IEnumerable<MyClass> values = //...
MyClass expected = //...

await Expect.That(values).Should().AllBe(expected).Equivalent();
await Expect.That(values).Should().AllBe(expected).Using(new MyClassComparer());
```

For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing white-space, or use a custom `IEqualityComparer<string>`:
```csharp
await Expect.That(["foo", "FOO", "Foo"]).Should().AllBe("foo").IgnoringCase();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## All be unique

You can verify, that all items in a collection are unique.
```csharp
await Expect.That([1, 2, 3]).Should().AllBeUnique();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

For dictionaries, this expectation only verifies the values, as the keys are unique by design:
```csharp
IDictionary<int, int> subject = new Dictionary<int, int>
{
  { 1, 1 },
  { 2, 1 }
};

// This following expectation will fail, even though the keys are unique!
await Expect.That(subject).Should().AllBeUnique();
```


## All satisfy

You can verify, that all items in a collection satisfy a condition:
```csharp
await Expect.That([1, 2, 3]).Should().AllSatisfy(x => x < 4);
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## Sort order

You can verify, that the collection contains is sorted in ascending or descending order:
```csharp
await Expect.That([1, 2, 3]).Should().BeInAscendingOrder();
await Expect.That(["c", "b", "a"]).Should().BeInDescendingOrder();
```

You can also specify a custom comparer:
```csharp
await Expect.That(["a", "B", "c"]).Should().BeInAscendingOrder().Using(StringComparer.OrdinalIgnoreCase);
```

For objects, you can also verify the sort order on a member:
```csharp
MyClass[] values = //...

await Expect.That(values).Should().BeInAscendingOrder(x => x.Value);
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


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


### Contain subset

You can verify, that a collection contains another collection as a subset:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 3);

await Expect.That(values).Should().Contain([1, 2]);
await Expect.That(values).Should().Contain([3, 2]).InAnyOrder();
await Expect.That(values).Should().Contain([1, 1, 2, 2]).IgnoringDuplicates();
await Expect.That(values).Should().Contain([3, 3, 1, 1]).InAnyOrder().IgnoringDuplicates();
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

To check for a proper subset, append `.Properly()` (which would fail for equal collections).


### Be contained in

You can verify, that a collection is contained in another collection (it is a superset):
```csharp
IEnumerable<int> values = Enumerable.Range(1, 3);

await Expect.That(values).Should().BeContainedIn([1, 2, 3, 4]);
await Expect.That(values).Should().BeContainedIn([4, 3, 2, 1]).InAnyOrder();
await Expect.That(values).Should().BeContainedIn([1, 1, 2, 2, 3, 3, 4, 4]).IgnoringDuplicates();
await Expect.That(values).Should().BeContainedIn([4, 4, 3, 3, 2, 2, 1, 1]).InAnyOrder().IgnoringDuplicates();
```
*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

To check for a proper superset, append `.Properly()` (which would fail for equal collections).


## Start with

You can verify, if a collection starts with another collection or not:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 3);

await Expect.That(values).Should().StartWith(1, 2);
await Expect.That(values).Should().NotStartWith(2, 3);
```

You can also use a [custom comparer](/docs/expectations/object#custom-comparer) or configure [equivalence](/docs/expectations/object#equivalence):
```csharp
IEnumerable<MyClass> values = //...
MyClass expected = //...

await Expect.That(values).Should().StartWith(expected).Equivalent();
await Expect.That(values).Should().StartWith(expected).Using(new MyClassComparer());
```

For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing white-space, or use a custom `IEqualityComparer<string>`:
```csharp
await Expect.That(["FOO", "BAR"]).Should().StartWith(["foo"]).IgnoringCase();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## End with

You can verify, if a collection ends with another collection or not:
```csharp
IEnumerable<int> values = Enumerable.Range(1, 5);

await Expect.That(values).Should().EndWith(4, 5);
await Expect.That(values).Should().NotEndWith(3, 5);
```

You can also use a [custom comparer](/docs/expectations/object#custom-comparer) or configure [equivalence](/docs/expectations/object#equivalence):
```csharp
IEnumerable<MyClass> values = //...
MyClass expected = //...

await Expect.That(values).Should().EndWith(expected).Equivalent();
await Expect.That(values).Should().EndWith(expected).Using(new MyClassComparer());
```

For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing white-space, or use a custom `IEqualityComparer<string>`:
```csharp
await Expect.That(["FOO", "BAR"]).Should().EndWith(["bar"]).IgnoringCase();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

*Caution: this method will always have to completely materialize the enumerable!*


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


## Dictionaries

### Contain key(s)

You can verify, that a dictionary contains the `expected` key(s):
```csharp
Dictionary<int, string> values = new() { { 42, "foo" }, { 43, "bar" } };

await Expect.That(values).Should().ContainKey(42);
await Expect.That(values).Should().ContainKeys(42, 43);
await Expect.That(values).Should().NotContainKey(44);
await Expect.That(values).Should().NotContainKeys(44, 45, 46);
```

### Contain value(s)

You can verify, that a dictionary contains the `expected` value(s):
```csharp
Dictionary<int, string> values = new() { { 42, "foo" }, { 43, "bar" } };

await Expect.That(values).Should().ContainValue("foo");
await Expect.That(values).Should().ContainValues("foo", "bar");
await Expect.That(values).Should().NotContainValue("something");
await Expect.That(values).Should().NotContainValues("something", "else");
```
