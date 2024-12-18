using System;
using System.Linq;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     A trigger result that also allows specifying parameter filters.
/// </summary>
public class TriggerParameterResult<T>(
	ExpectationBuilder expectationBuilder,
	IExpectSubject<T> returnValue,
	string eventName,
	Quantifier quantifier)
	: TriggerResult<T, TriggerParameterResult<T>>(expectationBuilder, returnValue, eventName, quantifier)
{
	private TriggerEventFilter? _filter;

	/// <summary>
	///     Adds a predicate for the sender of the event.
	/// </summary>
	/// <remarks>
	///     The sender is expected to be the first parameter.
	/// </remarks>
	public TriggerParameterResult<T> WithSender(
		Func<object?, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		_filter ??= new TriggerEventFilter();
		_filter.AddPredicate<object?>(
			o => o.Length > 0 && predicate(o[0]),
			$" with sender {doNotPopulateThisValue}");
		return this;
	}

	/// <summary>
	///     Adds a predicate for the <see cref="EventArgs" /> of the event.
	/// </summary>
	/// <remarks>
	///     The event args are expected to be the second parameter.
	/// </remarks>
	public TriggerParameterResult<T> With<TEventArgs>(
		Func<TEventArgs, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
		where TEventArgs : EventArgs
	{
		_filter ??= new TriggerEventFilter();
		_filter.AddPredicate<TEventArgs>(
			o => o.Length > 1 && o[1] is TEventArgs m && predicate(m),
			$" with {Formatter.Format(typeof(TEventArgs))} {doNotPopulateThisValue}");
		return this;
	}

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
		_filter.AddPredicate<TParameter>(
			o => o.Any(x => x is TParameter m && predicate(m)),
			$" with {Formatter.Format(typeof(TParameter))} parameter {doNotPopulateThisValue}");
		return this;
	}

	/// <summary>
	///     Adds a parameter predicate on the parameter at the given zero-based <paramref name="position" /> of type
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
			$" with {Formatter.Format(typeof(TParameter))} parameter [{position}] {doNotPopulateThisValue}");
		return this;
	}

	/// <summary>
	///     Adds a parameter predicate on the parameter at the given zero-based <paramref name="position" /> of type
	///     <typeparamref name="TParameter" /> and the <paramref name="expression" /> for extension methods.
	/// </summary>
	/// <remarks>
	///     This method is mainly intended for extension methods, as it allows overriding the default
	///     <paramref name="expression" />.
	/// </remarks>
	public TriggerParameterResult<T> WithParameter<TParameter>(
		string expression,
		int? position,
		Func<TParameter, bool> predicate)
	{
		_filter ??= new TriggerEventFilter();
		_filter.AddPredicate<TParameter>(
			o => position == null
				? o.Any(x => x is TParameter m && predicate(m))
				: o.Length > position && o[position.Value] is TParameter m && predicate(m),
			expression);
		return this;
	}

	/// <inheritdoc />
	protected override TriggerEventFilter? GetFilter() => _filter;

	/// <inheritdoc />
	protected override void ResetFilter() =>
		_filter = null;
}
