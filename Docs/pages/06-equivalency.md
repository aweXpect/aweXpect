# Equivalency

Describes how to verify that two objects are *equivalent* — that is, structurally equal — rather than referentially or
strictly equal. Equivalency walks both objects recursively and compares them member by member.

## Overview

Equality (`IsEqualTo`) delegates to `object.Equals`, which for most reference types means *reference* equality.
Equivalency instead compares the public state of two objects field by field and property by property, recursing into
nested objects and collections. Two objects are equivalent when every included member compares as equivalent.

```csharp
class MyClass(int value)
{
  public int Value { get; } = value;
}
MyClass subject = new(1);

await Expect.That(subject).IsEquivalentTo(new MyClass(1));
await Expect.That(subject).IsNotEquivalentTo(new MyClass(2));
```

## Where equivalency is available

Equivalency is exposed on three different surfaces.

### Direct on objects

`IsEquivalentTo` and `IsNotEquivalentTo` are extension methods on any object. They accept an optional callback to
configure the comparison via [`EquivalencyOptions<TExpected>`](#configuration).

```csharp
await Expect.That(subject).IsEquivalentTo(expected);
await Expect.That(subject).IsEquivalentTo(expected, o => o.IgnoringMember("Id"));
await Expect.That(subject).IsNotEquivalentTo(unexpected);
```

### On collection elements

`AreEquivalentTo` checks every selected element of an `IEnumerable<T>` (or `IAsyncEnumerable<T>`) against a single
expected value, using the same equivalency comparison.

```csharp
IEnumerable<MyClass> values = //...
MyClass expected = //...

await Expect.That(values).All().AreEquivalentTo(expected);
await Expect.That(values).AtLeast(2).AreEquivalentTo(expected, o => o.IgnoringMember("Id"));
```

### As a modifier on equality assertions

For expectations that accept a custom equality comparer (`IsEqualTo`, `Contains`, `StartsWith`, `EndsWith`, `HasItem`,
`All().AreEqualTo(...)`, …), append `.Equivalent()` to switch the comparison from `Equals` to structural equivalency.

```csharp
await Expect.That(subject).IsEqualTo(expected).Equivalent();

IEnumerable<MyClass> values = //...
await Expect.That(values).Contains(expected).Equivalent();
await Expect.That(values).StartsWith(expected).Equivalent();
await Expect.That(values).All().AreEqualTo(expected).Equivalent(o => o.IgnoringCollectionOrder());
```

## Default behaviour

By default, equivalency:

- Compares **public fields** and **public properties**.
- Recurses into nested objects.
- Treats primitives, `enum`, `string`, `decimal`, `DateTime`, `DateTimeOffset`, `TimeSpan` and `Guid` as
  *value types* and compares them with `Equals`. Everything else is compared **by members**.
- Respects collection **order** when comparing `IEnumerable<T>`.
- Detects cyclic references so two graphs that reference themselves do not cause infinite recursion.
- Honours `IEqualityComparer` if either side implements it — that comparer wins over the structural walk.

## Configuration

All equivalency overloads accept an `options` callback that receives an `EquivalencyOptions` (or
`EquivalencyOptions<TExpected>`) record. The callback returns the configured options. All methods are immutable and
chainable.

```csharp
await Expect.That(subject).IsEquivalentTo(expected, o => o
  .IncludingFields(IncludeMembers.Public | IncludeMembers.Internal)
  .IgnoringMember("Id")
  .IgnoringCollectionOrder());
```

### Ignoring members by name

```csharp
await Expect.That(subject).IsEquivalentTo(expected, o => o.IgnoringMember("Id"));
```

The match is case-insensitive. For nested members, the path is dot-separated (e.g. `"Inner.IntValue"`); for collection
elements, the index is bracketed (e.g. `"Inner.Inner.Collection[3]"`).

### Ignoring members by predicate

There are four overloads of `Ignoring`, depending on which information you need:

```csharp
// by member path and type
await Expect.That(subject).IsEquivalentTo(expected, o => o
  .Ignoring((memberPath, memberType)
    => memberPath.EndsWith("IntValue") && memberType == typeof(int)));

// by member path only
await Expect.That(subject).IsEquivalentTo(expected, o => o
  .Ignoring(memberPath => memberPath == "Inner.IntValue"));

// by type only
await Expect.That(subject).IsEquivalentTo(expected, o => o
  .Ignoring(memberType => memberType == typeof(DateTime)));

// by member path, type and reflected MemberInfo
await Expect.That(subject).IsEquivalentTo(expected, o => o
  .Ignoring((memberPath, _, memberInfo)
    => memberPath.EndsWith("IntValue") && memberInfo is PropertyInfo));
```

### Including fields and properties

You can change which fields and properties participate in the comparison. Both methods accept an `IncludeMembers` flags
enum with the values `None`, `Public`, `Internal` and `Private`.

```csharp
await Expect.That(subject).IsEquivalentTo(expected, o => o
  .IncludingFields(IncludeMembers.None)                         // exclude all fields
  .IncludingProperties(IncludeMembers.Public | IncludeMembers.Private));
```

Default for both is `IncludeMembers.Public`.

### Ignoring collection order

When comparing collections, order matters by default. To disable that:

```csharp
int[] subject  = [1, 2, 3];
int[] expected = [3, 2, 1];

await Expect.That(subject).IsEquivalentTo(expected, o => o.IgnoringCollectionOrder());
```

Pass `false` to re-enable ordered comparison if it was disabled globally.

### Per-type options with `For<T>`

You can apply options to a specific member type only. Type-specific options override the top-level options for members
of that type.

```csharp
await Expect.That(subject).IsEquivalentTo(expected, o => o
  .For<InnerClass>(x => x.IgnoringMember("IntValue"))
  .For<List<string>>(x => x.IgnoringCollectionOrder()));
```

### Comparing by value or by members

Each type can be compared either by value (`Equals`) or by walking its members. The default is determined by the type
itself (see [Default behaviour](#default-behaviour)). To override for a specific type:

```csharp
await Expect.That(subject).IsEquivalentTo(expected, o => o
  .For<MyValueObject>(x => x with { ComparisonType = EquivalencyComparisonType.ByValue }));
```

To change the global rule, replace the `DefaultComparisonTypeSelector`:

```csharp
await Expect.That(subject).IsEquivalentTo(expected, o => o with
{
  DefaultComparisonTypeSelector = type => type == typeof(MyValueObject)
    ? EquivalencyComparisonType.ByValue
    : EquivalencyDefaults.DefaultComparisonType(type),
});
```

## Per-property expectations with `It.Is<T>()`

Equivalency lets you compare against an *anonymous expectation object* in which individual members assert their own
expectations via `It.Is<T>()`. Use this when you do not have a concrete expected value but want to constrain individual
properties.

```csharp
class DummyClass
{
  public string? StringValue { get; set; }
  public int IntValue { get; set; }
}

DummyClass subject = new()
{
  StringValue = "foo",
  IntValue = 42,
};

await Expect.That(subject).IsEquivalentTo(new
{
  StringValue = It.Is<string>().That.IsNotEmpty(),
  IntValue = It.Is<int>().That.IsGreaterThan(2),
});
```

`It.Is<T>()` (without `.That`) only asserts that the property has the given type.

*Note: because the type cannot be inferred from `null`, an `It.Is<T>().That.IsNull()` check still works, but
`It.Is<T>().That.IsNotNull()` requires the property to be non-null.*

## Failure messages

Failure messages list each differing member with its full path and the configured options used for the comparison, for
example:

```
Expected that subject
is equivalent to OuterClass {
    Inner = InnerClass {
      Inner = InnerClass { Value = "Baz" },
      Value = "Bar"
    },
    Value = "Foo"
  },
but it was not:
  Property Inner.Inner.Value was <null> instead of "Baz"

Equivalency options:
 - include public fields and properties
```

## Customizing the global defaults

You can change the default `EquivalencyOptions` that are used when no callback is provided, via the
[customization API](/docs/expectations/advanced/customization):

```csharp
using IDisposable scope = Customize.aweXpect.Equivalency().DefaultEquivalencyOptions
  .Set(new EquivalencyOptions().IgnoringCollectionOrder());

// All equivalency checks within this scope ignore collection order by default.
```
