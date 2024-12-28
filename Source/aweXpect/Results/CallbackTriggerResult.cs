using System;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     A trigger result that also allows specifying parameter filters.
/// </summary>
public class CallbackTriggerResult<TCallbackResult>(
	ExpectationBuilder expectationBuilder,
	IThat<TCallbackResult> returnValue,
	TriggerCallbackOptions options)
	: AndOrResult<TCallbackResult, IThat<TCallbackResult>>(expectationBuilder, returnValue)
	where TCallbackResult : notnull
{
	/// <summary>
	///     Specifies a timeout for waiting on the callback.
	/// </summary>
	public CallbackTriggerResult<TCallbackResult> Within(TimeSpan timeout)
	{
		options.Timeout = timeout;
		return this;
	}
}
