using System;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Recording;

namespace aweXpect.Results;

/// <summary>
///     A trigger result that also allows specifying the timeout.
/// </summary>
public class SignalCountResult(
	ExpectationBuilder expectationBuilder,
	IThat<SignalCounter> returnValue,
	SignalCounterOptions options)
	: AndOrResult<SignalCounterResult, IThat<SignalCounter>>(expectationBuilder, returnValue)
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
	IThat<SignalCounter<TParameter>> returnValue,
	SignalCounterOptions<TParameter> options)
	: AndOrResult<SignalCounterResult<TParameter>, IThat<SignalCounter<TParameter>>>(expectationBuilder, returnValue)
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
	///     Specifies a timeout for waiting on the callback.
	/// </summary>
	public SignalCountResult<TParameter> With(
		Func<TParameter, bool> predicate,
		[CallerArgumentExpression("predicate")] string doNotPopulateThisValue = "")
	{
		options.WithPredicate(predicate, doNotPopulateThisValue);
		return this;
	}
}
