# Object

Describes the possible expectations for objects.

## Equality

You can verify that the `object` is equal to another one or not:

```csharp
record MyClass(int Value);
MyClass subject = new(1);

await Expect.That(subject).IsEqualTo(new MyClass(1));
await Expect.That(subject).IsNotEqualTo(new MyClass(2));
```

*Note: this uses the underlying `object.Equals(object?, object?)` method*

### Reference equality

You can verify that the `object` has the same reference as another one:

```csharp
record MyClass(int Value);
MyClass subject = new(1);

await Expect.That(subject).IsSameAs(subject);
await Expect.That(subject).IsNotSameAs(new MyClass(1));
```

*Note: this uses the underlying `object.ReferenceEquals(object?, object?)` method*

### Custom comparer

You can verify that the `object` is equal to another one while using a custom `IEqualityComparer<object>`:

```csharp
class MyClassComparer : IEqualityComparer<object>
{
  public bool Equals(object? x, object? y)
    => x != null && y != null;
  public int GetHashCode(object obj)
    => obj.GetHashCode();
}
MyClass subject = new(1);

await Expect.That(subject).IsEqualTo(new MyClass(2)).Using(new MyClassComparer());
```

## Equivalence

You can verify that the `object` is equivalent to another one or not:

```csharp
class MyClass(int value)
{
  int Value { get; } = value;
}
MyClass subject = new(1);

await Expect.That(subject).IsEqualTo(new MyClass(1)).Equivalent();
await Expect.That(subject).IsNotEqualTo(new MyClass(2)).Equivalent();
```

*Note: this compares recursively all properties on the two objects for equivalence.*

## Type check

You can verify that the `object` is of a given type or not:

```csharp
object subject = new MyClass(1);

await Expect.That(subject).Is<MyClass>();
await Expect.That(subject).Is(typeof(MyClass));
await Expect.That(subject).IsNot<OtherClass>();
await Expect.That(subject).IsNot(typeof(OtherClass));
```

This verifies, if the subject is of the given type or a derived type.

You can also verify that the `object` is only of the given type and not of a derived type:

```csharp
object subject = new MyClass(1);

await Expect.That(subject).IsExactly<MyClass>();
await Expect.That(subject).IsExactly(typeof(MyClass));
await Expect.That(subject).IsNotExactly<OtherClass>();
await Expect.That(subject).IsNotExactly(typeof(OtherClass));
```

## Null

You can verify, if the `object` is `null` or not:

```csharp
object? subject = null;

await Expect.That(subject).IsNull();
await Expect.That(new object()).IsNotNull();
```

## Satisfy

You can verify that any object satisfies a given predicate:

```csharp
object? subject = null;

await Expect.That(subject).Satisfies(x => x == null);
```

When the object changes in the background, you can also verify that it satisfies a condition within a given time
period:

```csharp
MyClass subject = new() {
	IsTriggered = false
};
// Start a background task that sets `IsTriggered` to true

await Expect.That(subject).Satisfies(x => x.IsTriggered == true).Within(2.Seconds());
// using aweXpect.Chronology
```

## Comply with

You can verify that any object complies with an expectation:

```csharp
List<int> values = new();

await Expect.That(values).CompliesWith(x => x.IsEmpty());
```

When the object changes in the background, you can also verify that it complies with an expectation within a given time
period:

```csharp
List<int> values = new();
// Start a background task that adds items to `values`

await Expect.That(values).CompliesWith(x => x.HasCount().AtLeast(4)).Within(2.Seconds());
// using aweXpect.Chronology
```
