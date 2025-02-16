using System;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Signaling;

namespace aweXpect.Results;

/// <summary>
///     A trigger result that also allows specifying the timeout.
/// </summary>
public class SignalCountResult(
	ExpectationBuilder expectationBuilder,
	IThat<Signaler> returnValue,
	SignalerOptions options)
	: AndOrResult<SignalerResult, IThat<Signaler>>(expectationBuilder, returnValue)
{
	/// <summary>
	///     Specifies a timeout for waiting on the callback.
	/// </summary>
	public SignalCountResult Within(TimeSpan timeout)
	{
		options.Timeout = timeout;
		return this;
	}
}

/// <summary>
///     A trigger result that also allows specifying the timeout.
/// </summary>
public class SignalCountResult<TParameter>(
	ExpectationBuilder expectationBuilder,
	IThat<Signaler<TParameter>> returnValue,
	SignalerOptions<TParameter> options)
	: SignalCountResult<TParameter, SignalCountResult<TParameter>>(expectationBuilder, returnValue, options);

/// <summary>
///     A trigger result that also allows specifying the timeout.
/// </summary>
public class SignalCountResult<TParameter, TSelf>(
	ExpectationBuilder expectationBuilder,
	IThat<Signaler<TParameter>> returnValue,
	SignalerOptions<TParameter> options)
	: AndOrResult<SignalerResult<TParameter>, IThat<Signaler<TParameter>>>(expectationBuilder, returnValue)
	where TSelf : SignalCountResult<TParameter, TSelf>
{
	/// <summary>
	///     Specifies a timeout for waiting on the callback.
	/// </summary>
	public TSelf Within(TimeSpan timeout)
	{
		options.Timeout = timeout;
		return (TSelf)this;
	}

	/// <summary>
	///     Specifies a predicate to filter for signals with a matching parameter.
	/// </summary>
	public TSelf With(
		Func<TParameter, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		options.WithPredicate(predicate, doNotPopulateThisValue);
		return (TSelf)this;
	}
}
