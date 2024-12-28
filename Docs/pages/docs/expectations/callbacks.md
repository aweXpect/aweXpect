---
sidebar_position: 16
---

# Callbacks

Describes the possible expectations for working with callbacks.

## Recording

First, you have to start recording callback signals. This class is available in the "aweXpect.Recording" namespace.

```csharp
// ↓ Counts signals from callbacks without parameters
SignalCounter signal = new();
SignalCounter<string> signal = new();
// ↑ Counts signals from callbacks with a string parameter
```

Then, you can signal the callback on the recording.

```csharp
class MyClass
{
  public void Execute(Action<string> onCompleted)
  {
    // do something in a background thread and then call the onCompleted callback
  }
}

sut.Execute(v => signal.Signal(v));
```

At last, you can wait for the callback to be signaled:

```csharp
await Expect.That(signal).Should().BeSignaled();
```

You can also verify that the callback will not be signaled:

```csharp
await Expect.That(signal).Should().NotBeSignaled();
```

*NOTE: The last statement will result never return, unless a timeout or cancellation is specified.
Therefore, when nothing is specified, a default timeout of 30 seconds is applied!*

### Timeout

You can specify a timeout, how long you want to wait for the callback to be signaled:

```csharp
await Expect.That(signal).Should().BeSignaled().Within(TimeSpan.FromSeconds(5))
  .Because("it should take at most 5 seconds to complete");
```

Alternatively you can also use a `CancellationToken` for a timeout:

```csharp
CancellationToken cancellationToken = new CancellationTokenSource(5000).Token;
await Expect.That(signal).Should().BeSignaled().WithCancellation(cancellationToken)
  .Because("it should be completed, before the cancellationToken is cancelled");
```

### Amount

You can specify a number of times, that a callback must at least be signaled:

```csharp
await Expect.That(signal).Should().BeSignaled(3.Times());
```

You can also verify, that the callback was not signaled at least the given number of times:

```csharp
await Expect.That(signal).Should().NotBeSignaled(3.Times());
```

### Parameters

You can also include a parameter during signaling:

```csharp
SignalCounter<string> signal = new();

signal.Signal("foo");
signal.Signal("bar");

await That(signal).Should().BeSignaled(2.Times());
```

You can filter for signals with specific parameters by providing a `predicate`:
```csharp
SignalCounter<string> signal = new();

signal.Signal("foo");
signal.Signal("bar");
signal.Signal("foo");

await That(signal).Should().BeSignaled(2.Times()).With(p => p == "foo");
```

*In case of a failed expectation, the recorded parameters will be displayed in the error message.*
