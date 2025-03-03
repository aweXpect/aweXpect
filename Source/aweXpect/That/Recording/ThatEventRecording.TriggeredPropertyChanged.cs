using System.ComponentModel;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Recording;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEventRecording
{
	/// <summary>
	///     Verifies that the subject has triggered the <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
	/// </summary>
	public static EventTriggerResult<TSubject> TriggeredPropertyChanged<TSubject>(
		this IThat<IEventRecording<TSubject>> source)
		where TSubject : INotifyPropertyChanged
	{
		Quantifier quantifier = new();
		TriggerEventFilter filter = new();
		return new EventTriggerResult<TSubject>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new HaveTriggeredConstraint<TSubject>(it, nameof(INotifyPropertyChanged.PropertyChanged), filter,
					quantifier)),
			source,
			filter,
			quantifier);
	}

	/// <summary>
	///     Verifies that the subject has not triggered the <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
	/// </summary>
	public static EventTriggerResult<TSubject> DidNotTriggerPropertyChanged<TSubject>(
		this IThat<IEventRecording<TSubject>> source)
		where TSubject : INotifyPropertyChanged
	{
		Quantifier quantifier = new();
		quantifier.Exactly(0);
		TriggerEventFilter filter = new();
		return new EventTriggerResult<TSubject>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new HaveTriggeredConstraint<TSubject>(it, nameof(INotifyPropertyChanged.PropertyChanged), filter,
					quantifier)),
			source,
			filter,
			quantifier);
	}
}
