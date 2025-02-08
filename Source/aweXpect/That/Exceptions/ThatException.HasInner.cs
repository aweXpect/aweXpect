using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatException
{
	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <typeparamref name="TInnerException" /> which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<Exception?, IThat<Exception?>> HasInner<TInnerException>(
		this IThat<Exception?> source,
		Action<IThat<TInnerException?>> expectations)
		where TInnerException : Exception?
		=> new(source.ThatIs().ExpectationBuilder
				.ForMember<Exception?, Exception?>(e => e?.InnerException,
					$"has an inner {typeof(TInnerException).Name} whose",
					false)
				.Validate(it
					=> new InnerExceptionIsTypeConstraint<TInnerException>(it))
				.AddExpectations(e => expectations(new ThatSubject<TInnerException?>(e)), ExpectationForm.Inner),
			source);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <typeparamref name="TInnerException" />.
	/// </summary>
	public static AndOrResult<Exception?, IThat<Exception?>> HasInner<TInnerException>(
		this IThat<Exception?> source)
		where TInnerException : Exception?
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new HasInnerExceptionValueConstraint<TInnerException>("has",
					it)),
			source);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <paramref name="innerExceptionType" /> which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<Exception?, IThat<Exception?>> HasInner(
		this IThat<Exception?> source,
		Type innerExceptionType,
		Action<IThat<Exception?>> expectations)
		=> new(source.ThatIs().ExpectationBuilder
				.ForMember<Exception?, Exception?>(e => e?.InnerException,
					$"has an inner {innerExceptionType.Name} whose")
				.Validate(it
					=> new InnerExceptionIsTypeConstraint(it,
						innerExceptionType))
				.AddExpectations(e => expectations(new ThatSubject<Exception?>(e)), ExpectationForm.Inner),
			source);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <paramref name="innerExceptionType" />.
	/// </summary>
	public static AndOrResult<Exception?, IThat<Exception?>> HaveInner(
		this IThat<Exception?> source,
		Type innerExceptionType)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form)
				=> new HasInnerExceptionValueConstraint(innerExceptionType,
					"has", it)),
			source);
}
