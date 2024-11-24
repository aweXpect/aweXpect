---
sidebar_position: 1
---

# Getting started

## Installation

Install the [`aweXpect`](https://www.nuget.org/packages/aweXpect) nuget package
```ps
dotnet add package aweXpect
```

Optional: If you want to simplify the assertions, you can add a `global using static aweXpect.Expect;` statement anywhere in your test project.

## Write your first expectation

Write your first expectation:
```csharp
[Fact]
public async Task SomeMethod_ShouldThrowArgumentNullException()
{
  await That(SomeMethod).Should().Throw<ArgumentNullException>()
    .WithMessage("Value cannot be null")
    .Because("we tested the null edge case");
}
```
