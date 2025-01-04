using System.ComponentModel;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Recording;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEventRecordingShould
{
	/// <summary>
	///     Verifies that the subject has triggered the <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
	/// </summary>
	public static EventTriggerResult<TSubject> HaveTriggeredPropertyChanged<TSubject>(
		this IThat<IEventRecording<TSubject>> source)
		where TSubject : INotifyPropertyChanged
	{
		Quantifier quantifier = new();
		TriggerEventFilter filter = new();
		return new EventTriggerResult<TSubject>(
			source.ExpectationBuilder.AddConstraint(it
				=> new HaveTriggeredConstraint<TSubject>(it, nameof(INotifyPropertyChanged.PropertyChanged), filter,
					quantifier)),
			source,
			filter,
			quantifier);
	}

	/// <summary>
	///     Verifies that the subject has not triggered the <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
	/// </summary>
	public static EventTriggerResult<TSubject> NotHaveTriggeredPropertyChanged<TSubject>(
		this IThat<IEventRecording<TSubject>> source)
		where TSubject : INotifyPropertyChanged
	{
		Quantifier quantifier = new();
		quantifier.Exactly(0);
		TriggerEventFilter filter = new();
		return new EventTriggerResult<TSubject>(
			source.ExpectationBuilder.AddConstraint(it
				=> new HaveTriggeredConstraint<TSubject>(it, nameof(INotifyPropertyChanged.PropertyChanged), filter,
					quantifier)),
			source,
			filter,
			quantifier);
	}
}
