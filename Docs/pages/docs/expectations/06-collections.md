# Collections

Describes the possible expectations for collections.

## Equality

You can verify, that a collection matches another collection:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 3);

await Expect.That(values).IsEqualTo([1, 2, 3]);
await Expect.That(values).IsEqualTo([3, 2, 1]).InAnyOrder();
await Expect.That(values).IsEqualTo([1, 1, 2, 2, 3, 3]).IgnoringDuplicates();
await Expect.That(values).IsEqualTo([3, 3, 2, 2, 1, 1]).InAnyOrder().IgnoringDuplicates();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

## Has count

You can verify the number of items in a collection:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 10);

await Expect.That(values).HasCount(10);
// or more explicit
await Expect.That(values).HasCount().EqualTo(10);

await Expect.That(values).HasCount().AtLeast(9);
await Expect.That(values).HasCount().AtMost(11);
await Expect.That(values).HasCount().Between(8).And(12);
```

## All be

You can verify, that all items in the collection are equal to the `expected` value

```csharp
await Expect.That([1, 1, 1]).All().AreEqualTo(1);
```

You can also use a [custom comparer](/docs/expectations/object#custom-comparer) or
configure [equivalence](/docs/expectations/object#equivalence):

```csharp
IEnumerable<MyClass> values = //...
MyClass expected = //...

await Expect.That(values).All().AreEqualTo(expected).Equivalent();
await Expect.That(values).All().AreEqualTo(expected).Using(new MyClassComparer());
```

For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing
white-space, or use a custom `IEqualityComparer<string>`:

```csharp
await Expect.That(["foo", "FOO", "Foo"]).All().AreEqualTo("foo").IgnoringCase();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

## All be unique

You can verify, that all items in a collection are unique.

```csharp
await Expect.That([1, 2, 3]).AreAllUnique();
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
await Expect.That(subject).AreAllUnique();
```

## Elements

You can add expectations that a certain number of elements must meet.

### Comply with

You can verify, that items in a collection comply with an expectation on the individual elements:

```csharp
await Expect.That([1, 2, 3]).All().ComplyWith(item => item.IsLessThan(4));
await Expect.That([1, 2, 3]).AtLeast(2).ComplyWith(item => item.IsGreaterThanOrEqualTo(2));
await Expect.That([1, 2, 3]).AtMost(1).ComplyWith(item => item.IsNegative());
await Expect.That([1, 2, 3]).Between(2).And(3).ComplyWith(item => item.IsPositive());
await Expect.That([1, 2, 3]).Exactly(1).ComplyWith(item => item.IsEqualTo(2));
await Expect.That([1, 2, 3]).None().ComplyWith(item => item.IsNegative());
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

### Satisfy

You can verify, that items in a collection satisfy a condition:

```csharp
await Expect.That([1, 2, 3]).All().Satisfy(item => item < 4);
await Expect.That([1, 2, 3]).AtLeast(2).Satisfy(item => item >= 2);
await Expect.That([1, 2, 3]).AtMost(1).Satisfy(item => item < 0);
await Expect.That([1, 2, 3]).Between(2).And(3).Satisfy(item => item > 0);
await Expect.That([1, 2, 3]).Exactly(1).Satisfy(item => item == 2);
await Expect.That([1, 2, 3]).None().Satisfy(item => item < 0);
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*


## Sort order

You can verify, that the collection contains is sorted in ascending or descending order:

```csharp
await Expect.That([1, 2, 3]).IsInAscendingOrder();
await Expect.That(["c", "b", "a"]).IsInDescendingOrder();
```

You can also specify a custom comparer:

```csharp
await Expect.That(["a", "B", "c"]).IsInAscendingOrder().Using(StringComparer.OrdinalIgnoreCase);
```

For objects, you can also verify the sort order on a member:

```csharp
MyClass[] values = //...

await Expect.That(values).IsInAscendingOrder(x => x.Value);
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

## Contain

You can verify, that the collection contains a specific item or not:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Contains(13);
await Expect.That(values).DoesNotContain(42);
```

You can also set occurrence constraints on `Contain`:

```csharp
IEnumerable<int> values = [1, 1, 1, 2];

await Expect.That(values).Contains(1).AtLeast(2.Times());
await Expect.That(values).Contains(1).Exactly(3.Times());
await Expect.That(values).Contains(1).AtMost(4.Times());
await Expect.That(values).Contains(1).Between(1).And(5.Times());
```

