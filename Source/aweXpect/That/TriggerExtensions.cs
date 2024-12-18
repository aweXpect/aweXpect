using System.ComponentModel;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

/// <summary>
///     Extensions for common triggers.
/// </summary>
public static class TriggerExtensions
{
	/// <summary>
	///     Verifies that the subject triggers a <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
	/// </summary>
	public static TriggerPropertyChangedParameterResult<T> TriggersPropertyChanged<T>(this IExpectSubject<T> subject)
		where T : INotifyPropertyChanged
	{
		Quantifier quantifier = new();
		IThat<T> should = subject.Should(_ => { });
		return new TriggerPropertyChangedParameterResult<T>(should.ExpectationBuilder, subject, nameof(INotifyPropertyChanged.PropertyChanged), quantifier);
	}
}
