---
sidebar_position: 1
---

# Cancellation

You can add cancellation support on the expectations, so that they can't run indefinitely:

## Global settings

### Timeout
You can set a global timeout that is applied for all expectations:
```csharp
// Sets a global timeout of 10 seconds
Customize.aweXpect.Settings().TestCancellation
    .Set(TestCancellation.FromTimeout(TimeSpan.FromSeconds(10)));
// Uses the CancellationToken from the test context
Customize.aweXpect.Settings().TestCancellation.Set(() => TestContext.Current.CancellationToken);
```

### `CancellationToken`

You can set a global provider for getting a `CancellationToken` that is applied for all expectations:
```csharp
// Uses the CancellationToken from the test context
Customize.aweXpect.Settings().TestCancellation
    .Set(() => TestContext.Current.CancellationToken);
```

Note: Like all customization options, the setter returns an `IDisposable` that will remove the cancellation on `Dispose()`.


## Local settings

You can overwrite or apply the `CancellationToken` also on individual expectations, using the `WithCancellation(CancellationToken)` method, e.g. 
```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
IAsyncEnumerable<int> myEnumerable = // ...
await Expect.That(myEnumerable).All().AreEqualTo(1)
    .WithCancellation(cts.Token);
```

Note: A local `CancellationToken` will replace the global one and not be applied additionally.
If necessary, provide a [linked cancellation token](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.createlinkedtokensource) yourself!
