---
sidebar_position: 1
---

# Object

Describes the possible expectations for objects.

## Equality

You can verify, that the `object` is equal to another one:
```csharp
record MyClass(int Value);
MyClass subject = new(1);

await Expect.That(subject).Should().Be(new MyClass(1));
```
*Note: this uses the underlying `object.Equals(object?, object?)` method*

### Reference equality

You can verify, that the `object` has the same reference as another one:
```csharp
record MyClass(int Value);
MyClass subject = new(1);

await Expect.That(subject).Should().BeSameAs(subject);
await Expect.That(subject).Should().NotBeSameAs(new MyClass(1));
```
*Note: this uses the underlying `object.ReferenceEquals(object?, object?)` method*

### Custom comparer

You can verify, that the `object` is equal to another one while using a custom `IEqualityComparer<object>`:
```csharp
class MyClassComparer : IEqualityComparer<object>
{
  public bool Equals(object? x, object? y)
    => x != null && y != null;
  public int GetHashCode(object obj)
    => obj.GetHashCode();
}
MyClass subject = new(1);

await Expect.That(subject).Should().Be(new MyClass(2)).Using(new MyClassComparer());
```


## Equivalence

You can verify, that the `object` is equivalent to another one:
```csharp
class MyClass(int value)
{
  int Value { get; } = value;
}
MyClass subject = new(1);

await Expect.That(subject).Should().Be(new MyClass(1)).Equivalent();
```
*Note: this compares recursively all properties on the two objects for equivalence.*


## Type check

You can verify, that the `object` is of a given type:
```csharp
object subject = new MyClass(1);

await Expect.That(subject).Should().Be<MyClass>();
await Expect.That(subject).Should().Be(typeof(MyClass));
```
This verifies, if the subject is of the given type or a derived type.


## Null

You can verify, if the `object` is `null` or not:
```csharp
object? subject = null;

await Expect.That(subject).Should().BeNull();
await Expect.That(new object()).Should().NotBeNull();
```