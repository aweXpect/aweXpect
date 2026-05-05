# Equivalency

Describes how to verify that two objects are *equivalent* — that is, structurally equal — rather than referentially or
strictly equal. Equivalency walks both objects recursively and compares them member by member.

## Overview

Equality (`IsEqualTo`) delegates to `object.Equals`, which for most reference types means *reference* equality.
Equivalency instead compares the public state of two objects field by field and property by property, recursing into
nested objects and collections. Two objects are equivalent when every included member compares as equivalent.

```csharp
class Pirate(string name)
{
  public string Name { get; } = name;
}
Pirate jack = new("Jack");

await Expect.That(jack).IsEquivalentTo(new Pirate("Jack"));
await Expect.That(jack).IsNotEquivalentTo(new Pirate("Hector"));
```

## Where equivalency is available

Equivalency is exposed on three different surfaces.

### Direct on objects

`IsEquivalentTo` and `IsNotEquivalentTo` are extension methods on any object. They accept an optional callback to
configure the comparison via [`EquivalencyOptions<TExpected>`](#configuration).

```csharp
await Expect.That(pirate).IsEquivalentTo(expected);
await Expect.That(pirate).IsEquivalentTo(expected, o => o.IgnoringMember("Bounty"));
await Expect.That(pirate).IsNotEquivalentTo(landlubber);
```

### On collection elements

`AreEquivalentTo` checks every selected element of an `IEnumerable<T>` (or `IAsyncEnumerable<T>`) against a single
expected value, using the same equivalency comparison.

```csharp
IEnumerable<Pirate> crew = //...
Pirate captain = //...

await Expect.That(crew).All().AreEquivalentTo(captain);
await Expect.That(crew).AtLeast(2).AreEquivalentTo(captain, o => o.IgnoringMember("Name"));
```

### As a modifier on equality assertions

For expectations that accept a custom equality comparer (`IsEqualTo`, `Contains`, `StartsWith`, `EndsWith`, `HasItem`,
`All().AreEqualTo(...)`, …), append `.Equivalent()` to switch the comparison from `Equals` to structural equivalency.

```csharp
await Expect.That(pirate).IsEqualTo(captain).Equivalent();

IEnumerable<Pirate> crew = //...
await Expect.That(crew).Contains(captain).Equivalent();
await Expect.That(crew).StartsWith(captain).Equivalent();
await Expect.That(crew).All().AreEqualTo(captain).Equivalent(o => o.IgnoringCollectionOrder());
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
await Expect.That(pirate).IsEquivalentTo(expected, o => o
  .IncludingFields(IncludeMembers.Public | IncludeMembers.Internal)
  .IgnoringMember("Bounty")
  .IgnoringCollectionOrder());
```

### Ignoring members by name

```csharp
await Expect.That(pirate).IsEquivalentTo(expected, o => o.IgnoringMember("Bounty"));
```

The match is case-insensitive. For nested members, the path is dot-separated (e.g. `"Ship.Name"`); for collection
elements, the index is bracketed (e.g. `"Ship.Cargo[3]"`).

### Ignoring members by predicate

There are four overloads of `Ignoring`, depending on which information you need:

```csharp
// by member path and type
await Expect.That(pirate).IsEquivalentTo(expected, o => o
  .Ignoring((memberPath, memberType)
    => memberPath.EndsWith("Bounty") && memberType == typeof(int)));

// by member path only
await Expect.That(pirate).IsEquivalentTo(expected, o => o
  .Ignoring(memberPath => memberPath == "Ship.Name"));

// by type only
await Expect.That(pirate).IsEquivalentTo(expected, o => o
  .Ignoring(memberType => memberType == typeof(DateTime)));

// by member path, type and reflected MemberInfo
await Expect.That(pirate).IsEquivalentTo(expected, o => o
  .Ignoring((memberPath, _, memberInfo)
    => memberPath.EndsWith("Bounty") && memberInfo is PropertyInfo));
```

### Including fields and properties

You can change which fields and properties participate in the comparison. Both methods accept an `IncludeMembers` flags
enum with the values `None`, `Public`, `Internal` and `Private`.

```csharp
await Expect.That(pirate).IsEquivalentTo(expected, o => o
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
await Expect.That(pirate).IsEquivalentTo(expected, o => o
  .For<Ship>(x => x.IgnoringMember("Cargo"))
  .For<List<string>>(x => x.IgnoringCollectionOrder()));
```

### Comparing by value or by members

Each type can be compared either by value (`Equals`) or by walking its members. The default is determined by the type
itself (see [Default behaviour](#default-behaviour)). To override for a specific type:

```csharp
await Expect.That(pirate).IsEquivalentTo(expected, o => o
  .For<Coordinate>(x => x with { ComparisonType = EquivalencyComparisonType.ByValue }));
```

To change the global rule, replace the `DefaultComparisonTypeSelector`:

```csharp
await Expect.That(pirate).IsEquivalentTo(expected, o => o with
{
  DefaultComparisonTypeSelector = type => type == typeof(Coordinate)
    ? EquivalencyComparisonType.ByValue
    : EquivalencyDefaults.DefaultComparisonType(type),
});
```

### Customizing the global defaults

You can change the default `EquivalencyOptions` that are used when no callback is provided, via the
[customization API](/docs/expectations/advanced/customization):

```csharp
using IDisposable scope = Customize.aweXpect.Equivalency().DefaultEquivalencyOptions
  .Set(new EquivalencyOptions().IgnoringCollectionOrder());

// All equivalency checks within this scope ignore collection order by default.
```

## Per-property expectations with `It.Is<T>()`

Equivalency lets you compare against an *anonymous expectation object* in which individual members assert their own
expectations via `It.Is<T>()`. Think of it as matching a subject against a wanted poster: each property carries its
own description rather than a concrete value.

```csharp
class Suspect
{
  public string? Alias { get; set; }
  public int HeistsPulled { get; set; }
}

Suspect cat = new()
{
  Alias = "The Cat",
  HeistsPulled = 42,
};

await Expect.That(cat).IsEquivalentTo(new
{
  Alias = It.Is<string>().That.IsNotEmpty(),
  HeistsPulled = It.Is<int>().That.IsGreaterThan(2),
});
```

`It.Is<T>()` (without `.That`) only asserts that the property has the given type.

*Note: because the type cannot be inferred from `null`, an `It.Is<T>().That.IsNull()` check still works, but
`It.Is<T>().That.IsNotNull()` requires the property to be non-null.*

## Failure messages

Failure messages list each differing member with its full path and the configured options used for the comparison.

For a structural mismatch:

```
Expected that captain
is equivalent to Captain {
    Name = "Hook",
    Ship = Ship { Name = "Black Pearl" }
  },
but it was not:
  Property Ship.Name was "Jolly Roger" instead of "Black Pearl"

Equivalency options:
 - include public fields and properties
```

When the wanted-poster pattern with `It.Is<T>()` fails, the failure renders each member's expectation inline:

```
Expected that cat
is equivalent to { Alias = is string that is not empty, HeistsPulled = is int that is greater than 2 },
but it was not:
  Property HeistsPulled was 1

Equivalency options:
 - include public fields and properties
```
