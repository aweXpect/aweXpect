﻿using System;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />?.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying a
///     tolerance.
/// </summary>
public class NullableNumberToleranceResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	NumberTolerance<TType> options)
	: NullableNumberToleranceResult<TType, TThat,
		NullableNumberToleranceResult<TType, TThat>>(
		expectationBuilder,
		returnValue,
		options)
	where TType : struct, IComparable<TType>;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />?.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying a
///     tolerance.
/// </summary>
public class NullableNumberToleranceResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	NumberTolerance<TType> options)
	: AndOrResult<TType?, TThat, TSelf>(expectationBuilder, returnValue)
	where TSelf : NullableNumberToleranceResult<TType, TThat, TSelf>
	where TType : struct, IComparable<TType>
{
	/// <summary>
	///     Specifies a <paramref name="tolerance" /> to apply on the number comparison.
	/// </summary>
	public TSelf Within(TType tolerance)
	{
		options.SetTolerance(tolerance);
		return (TSelf)this;
	}
}