You can also use a [custom comparer](/docs/expectations/object#custom-comparer) or
configure [equivalence](/docs/expectations/object#equivalence):

```csharp
IEnumerable<MyClass> values = //...
MyClass expected = //...

await Expect.That(values).Contains(expected).Equivalent();
await Expect.That(values).Contains(expected).Using(new MyClassComparer());
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

### Predicate

You can verify, that the collection contains an item that satisfies a condition:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Contains(x => x > 12 && x < 14);
await Expect.That(values).DoesNotContain(x => x >= 42);
```

You can also set occurrence constraints on `Contain`:

```csharp
IEnumerable<int> values = [1, 1, 1, 2];

await Expect.That(values).Contains(x => x == 1).AtLeast(2.Times());
await Expect.That(values).Contains(x => x == 1).Exactly(3.Times());
await Expect.That(values).Contains(x => x == 1).AtMost(4.Times());
await Expect.That(values).Contains(x => x == 1).Between(1).And(5.Times());
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

### Contain subset

You can verify, that a collection contains another collection as a subset:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 3);

await Expect.That(values).Contains([1, 2]);
await Expect.That(values).Contains([3, 2]).InAnyOrder();
await Expect.That(values).Contains([1, 1, 2, 2]).IgnoringDuplicates();
await Expect.That(values).Contains([3, 3, 1, 1]).InAnyOrder().IgnoringDuplicates();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

To check for a proper subset, append `.Properly()` (which would fail for equal collections).

### Be contained in

You can verify, that a collection is contained in another collection (it is a superset):

```csharp
IEnumerable<int> values = Enumerable.Range(1, 3);

await Expect.That(values).IsContainedIn([1, 2, 3, 4]);
await Expect.That(values).IsContainedIn([4, 3, 2, 1]).InAnyOrder();
await Expect.That(values).IsContainedIn([1, 1, 2, 2, 3, 3, 4, 4]).IgnoringDuplicates();
await Expect.That(values).IsContainedIn([4, 4, 3, 3, 2, 2, 1, 1]).InAnyOrder().IgnoringDuplicates();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

To check for a proper superset, append `.Properly()` (which would fail for equal collections).

## Start with

You can verify, if a collection starts with another collection or not:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 3);

await Expect.That(values).StartsWith(1, 2);
await Expect.That(values).DoesNotStartWith(2, 3);
```

You can also use a [custom comparer](/docs/expectations/object#custom-comparer) or
configure [equivalence](/docs/expectations/object#equivalence):

```csharp
IEnumerable<MyClass> values = //...
MyClass expected = //...

await Expect.That(values).StartsWith(expected).Equivalent();
await Expect.That(values).StartsWith(expected).Using(new MyClassComparer());
```

For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing
white-space, or use a custom `IEqualityComparer<string>`:

```csharp
await Expect.That(["FOO", "BAR"]).StartsWith(["foo"]).IgnoringCase();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

## End with

You can verify, if a collection ends with another collection or not:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 5);

await Expect.That(values).EndsWith(4, 5);
await Expect.That(values).DoesNotEndWith(3, 5);
```

You can also use a [custom comparer](/docs/expectations/object#custom-comparer) or
configure [equivalence](/docs/expectations/object#equivalence):

```csharp
IEnumerable<MyClass> values = //...
MyClass expected = //...

await Expect.That(values).EndsWith(expected).Equivalent();
await Expect.That(values).EndsWith(expected).Using(new MyClassComparer());
```

For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing
white-space, or use a custom `IEqualityComparer<string>`:

```csharp
await Expect.That(["FOO", "BAR"]).EndsWith(["bar"]).IgnoringCase();
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

*Caution: this method will always have to completely materialize the enumerable!*


## Have

Specifications that count the elements in a collection.

### All

You can verify, that all items in the collection, satisfy an expectation:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).All().Satisfy(i => i <= 20);
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

### At least

You can verify, that at least `minimum` items in the collection, satisfy an expectation:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).AtLeast(9).Satisfy(i => i < 10);
```

*Note: The same expectations works also for `IAsyncEnumerable<T>`.*

### At most

You can verify, that at most `maximum` items in the collection, satisfy an expectation:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).AtMost(1).Satisfy(i => i < 2);
```

*Note: The same expectations works also for `IAsyncEnumerable<T>`.*

### Between

You can verify, that between `minimum` and `maximum` items in the collection, satisfy an expectation:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Between(1).And(2).Satisfy(i => i < 2);
```

*Note: The same expectations works also for `IAsyncEnumerable<T>`.*

### Exactly

You can verify, that exactly `expected` items in the collection, satisfy an expectation:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).Exactly(9).Satisfy(i => i < 10);
```

*Note: The same expectations works also for `IAsyncEnumerable<T>`.*

### None

You can verify, that not item in the collection, satisfies an expectation:

```csharp
IEnumerable<int> values = Enumerable.Range(1, 20);

await Expect.That(values).None().Satisfy(i => i > 20);
```

You can also verify, that the collection is empty.

```csharp
IEnumerable<int> values = Array.Empty<int>();

await Expect.That(values).IsEmpty();
```

*Note: The same expectations works also for `IAsyncEnumerable<T>`.*

## Have single

You can verify, that the collection contains a single element that satisfies an expectation.

```csharp
IEnumerable<int> values = [42];

await Expect.That(values).HasSingle();
await Expect.That(values).HasSingle().Which.IsGreaterThan(41);
```

The awaited result is the single element:

```csharp
IEnumerable<int> values = [42];

int result = await Expect.That(values).HasSingle();
await Expect.That(result).IsGreaterThan(41);
```

*Note: The same expectation works also for `IAsyncEnumerable<T>`.*

## Dictionaries

### Contain key(s)

You can verify, that a dictionary contains the `expected` key(s):

```csharp
Dictionary<int, string> values = new() { { 42, "foo" }, { 43, "bar" } };

await Expect.That(values).ContainsKey(42);
await Expect.That(values).ContainsKeys(42, 43);
await Expect.That(values).DoesNotContainKey(44);
await Expect.That(values).DoesNotContainKeys(44, 45, 46);
```

You can add additional expectations on the corresponding value(s):

```csharp
Dictionary<int, string> values = new() { { 42, "foo" }, { 43, "bar" }, { 44, "baz" } };

await Expect.That(values).ContainsKey(42).WhoseValue.IsEqualTo("foo");
await Expect.That(values).ContainsKeys(43, 44).WhoseValues.ComplyWith(v => v.StartsWith("ba"));
```

### Contain value(s)

You can verify, that a dictionary contains the `expected` value(s):

```csharp
Dictionary<int, string> values = new() { { 42, "foo" }, { 43, "bar" } };

await Expect.That(values).ContainsValue("foo");
await Expect.That(values).ContainsValues("foo", "bar");
await Expect.That(values).DoesNotContainValue("something");
await Expect.That(values).DoesNotContainValues("something", "else");
```
