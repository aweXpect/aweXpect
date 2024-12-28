---
sidebar_position: 16
---

# Callbacks

Describes the possible expectations for working with callbacks.

## Recording

First, you have to start a recording of callbacks. This method is available in the "aweXpect.Recording" namespace.

```csharp
// ↓ Record callbacks without parameters
ICallbackRecording recording = Record.Callback();
ICallbackRecording<string> recording = Record.Callback<string>();
// ↑ Records callbacks with a string parameter
```

Then you can trigger the callback on the recording.

```csharp
class MyClass
{
  public void Execute(Action<string> onCompleted)
  {
    // do something in a background thread and then call the onCompleted callback
  }
}

sut.Execute(v => recording.Trigger(v));
```

At last you can wait for the callback to be triggered:

```csharp
await Expect.That(recording).Should().Trigger();
```

You can also verify that the callback will not be triggered:

```csharp
await Expect.That(recording).Should().NotTrigger();
```

*NOTE: The last statement will result never return, unless a timeout or cancellation is specified. Therefore, when
nothing is specified, a default timeout of 30 seconds is applied!*

### Timeout

You can specify a timeout, how long you want to wait for the callback to be triggered:

```csharp
await Expect.That(recording).Should().Trigger().Within(TimeSpan.FromSeconds(5))
  .Because("it should take at most 5 seconds to complete");
```

Alternatively you can also use a `CancellationToken` for a timeout:

```csharp
CancellationToken cancellationToken = new CancellationTokenSource(5000).Token;
await Expect.That(recording).Should().Trigger().WithCancellation(cancellationToken)
  .Because("it should be completed, before the cancellationToken is cancelled");
```

### Amount

You can specify a number of times, that a callback must at least be executed:

```csharp
await Expect.That(recording).Should().Trigger(3.Times());
```

You can also verify, that the callback was not executed at least the given number of times:

```csharp
await Expect.That(recording).Should().NotTrigger(3.Times());
```

### Parameters

You can also use callbacks with a single parameter:

```csharp
ICallbackRecording<string> recording = Record.Callback<string>();

recording.Trigger("foo");
recording.Trigger("bar");

await That(recording).Should().Trigger(2.Times());
```

*In case of a failed expectation, the recorded parameters will be displayed in the error message.*
