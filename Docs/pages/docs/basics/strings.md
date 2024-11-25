---
sidebar_position: 2
---

# Strings

String values support the following expectations:

## Null, empty or white-space

For asserting whether a string is null, empty or contains whitespace only, you can use the following expectations:

```csharp
string subject = "";
await Expect.That(subject).Should().NotBeNull();
await Expect.That(subject).Should().BeNull();
await Expect.That(subject).Should().BeEmpty();
await Expect.That(subject).Should().NotBeEmpty().Because("the string is not empty");
await Expect.That(subject).Should().HaveLength(0);
await Expect.That(subject).Should().BeNullOrWhiteSpace();
await Expect.That(subject).Should().NotBeNullOrWhiteSpace();
```

## Equality

Equality comparison can be configured to ignore case, use wildcard or regex expressions or even use a custom `IEqualityComparer<string>`:

```csharp
string subject = "some text";
await Expect.That(subject).Should().Be("some text");
await Expect.That(subject).Should().Be("SOME TEXT").IgnoringCase();
await Expect.That(subject).Should().Be("*me tex?").AsWildcard();
await Expect.That(subject).Should().Be("(.*)xt").AsRegex();
await Expect.That(subject).Should().Be("SOME TEXT").Using(StringComparer.OrdinalIgnoreCase);
```

### Wildcards

When using `AsWildcard`, the following wildcard specifiers are supported:

| Wildcard specifier  | Matches                                   |
|---------------------|-------------------------------------------|
| * (asterisk)        | Zero or more characters in that position. |
| ? (question mark)   | Exactly one character in that position.   |

## String start and end

To compare only the beginning or end of the string, you can use `StartWith` or `EndWith` respectively. Both methods can be configured to ignore case or use a custom `IEqualityComparer<string>`:

```csharp
string subject = "some text";
await Expect.That(subject).Should().StartWith("some");
await Expect.That(subject).Should().StartWith("SOME").IgnoringCase();
await Expect.That(subject).Should().StartWith("SOME").Using(StringComparer.OrdinalIgnoreCase);

await Expect.That(subject).Should().EndWith("text");
await Expect.That(subject).Should().EndWith("TEXT").IgnoringCase();
await Expect.That(subject).Should().EndWith("TEXT").Using(StringComparer.OrdinalIgnoreCase);
```

## Contains

To compare substrings within a string, you can use `Contain`. This method can be configured to ignore case or use a custom `IEqualityComparer<string>`:

```csharp
string subject = "some text";
await Expect.That(subject).Should().Contain("me");
await Expect.That(subject).Should().Contain("ME").IgnoringCase();
await Expect.That(subject).Should().Contain("ME").Using(StringComparer.OrdinalIgnoreCase);
```

Additionally you can specify, how often the substring should be found:
```csharp
string subject = "In this text in between the word an investigator should find the word 'IN' multiple times.";
await Expect.That(subject).Should().Contain("in").AtLeast(2);
await Expect.That(subject).Should().Contain("in").Exactly(3);
await Expect.That(subject).Should().Contain("in").AtMost(4);
await Expect.That(subject).Should().Contain("in").Between(1).And(5);
```

## Character casing
To ensure the characters in a string are all upper or lower cased, you can use the following expectations:

```csharp
await Expect.That(subject).Should().BeUpperCased();
await Expect.That(subject).Should().NotBeUpperCased();
await Expect.That(subject).Should().BeLowerCased();
await Expect.That(subject).Should().NotBeLowerCased();
```
