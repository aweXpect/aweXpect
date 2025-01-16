---
sidebar_position: 3
---

# String

Describes the possible expectations for strings.

## Equality

You can verify, that the `string` is equal to another one.  
This expectation can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or use a custom `IEqualityComparer<string>`:
```csharp
string subject = "some text";

await Expect.That(subject).Should().Be("some text")
  .Because("it is equal");
await Expect.That(subject).Should().Be("SOME TEXT").IgnoringCase()
  .Because("we ignored the casing");
await Expect.That("a\r\nb").Should().Be("a\nb").IgnoringNewlineStyle()
  .Because("we ignored the newline style");
await Expect.That(subject).Should().Be("  some text").IgnoringLeadingWhiteSpace()
  .Because("we ignored leading white-space");
await Expect.That(subject).Should().Be("some text \t").IgnoringTrailingWhiteSpace()
  .Because("we ignored trailing white-space");
await Expect.That(subject).Should().Be("SOME TEXT").Using(StringComparer.OrdinalIgnoreCase)
  .Because("the comparer ignored the casing");
```

### Wildcards

You can also compare strings using wildcards:
```csharp
string subject = "some text";

await Expect.That(subject).Should().Be("*me tex?").AsWildcard();
```

When using `AsWildcard`, the following wildcard specifiers are supported:

| Wildcard specifier | Matches                 |
|--------------------|-------------------------|
| * (asterisk)       | Zero or more characters |
| ? (question mark)  | Exactly one character   |

### Regular expressions

You can also compare strings using [regular expressions](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expressions):
```csharp
string subject = "some text";

await Expect.That(subject).Should().Be("(.*)xt").AsRegex();
```

The regex comparison uses the following [`options`](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regexoptions?view=net-8.0#fields):
- `Multiline` (always)
- `IgnoreCase` (if the `IgnoringCase` method is also used)


## One of

You can verify, that the `string` is one of many alternatives.  
This expectation can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or use a custom `IEqualityComparer<string>`:

```csharp
string subject = "some";

await Expect.That(subject).Should().BeOneOf("none", "some", "many");
await Expect.That(subject).Should().BeOneOf("NONE", "SOME", "MANY").IgnoringCase()
  .Because("we ignored the casing");
await Expect.That(subject).Should().BeOneOf("NONE", "SOME", "MANY").Using(StringComparer.OrdinalIgnoreCase)
  .Because("the comparer ignored the casing");
```


## Null, empty or white-space

You can verify, that the `string` is null, empty or contains only whitespace:
```csharp
string? subject = null;

await Expect.That(subject).IsNull();
await Expect.That("foo").Should().NotBeNull();

await Expect.That("").Should().BeEmpty();
await Expect.That("foo").Should().NotBeEmpty()
  .Because("the string is not empty");

await Expect.That(subject).IsNullOrEmpty();
await Expect.That("foo").Should().NotBeNullOrEmpty();
await Expect.That(subject).IsNullOrWhiteSpace();
await Expect.That("foo").Should().NotBeNullOrWhiteSpace();
```


## String start / end

You can verify, that the `string` starts or ends with a given string.  
These expectations can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or use a custom `IEqualityComparer<string>`:
```csharp
string subject = "some text";

await Expect.That(subject).Should().StartWith("some");
await Expect.That(subject).Should().StartWith("SOME").IgnoringCase()
  .Because("we ignored the casing");
await Expect.That(subject).Should().StartWith("SOME").Using(StringComparer.OrdinalIgnoreCase)
  .Because("the comparer ignored the casing");

await Expect.That(subject).Should().EndWith("text");
await Expect.That(subject).Should().EndWith("TEXT").IgnoringCase()
  .Because("we ignored the casing");
await Expect.That(subject).Should().EndWith("TEXT").Using(StringComparer.OrdinalIgnoreCase)
  .Because("the comparer ignored the casing");
```


## Contains

You can verify, that the `string` contains a given substring.  
These expectations can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or use a custom `IEqualityComparer<string>`:
```csharp
string subject = "some text";

await Expect.That(subject).Should().Contain("me");
await Expect.That(subject).Should().Contain("ME").IgnoringCase()
  .Because("we ignored the casing");
await Expect.That(subject).Should().Contain("ME").Using(StringComparer.OrdinalIgnoreCase)
  .Because("the comparer ignored the casing");
```

You can also specify, how often the substring should be found:
```csharp
string subject = "In this text in between the word an investigator should find the word 'IN' multiple times.";

await Expect.That(subject).Should().Contain("in").AtLeast(2)
  .Because("'in' can be found 3 times");
await Expect.That(subject).Should().Contain("in").Exactly(3)
  .Because("'in' can be found 3 times");
await Expect.That(subject).Should().Contain("in").AtMost(4)
  .Because("'in' can be found 3 times");
await Expect.That(subject).Should().Contain("in").Between(1).And(5)
  .Because("'in' can be found 3 times");
```


## Character casing

You can verify, that the characters in a `string` are all upper or lower cased:
```csharp
await Expect.That("1ST PLACE").Should().BeUpperCased()
  .Because("it contains no lowercase characters");
await Expect.That("1st PLACE").Should().NotBeUpperCased()
  .Because("it contains at least one lowercase characters");

await Expect.That("1st place").Should().BeLowerCased()
  .Because("it contains no uppercase characters");
await Expect.That("1st PLACE").Should().NotBeLowerCased()
  .Because("it contains at least one uppercase characters");
```
