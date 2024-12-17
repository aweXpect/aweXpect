using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public class TriggerPropertyChangedParameterResult<T>(IThat<T> returnValue, string eventName)
	: TriggerResult<T>(returnValue, eventName)
{
	private TriggerEventFilter? _filter;

	public TriggerResult<T> WithPropertyChangedEventArgs(Func<PropertyChangedEventArgs, bool> predicate,
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
