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

## Migration

We added support to migrate from other testing frameworks.

1. Temporarily install the
   [aweXpect.Migration](https://github.com/aweXpect/aweXpect.Migration) package [![Nuget](https://img.shields.io/nuget/v/aweXpect.Migration)](https://www.nuget.org/packages/aweXpect.Migration)
   in the test project and add the following global using statements in the test project:
   ```csharp
   global using System.Threading.Tasks;
   global using aweXpect;
   ```

2. Depending on the framework, the assertions will be marked with a warning:
	- For [FluentAssertions](https://fluentassertions.com/):  
      All usages of `.Should()` will be marked with
	  `aweXpectM002: fluentassertions should be migrated to aweXpect`
	- For [Xunit](https://xunit.net/):  
      All usages of `Assert` will be marked with `aweXpectM003: Xunit assertions should be migrated to aweXpect`

3. Most warnings can be automatically fixed with a code fix provider. Make sure to await all migrated expectations (fix `aweXpect0001: Expectations must be awaited or verified`).

4. Fix the remaining warnings manually.

5. Remove the `aweXpect.Migration` package again.

## Detecting test frameworks

We support a lot of different unit testing frameworks:

- [Microsoft Test Framework](https://github.com/microsoft/testfx/)
- [xUnit](https://xunit.net/) (v2 & v3)
- [NUnit](https://nunit.org/) (v3 & v4)
- [TUnit](https://thomhurst.github.io/TUnit/)

When you have a reference to the corresponding test framework
assembly, we will automatically throw the corresponding exceptions.
