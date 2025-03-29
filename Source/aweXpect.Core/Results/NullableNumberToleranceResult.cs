using aweXpect.Core;
using aweXpect.Options;
#if NET8_0_OR_GREATER
using System.Numerics;
#else
using System;
#endif

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
#if NET8_0_OR_GREATER
	where TType : struct, INumber<TType>;
#else
	where TType : struct, IComparable<TType>;
#endif

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
#if NET8_0_OR_GREATER
	where TType : struct, INumber<TType>
#else
	where TType : struct, IComparable<TType>
#endif
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
