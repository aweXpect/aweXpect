---
sidebar_position: 14
---

# Delegates

Describes the possible expectations for delegates and exceptions.

A delegate can be any of the following:
- `Action` or `Action<CancellationToken>`  
  a synchronous method without return value (optionally accepting a `CancellationToken` for timeout)
- `Func<Task>` or `Func<CancellationToken, Task>`  
  an asynchronous method without return value (optionally accepting a `CancellationToken` for timeout)
- `Task` or `ValueTask`  
  the result of an asynchronous method without return value
- `Func<T>` or `Func<CancellationToken, T>`  
  a synchronous method with return value `T` (optionally accepting a `CancellationToken` for timeout)
- `Func<Task<T>>` or `Func<CancellationToken, Task<T>>`  
  an asynchronous method with return value `T` (optionally accepting a `CancellationToken` for timeout)
- `Task<T>` or `ValueTask<T>`  
  the result of an asynchronous method with return value `T`


## Not throw

You can verify, that the delegate does not throw any exception:
```csharp
void Act() => {};

await Expect.That(Act).Should().NotThrow();
```


## Throw exception

You can verify, that the delegate throws an exception:
```csharp
void Act() => throw new CustomException("my exception");

await Expect.That(Act).Should().ThrowException();
```

### Specific exception

You can verify, that the delegate throws a specific exception:
```csharp
void Act() => throw new CustomException("my exception");

await Expect.That(Act).Should().Throw<CustomException>();
await Expect.That(Act).Should().Throw(typeof(CustomException));
```
This will verify that the thrown exception is of type `CustomException` or any derived type.


### Exact exception

You can verify, that the delegate throws exactly a specific exception:
```csharp
void Act() => throw new CustomException("my exception");

await Expect.That(Act).Should().ThrowExactly<CustomException>();
await Expect.That(Act).Should().ThrowExactly(typeof(CustomException));
```
This will verify that the thrown exception is of type `CustomException` and not any derived type.


### Conditional throw

You can verify, that the delegate throws an exception only if a predicate is satisfied (otherwise it verifies, that no exception is thrown):
```csharp
void Act() => throw new CustomException("my exception");
bool expectThrownException = true;

await Expect.That(Act).Should().Throw<CustomException>().OnlyIf(expectThrownException);
```
This is especially useful with parametrized tests where it depends on a parameter if an exception is thrown or not.


## Exception message

You can verify the message of the thrown exception:
```csharp
void Act() => throw new CustomException("my exception");

await Expect.That(Act).Should().ThrowException().WithMessage("my exception");
```
You can use the same configuration options as when [comparing strings](/docs/expectations/string#equality).


## Inner exceptions

You can verify the inner exception of the thrown exception;
```csharp
void Act() => throw new CustomException("outer", new CustomException("inner"));

await Expect.That(Act).Should().ThrowException().WithInnerException();
await Expect.That(Act).Should().ThrowException().WithInner<CustomException>();
```

### Recursive inner exceptions

You can recursively verify the collection of inner exceptions of the thrown exception:
```csharp
void Act() => throw new AggregateException("outer", new CustomException("inner"));

await Expect.That(Act).Should().ThrowException().WithRecursiveInnerExceptions(a => a.HaveAtLeast(1).Be<CustomException>());
```

### Other members

You can recursively verify additional members of the exception:
```csharp
void Act() => throw new MyException("outer", paramName: "paramName", hResult: 12345);

await Expect.That(Act).Should().ThrowException().WithParamName("paramName")
  .Because("you can verify the `paramName`");
await Expect.That(Act).Should().ThrowException().WithHResult(12345)
  .Because("you can verify the `HResult`");
await Expect.That(Act).Should().ThrowException()
  .Which(e => e.HResult, h => h.Should().BeGreaterThan(12340))
  .Because("you can verify arbitrary additional members");

```


## Execute within

You can verify, that the delegate finishes execution in a specified amount of time
```csharp
await Expect.That(Task.Delay(200)).Should().ExecuteWithin(TimeSpan.FromMilliseconds(300))
  .Because("it should only take about 200ms");
await Expect.That(Task.Delay(200)).Should().NotExecuteWithin(TimeSpan.FromMilliseconds(100))
  .Because("it should take at least 200ms");
```
