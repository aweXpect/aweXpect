---
sidebar_position: 1
---

# Write your own extension

This library will never be able to cope with all ideas and use cases. Therefore it is possible to use the [`aweXpect.Core`](https://www.nuget.org/packages/aweXpect.Core/) package and write your own extensions.

## For existing types
You can extend the functionality for existing types, by adding extension methods on `IThat<TType>`.

### Example 1
You want to verify that a string corresponds to an absolute path.

```csharp
/// <summary>
///     Verifies that the <paramref name="subject"/> is an absolute path.
/// </summary>
public static AndOrResult<string, IThat<string>> BeAbsolutePath(
  this IThat<string> subject)
  => new(subject.ExpectationBuilder.AddConstraint(it
      => new BeAbsolutePathConstraint(it)),
    subject);

private readonly struct BeAbsolutePathConstraint(string it) : IValueConstraint<string>
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

### Constraints
The basis for expectations are constraints. You can add different constraints to the `ExpectationBuilder` that is available for the `IThat<T>`. They differ in the input and output parameters:
- `IValueConstraint<T>`   
  It receives the actual value `T` and returns a `ConstraintResult`.
- `IAsyncConstraint<T>`  
  It receives the actual value `T` and a `CancellationToken` and returns the `ConstraintResult` asynchronously.  
  *Use it when you need asynchronous functionality or access to the timeout `CancellationToken`.*
- `IContextConstraint<T>` / `IAsyncContextConstraint<T>`  
  Similar to the `IValueConstraint<T>` and `IAsyncConstraint<T>` respectively but receives an additional `IEvaluationContext` parameter that allows storing and receiving data between expectations.  
  *This mechanism is used for example to avoid enumerating an `IEnumerable` multiple times across multiple constraints.*

## For new types
You can extend the functionality to new types, by adding a `.Should()` extension methods on `IExpectSubject<TType>` and then the corresponding methods on `IThat<TType>`.

### Example 2
You want to verify that a directory is empty.

First you need to enable expectations for `DirectoryInfo`:
```csharp
public static class DirectoryInfoExtensions
{
  public static IThat<DirectoryInfo> Should(this IExpectSubject<DirectoryInfo> subject)
    => subject.Should(_ => {});
}
```

Then you can add the [extension method on `IThat<DirectoryInfo>`](#for-existing-types):
```csharp
/// <summary>
///     Verifies that the <paramref name="directory"/> is empty.
/// </summary>
public static AndOrResult<DirectoryInfo, IThat<DirectoryInfo>> BeEmpty(
  this IThat<DirectoryInfo> directory)
  => new(directory.ExpectationBuilder.AddConstraint(it
      => new BeEmptyConstraint(it)),
    directory);

private readonly struct BeEmptyConstraint(string it) : IValueConstraint<DirectoryInfo>
{
  public ConstraintResult IsMetBy(DirectoryInfo actual)
  {
    var fileCount = actual.GetFiles().Length;
    var directoryCount = actual.GetDirectories().Length;
    if (fileCount + directoryCount == 0)
    {
      return new ConstraintResult.Success<DirectoryInfo>(actual, "be empty");
    }

    return new ConstraintResult.Failure("be empty",
      $"{it} contained {fileCount} files and {directoryCount} directories");
  }
}
```
