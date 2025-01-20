---
sidebar_position: 1
---

# Write your own extension

This library will never be able to cope with all ideas and use cases. Therefore, it is possible to use the [
`aweXpect.Core`](https://www.nuget.org/packages/aweXpect.Core/) package and write your own extensions.
Goal of this package is to be more stable than the main aweXpect package, so reduce the risk of version conflicts
between different extensions.

You can extend the functionality for any types, by adding extension methods on `IThat<TType>`.

## Example

You want to verify that a string corresponds to an absolute path.

```csharp
/// <summary>
///     Verifies that the <paramref name="subject"/> is an absolute path.
/// </summary>
public static AndOrResult<string, IThat<string>> IsAbsolutePath(
  this IThat<string> subject)
  => new(subject.ExpectationBuilder.AddConstraint(it
      => new IsAbsolutePathConstraint(it)),
    subject);

private readonly struct IsAbsolutePathConstraint(string it) : IValueConstraint<string>
{
  public ConstraintResult IsMetBy(string actual)
  {
    var absolutePath = Path.GetFullPath(actual);
    if (absolutePath == actual)
    {
      return new ConstraintResult.Success<string>(actual, "be an absolute path");
    }

    return new ConstraintResult.Failure("be an absolute path",
      $"{it} found {Formatter.Format(actual)}");
  }
}
```

## Constraints

The basis for expectations are constraints. You can add different constraints to the `ExpectationBuilder` that is
available for the `IThat<T>`. They differ in the input and output parameters:

- `IValueConstraint<T>`   
  It receives the actual value `T` and returns a `ConstraintResult`.
- `IAsyncConstraint<T>`  
  It receives the actual value `T` and a `CancellationToken` and returns the `ConstraintResult` asynchronously.  
  *Use it when you need asynchronous functionality or access to the timeout `CancellationToken`.*
- `IContextConstraint<T>` / `IAsyncContextConstraint<T>`  
  Similar to the `IValueConstraint<T>` and `IAsyncConstraint<T>` respectively but receives an additional
  `IEvaluationContext` parameter that allows storing and receiving data between expectations.  
  *This mechanism is used for example to avoid enumerating an `IEnumerable` multiple times across multiple constraints.*
