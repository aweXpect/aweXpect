using System;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     A trigger result that also allows specifying the timeout.
/// </summary>
public class CallbackTriggerResult<TCallback, TCallbackResult>(
	ExpectationBuilder expectationBuilder,
	IThat<TCallback> returnValue,
	TriggerCallbackOptions options)
	: AndOrResult<TCallbackResult, IThat<TCallback>>(expectationBuilder, returnValue)
{
	/// <summary>
	///     Specifies a timeout for waiting on the callback.
	/// </summary>
	public CallbackTriggerResult<TCallback, TCallbackResult> Within(TimeSpan timeout)
	{
		options.Timeout = timeout;
		return this;
	}
}
