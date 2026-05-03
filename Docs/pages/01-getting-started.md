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

If it fails, it will throw a framework-specific exception with the following message:

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
