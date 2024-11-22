using System;
using Testably.Expectations.Core;
using Testably.Expectations.Options;

namespace Testably.Expectations.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying a
///     tolerance.
/// </summary>
public class TimeToleranceResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	TimeTolerance options)
	: TimeToleranceResult<TType, TThat,
		TimeToleranceResult<TType, TThat>>(
		expectationBuilder,
		returnValue,
		options);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying a
///     tolerance.
/// </summary>
public class TimeToleranceResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	TimeTolerance options)
	: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue)
	where TSelf : TimeToleranceResult<TType, TThat, TSelf>
{
	private readonly ExpectationBuilder _expectationBuilder = expectationBuilder;

	/// <summary>
	///     Specifies a <paramref name="tolerance" /> to apply on the time comparison.
	/// </summary>
	public TimeToleranceResult<TType, TThat, TSelf> Within(TimeSpan tolerance)
	{
		options.SetTolerance(tolerance);
		return this;
	}
}
