---
sidebar_position: 15
---

# Events

Describes the possible expectations for verifying events.


## Triggering

You can verify, that an object triggers an event:
```csharp
class ThresholdReachedEventArgs(int threshold = 0) : EventArgs
{
    public int Threshold { get; } = threshold;
}
class MyClass
{
  public event EventHandler? ThresholdReached;
  public void OnThresholdReached(ThresholdReachedEventArgs e)
    => ThresholdReached?.Invoke(this, e);
}
MyClass sut = new MyClass();

await Expect.That(sut)
  .Triggers(nameof(MyClass.ThresholdReached))
  .While(subject => subject.OnThresholdReached(new ThresholdReachedEventArgs()));
```

This will register a recording of all events named "ThresholdReached" that are triggered during the execution of the callback.

The callback in `While()` can also be asynchronous. In this case, you can also get the cancellation token as second parameter:
```csharp
await Expect.That(sut)
  .Triggers(nameof(MyClass.ThresholdReached))
  .While(subject => subject.OnThresholdReachedAsync(new ThresholdReachedEventArgs()))
  .Because("we support asynchronous callbacks");

await Expect.That(sut)
  .Triggers(nameof(MyClass.ThresholdReached))
  .While((subject, token) => subject.OnThresholdReachedAsync(new ThresholdReachedEventArgs(), token))
  .Because("we also support cancellation");
```


## Filtering

You can filter triggered events based on their parameters.
```csharp
await Expect.That(sut)
  .Triggers(nameof(MyClass.ThresholdReached))
  .WithParameter<ThresholdReachedEventArgs>(e => e.Threshold > 10)
  .While(subject =>
  {
    subject.OnThresholdReached(new ThresholdReachedEventArgs(5));
    subject.OnThresholdReached(new ThresholdReachedEventArgs(15));
  });
```

## Counting

You can verify, that an event was triggered a specific number of times
```csharp
await Expect.That(sut)
  .Triggers(nameof(MyClass.ThresholdReached))
  .While(subject =>
  {
    subject.OnThresholdReached(new ThresholdReachedEventArgs(5));
    subject.OnThresholdReached(new ThresholdReachedEventArgs(15));
  })
  .Between(1).And(2.Times());
```
You can use the same occurrence constraints as in the [contain](/docs/expectations/collections#contain) method:
- `AtLeast(2.Times())`
- `AtMost(3.Times())`
- `Between(1).And(4.Times())`
- `Exactly(0.Times())`


## Special events

For common events, you can create specific overloads.  
Included is an overload for the [`INotifyPropertyChanged.PropertyChanged`](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged.propertychanged) event:
```csharp
MyClass sut = // ...implements INotifyPropertyChanged

await Expect.That(sut)
  .TriggersPropertyChanged()
  .WithPropertyChangedEventArgs(e => e.PropertyName == "MyProperty")
  .While(subject => subject.Execute())
  .AtLeast(2.Times());
```
