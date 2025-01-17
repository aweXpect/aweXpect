﻿using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="Exception" /> values.
/// </summary>
public static partial class ThatException
{
	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <typeparamref name="TInnerException" /> which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<Exception?, IExpectSubject<Exception?>> HasInner<TInnerException>(
		this IExpectSubject<Exception?> source,
		Action<IExpectSubject<TInnerException?>> expectations)
		where TInnerException : Exception?
		=> new(source.ThatIs().ExpectationBuilder
				.ForMember<Exception?, Exception?>(e => e?.InnerException,
					$"have an inner {typeof(TInnerException).Name} which should ",
					false)
				.Validate(it
					=> new InnerExceptionIsTypeConstraint<TInnerException>(it))
				.AddExpectations(e => expectations(new That.Subject<TInnerException?>(e))),
			source);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <typeparamref name="TInnerException" />.
	/// </summary>
	public static AndOrResult<Exception?, IExpectSubject<Exception?>> HasInner<TInnerException>(
		this IExpectSubject<Exception?> source)
		where TInnerException : Exception?
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new HasInnerExceptionValueConstraint<TInnerException>("have",
					it)),
			source);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <paramref name="innerExceptionType" /> which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<Exception?, IExpectSubject<Exception?>> HasInner(
		this IExpectSubject<Exception?> source,
		Type innerExceptionType,
		Action<IExpectSubject<Exception?>> expectations)
		=> new(source.ThatIs().ExpectationBuilder
				.ForMember<Exception?, Exception?>(e => e?.InnerException,
					$"have an inner {innerExceptionType.Name} which should ")
				.Validate(it
					=> new InnerExceptionIsTypeConstraint(it,
						innerExceptionType))
				.AddExpectations(e => expectations(new That.Subject<Exception?>(e))),
			source);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <paramref name="innerExceptionType" />.
	/// </summary>
	public static AndOrResult<Exception?, IExpectSubject<Exception?>> HaveInner(
		this IExpectSubject<Exception?> source,
		Type innerExceptionType)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new HasInnerExceptionValueConstraint(innerExceptionType,
					"have", it)),
			source);
}