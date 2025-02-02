---
sidebar_position: 3
---

# String

Describes the possible expectations for strings.

## Equality

You can verify, that the `string` is equal to another one.  
This expectation can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or
use a custom `IEqualityComparer<string>`:

```csharp
string subject = "some text";

await Expect.That(subject).IsEqualTo("some text")
  .Because("it is equal");
await Expect.That(subject).IsEqualTo("SOME TEXT").IgnoringCase()
  .Because("we ignored the casing");
await Expect.That("a\r\nb").IsEqualTo("a\nb").IgnoringNewlineStyle()
  .Because("we ignored the newline style");
await Expect.That(subject).IsEqualTo("  some text").IgnoringLeadingWhiteSpace()
  .Because("we ignored leading white-space");
await Expect.That(subject).IsEqualTo("some text \t").IgnoringTrailingWhiteSpace()
  .Because("we ignored trailing white-space");
await Expect.That(subject).IsEqualTo("SOME TEXT").Using(StringComparer.OrdinalIgnoreCase)
  .Because("the comparer ignored the casing");
```

### Wildcards

You can also compare strings using wildcards:

```csharp
string subject = "some text";

await Expect.That(subject).IsEqualTo("*me tex?").AsWildcard();
```

When using `AsWildcard`, the following wildcard specifiers are supported:

| Wildcard specifier | Matches                 |
|--------------------|-------------------------|
| * (asterisk)       | Zero or more characters |
| ? (question mark)  | Exactly one character   |

### Regular expressions

You can also compare strings
using [regular expressions](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expressions):

```csharp
string subject = "some text";

await Expect.That(subject).IsEqualTo("(.*)xt").AsRegex();
```

The regex comparison uses the following [
`options`](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regexoptions?view=net-8.0#fields):

- `Multiline` (always)
- `IgnoreCase` (if the `IgnoringCase` method is also used)

## One of

You can verify, that the `string` is one of many alternatives.  
This expectation can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or
use a custom `IEqualityComparer<string>`:

```csharp
string subject = "some";

await Expect.That(subject).IsOneOf("none", "some", "many");
await Expect.That(subject).IsOneOf("NONE", "SOME", "MANY").IgnoringCase()
  .Because("we ignored the casing");
await Expect.That(subject).IsOneOf("NONE", "SOME", "MANY").Using(StringComparer.OrdinalIgnoreCase)
  .Because("the comparer ignored the casing");
```

## Null, empty or white-space

You can verify, that the `string` is null, empty or contains only whitespace:

```csharp
string? subject = null;

await Expect.That(subject).IsNull();
await Expect.That("foo").IsNotNull();

await Expect.That("").IsEmpty();
await Expect.That("foo").IsNotEmpty()
  .Because("the string is not empty");

await Expect.That(subject).IsNullOrEmpty();
await Expect.That("foo").IsNotNullOrEmpty();
await Expect.That(subject).IsNullOrWhiteSpace();
await Expect.That("foo").IsNotNullOrWhiteSpace();
```

## Length

You can verify, that the `string` has the expected length:

```csharp
string subject = "some value";

await Expect.That(subject).HasLength().EqualTo(10);
await Expect.That(subject).HasLength().NotEqualTo(9);

await Expect.That(subject).HasLength().GreaterThan(8);
await Expect.That(subject).HasLength().GreaterThanOrEqualTo(9);
await Expect.That(subject).HasLength().LessThanOrEqualTo(11);
await Expect.That(subject).HasLength().LessThan(12);
```

## String start / end

You can verify, that the `string` starts or ends with a given string.  
These expectations can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or
use a custom `IEqualityComparer<string>`:

```csharp
string subject = "some text";

await Expect.That(subject).StartsWith("some");
await Expect.That(subject).StartsWith("SOME").IgnoringCase()
  .Because("we ignored the casing");
await Expect.That(subject).StartsWith("SOME").Using(StringComparer.OrdinalIgnoreCase)
  .Because("the comparer ignored the casing");

await Expect.That(subject).EndsWith("text");
await Expect.That(subject).EndsWith("TEXT").IgnoringCase()
  .Because("we ignored the casing");
await Expect.That(subject).EndsWith("TEXT").Using(StringComparer.OrdinalIgnoreCase)
  .Because("the comparer ignored the casing");
```

## Contains

You can verify, that the `string` contains a given substring.  
These expectations can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or
use a custom `IEqualityComparer<string>`:

```csharp
string subject = "some text";

await Expect.That(subject).Contains("me");
await Expect.That(subject).Contains("ME").IgnoringCase()
  .Because("we ignored the casing");
await Expect.That(subject).Contains("ME").Using(StringComparer.OrdinalIgnoreCase)
  .Because("the comparer ignored the casing");
```

You can also specify, how often the substring should be found:

```csharp
string subject = "In this text in between the word an investigator should find the word 'IN' multiple times.";

await Expect.That(subject).Contains("in").AtLeast(2)
  .Because("'in' can be found 3 times");
await Expect.That(subject).Contains("in").Exactly(3)
  .Because("'in' can be found 3 times");
await Expect.That(subject).Contains("in").AtMost(4)
  .Because("'in' can be found 3 times");
await Expect.That(subject).Contains("in").Between(1).And(5)
  .Because("'in' can be found 3 times");
```

## Character casing

You can verify, that the characters in a `string` are all upper or lower cased:

```csharp
await Expect.That("1ST PLACE").IsUpperCased()
  .Because("it contains no lowercase characters");
await Expect.That("1st PLACE").IsNotUpperCased()
  .Because("it contains at least one lowercase characters");

await Expect.That("1st place").IsLowerCased()
  .Because("it contains no uppercase characters");
await Expect.That("1st PLACE").IsNotLowerCased()
  .Because("it contains at least one uppercase characters");
```
