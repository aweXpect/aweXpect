using System;
using System.Linq;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     A trigger result that also allows specifying parameter filters.
/// </summary>
public class TriggerParameterResult<T>(IThat<T> returnValue, string eventName)
	: TriggerResult<T>(returnValue, eventName)
{
	private TriggerEventFilter? _filter;

	/// <summary>
	///     Adds a parameter predicate on the first parameter of type <typeparamref name="TParameter" />.
	/// </summary>
	/// <remarks>
	///     The filter will exclude parameters where the type does not match any parameter.
	/// </remarks>
	public TriggerParameterResult<T> WithParameter<TParameter>(Func<TParameter, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		_filter ??= new TriggerEventFilter();
		_filter.AddPredicate<TParameter>(o => o.Any(x => x is TParameter m && predicate(m)), doNotPopulateThisValue);
		return this;
	}

	/// <summary>
	///     Adds a parameter predicate on the parameter at the given <paramref name="position" /> of type
	///     <typeparamref name="TParameter" />.
	/// </summary>
	/// <remarks>
	///     The filter will exclude parameters where the type does not match.
	/// </remarks>
	public TriggerParameterResult<T> WithParameter<TParameter>(int position, Func<TParameter, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		_filter ??= new TriggerEventFilter();
		_filter.AddPredicate<TParameter>(
			o => o.Length > position && o[position] is TParameter m && predicate(m),
			$"[{position}] {doNotPopulateThisValue}");
		return this;
	}

	/// <inheritdoc />
	protected override TriggerEventFilter? GetFilter() => _filter;
}
