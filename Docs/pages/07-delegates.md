# Delegates

Describes the possible expectations for delegates and exceptions.

A delegate can be any of the following:

- `Action` or `Action<CancellationToken>`  
  a synchronous method without return value (optionally accepting a `CancellationToken` for timeout)
- `Func<Task>` or `Func<CancellationToken, Task>`  
  an asynchronous method without return value (optionally accepting a `CancellationToken` for timeout)
- `Func<ValueTask>` or `Func<CancellationToken, ValueTask>`  
  an asynchronous method using `ValueTask` without return value (optionally accepting a `CancellationToken` for timeout)
- `Func<T>` or `Func<CancellationToken, T>`  
  a synchronous method with return value `T` (optionally accepting a `CancellationToken` for timeout)
- `Func<Task<T>>` or `Func<CancellationToken, Task<T>>`  
  an asynchronous method with return value `T` (optionally accepting a `CancellationToken` for timeout)
- `Func<ValueTask<T>>` or `Func<CancellationToken, ValueTask<T>>`  
  an asynchronous method using `ValueTask` with return value `T` (optionally accepting a `CancellationToken` for
  timeout)

## Not throw

You can verify that the delegate does not throw any exception:

```csharp
void Act() => {};

await Expect.That(Act).DoesNotThrow();
```

## Throw exception

You can verify that the delegate throws an exception:

```csharp
void Act() => throw new CustomException("my exception");

await Expect.That(Act).ThrowsException();
```

### Specific exception

You can verify that the delegate throws a specific exception:

```csharp
void Act() => throw new CustomException("my exception");

await Expect.That(Act).Throws<CustomException>();
await Expect.That(Act).Throws(typeof(CustomException));
```

This will verify that the thrown exception is of type `CustomException` or any derived type.

### Exact exception

You can verify that the delegate throws exactly a specific exception:

```csharp
void Act() => throw new CustomException("my exception");

await Expect.That(Act).ThrowsExactly<CustomException>();
await Expect.That(Act).ThrowsExactly(typeof(CustomException));
```

This will verify that the thrown exception is of type `CustomException` and not any derived type.

### Conditional throw

You can verify that the delegate throws an exception only if a predicate is satisfied (otherwise it verifies, that no
exception is thrown):

```csharp
void Act() => throw new CustomException("my exception");
bool expectThrownException = true;

await Expect.That(Act).Throws<CustomException>().OnlyIf(expectThrownException);
```

This is especially useful with parametrized tests where it depends on a parameter if an exception is thrown or not.

## Exception message

You can verify the message of the thrown exception:

```csharp
void Act() => throw new CustomException("This is my exception text");

await Expect.That(Act).ThrowsException().WithMessage("This is my exception text");
await Expect.That(Act).ThrowsException().WithoutMessage("some other text");
await Expect.That(Act).ThrowsException().WithMessageContaining("my exception");
await Expect.That(Act).ThrowsException().WithoutMessageContaining("something else");
```

You can use the same configuration options as when [comparing strings](/docs/expectations/string#equality).

## Inner exceptions

You can verify the inner exception of the thrown exception;

```csharp
void Act() => throw new CustomException("outer", new CustomException("inner"));

await Expect.That(Act).ThrowsException().WithInnerException();
await Expect.That(Act).ThrowsException().WithInner<CustomException>();
```

### Recursive inner exceptions

You can recursively verify the collection of inner exceptions of the thrown exception:

```csharp
void Act() => throw new AggregateException("outer", new CustomException("inner"));

await Expect.That(Act).ThrowsException().WithRecursiveInnerExceptions(innerExceptions => innerExceptions.HasAtLeast(1).Be<CustomException>());
```

### Other members

You can recursively verify additional members of the exception:

```csharp
var exception = new MyException("outer", paramName: "paramName", hResult: 12345);
void Act() => throw exception;

await Expect.That(Act).ThrowsException().WithParamName("paramName")
  .Because("you can verify the `paramName`");
await Expect.That(Act).ThrowsException().WithHResult(12345)
  .Because("you can verify the `HResult`");
await Expect.That(Act).ThrowsException()
  .Whose(e => e.HResult, h => h.IsGreaterThan(12340))
  .Because("you can verify arbitrary additional members");
await Expect.That(Act).ThrowsException()
  .Which.IsSameAs(exception)
  .Because("you can access the thrown exception");

```

## Execution time

You can verify that the execution time of a delegate:

```csharp
await Expect.That(Task.Delay(200)).ExecutesIn().AtMost(300.Milliseconds())
  .Because("the delegate should execute faster than 300ms");
await Expect.That(Task.Delay(200)).ExecutesIn().AtLeast(100.Milliseconds())
  .Because("the delegate should execute slower than 100ms");
await Expect.That(Task.Delay(200)).ExecutesIn().Approximately(200.Milliseconds(), 50.Milliseconds())
  .Because("the delegate should execute within 200ms ± 50ms");
await Expect.That(Task.Delay(200)).ExecutesIn().Between(100.Milliseconds()).And(300.Milliseconds())
  .Because("the delegate should execute slower than 100ms and faster than 300ms");
```

### Execute within

There is also a shorthand expectation for a delegate that finishes the execution without throwing an exception
in (at most) a given time:

```csharp
await Expect.That(Task.Delay(200)).ExecutesWithin(TimeSpan.FromMilliseconds(300))
  .Because("it should only take about 200ms");
await Expect.That(Task.Delay(200)).DoesNotExecuteWithin(TimeSpan.FromMilliseconds(100))
  .Because("it should take at least 200ms");
```
