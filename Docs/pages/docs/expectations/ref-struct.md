---
sidebar_position: 18
---

# Ref struct types

Describes how you can work with `ref` structure types

[ref struct types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/ref-struct) can't be
used in an `async` context.  
In order to allow using aweXpect on properties of `ref struct` types, there is a built-in workaround to verify the
expectation synchronously, so that the test method itself can remain synchronous:

```csharp
ReadOnlySpan<char> subject = @"foo".AsSpan();

Synchronously.Verify(Expect.That(subject.Length).IsEqualTo(3));
// or alternatively:
Expect.That(subject.Length).IsEqualTo(3).Verify();
```

*Note: These methods are in the namespace `aweXpect.Synchronous`.*
