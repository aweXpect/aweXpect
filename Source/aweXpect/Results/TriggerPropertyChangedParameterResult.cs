using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     Result for a <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
/// </summary>
public class TriggerPropertyChangedParameterResult<T>(ExpectationBuilder expectationBuilder, IExpectSubject<T> returnValue, string eventName, Quantifier quantifier)
	: TriggerParameterResult<T>(expectationBuilder, returnValue, eventName, quantifier)
{
	private TriggerEventFilter? _filter;

	/// <summary>
	///     Predicate to filter for the <see cref="PropertyChangedEventArgs" /> parameter.
	/// </summary>
	public TriggerParameterResult<T> WithPropertyChangedEventArgs(Func<PropertyChangedEventArgs, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		_filter ??= new TriggerEventFilter();
		_filter.AddPredicate<PropertyChangedEventArgs>(o => o.Any(x => x is PropertyChangedEventArgs m && predicate(m)),
			doNotPopulateThisValue);
		return this;
	}

	/// <inheritdoc />
	protected override TriggerEventFilter? GetFilter() => _filter;
}
