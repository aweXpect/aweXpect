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
	IThatShould<Signaler> returnValue,
	SignalerOptions options)
	: AndOrResult<SignalerResult, IThatShould<Signaler>>(expectationBuilder, returnValue)
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
	IThatShould<Signaler<TParameter>> returnValue,
	SignalerOptions<TParameter> options)
	: AndOrResult<SignalerResult<TParameter>, IThatShould<Signaler<TParameter>>>(expectationBuilder, returnValue)
{
	/// <summary>
	///     Specifies a timeout for waiting on the callback.
	/// </summary>
	public SignalCountResult<TParameter> Within(TimeSpan timeout)
	{
		options.Timeout = timeout;
		return this;
	}

	/// <summary>
	///     Specifies a predicate to filter for signals with a matching parameter.
	/// </summary>
	public SignalCountResult<TParameter> With(
		Func<TParameter, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		options.WithPredicate(predicate, doNotPopulateThisValue);
		return this;
	}
}
