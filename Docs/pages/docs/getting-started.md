---
sidebar_position: 1
---

# Getting started

## Installation

1. Install the [`aweXpect`](https://www.nuget.org/packages/aweXpect) nuget package
   ```ps
   dotnet add package aweXpect
   ```


2. Add the following `using` statement:
   ```csharp
   using aweXpect;
   ```
   This brings the static `Expect` class and lots of extension methods into scope.


3. Simplify expectations (optional)  
   If you want to simplify the assertions, you can add a `global using static aweXpect.Expect;` statement anywhere in
   your test project.
   This allows writing a more concise syntax:
   ```csharp
   //    ↓ Default behaviour
   await Expect.That(subject).IsTrue();
   await That(subject).IsTrue();
   //    ↑ With global static
   ```

## Write your first expectation

Write your first expectation:

```csharp
[Fact]
public async Task SomeMethod_WhenInputIsInvalid_ShouldReturnFalse()
{
  bool result = SomeMethod("invalid input");
  
  await Expect.That(result).IsFalse();
}
```

If it fails, it will throw a [framework-specific](#detecting-test-frameworks) exception with the following message:

> ```
> Expected result to
> be False,
> but it was True
> ```

## Add a reason

You can add a reason for all expectations, that will be included in the exception message:

```csharp
[Fact]
public async Task SomeMethod_WhenInputIsInvalid_ShouldReturnFalse()
{
  bool result = SomeMethod("invalid input");
  
  await Expect.That(result).IsFalse().Because("the input was invalid");
}
```

This will result in
> ```
> Expected result to
> be False, because the input was invalid,
> but it was True
> ```

## Detecting test frameworks

We support a lot of different unit testing frameworks:
- [Microsoft Test Framework](https://github.com/microsoft/testfx/)
- [xUnit](https://xunit.net/) (v2 & v3)
- [NUnit](https://nunit.org/) (v3 & v4)
- [TUnit](https://thomhurst.github.io/TUnit/)

When you have a reference to the corresponding test framework
assembly, we will automatically throw the corresponding exceptions.

## Multiple expectations
You can combine multiple expectations in different ways:

### On the same property

Simply use `.And` or `.Or` to combine multiple expectations, e.g.
```csharp
string subject = "something different"
await Expect.That(subject).Should().StartWith("some").And.EndWith("text");
```
> ```
> Expected subject to
> start with "some" and end with "text",
> but it was "something different"
> ```

### On different properties of the same subject

Use the `For`-syntax to access different properties of a common subject and combine them again with `.And` or `.Or`, e.g.
```csharp
  public record MyClass(int Status, string Content);
  MyClass subject = new(1, "some other content");
  
  await Expect.That(subject)
    .For(x => x.Status, x => x.Should().BeGreaterThan(1)).And
    .For(x => x.Content, x => x.Should().Be("some content"));
```
> ```
> Expected subject to
> for .Status be greater than 1 and for .Content be equal to "some content",
> but .Status was 1 and .Content was "some other content" which differs at index 5:
>         ↓ (actual)
>   "some other content"
>   "some content"
>         ↑ (expected)
> ```

### On different subjects

Use the `Expect.ThatAll` or `Expect.ThatAny` syntax to combine arbitrary expectations, e.g.
```csharp
  string subjectA = "ABC";
  string subjectB = "XYZ";
  
  await Expect.ThatAll(
    Expect.That(subjectA).Should().Be("ABC"),
    Expect.That(subjectB).Should().Be("DEF"));
```
> ```
> Expected all of the following to succeed:
>  [01] Expected subjectA to be equal to "ABC"
>  [02] Expected subjectB to be equal to "DEF"
> but
>  [02] it was "XYZ" which differs at index 0:
>          ↓ (actual)
>         "XYZ"
>         "DEF"
>          ↑ (expected)
> ```
