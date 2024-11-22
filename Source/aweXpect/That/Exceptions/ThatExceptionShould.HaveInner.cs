using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

/// <summary>
///     Expectations on <see cref="Exception" /> values.
/// </summary>
public partial class ThatExceptionShould<TException>
{
	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <typeparamref name="TInnerException" /> which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public AndOrResult<TException, ThatExceptionShould<TException>> HaveInner<
		TInnerException>(
		Action<ThatExceptionShould<TInnerException?>> expectations)
		where TInnerException : Exception?
		=> new(ExpectationBuilder
				.ForProperty<Exception, Exception?>(e => e.InnerException,
					$"have an inner {typeof(TInnerException).Name} which should ",
					replaceIt: false)
				.Validate(it
					=> new ThatExceptionShould.InnerExceptionIsTypeConstraint<TInnerException>(it))
				.AddExpectations(e => expectations(new ThatExceptionShould<TInnerException?>(e))),
			this);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <typeparamref name="TException" />.
	/// </summary>
	public AndOrResult<TException, ThatExceptionShould<TException>> HaveInner<
		TInnerException>()
		where TInnerException : Exception?
		=> new(ExpectationBuilder.AddConstraint(it =>
				new ThatExceptionShould.HasInnerExceptionValueConstraint<TInnerException>("have",
					it)),
			this);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <paramref name="innerExceptionType" /> which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public AndOrResult<TException, ThatExceptionShould<TException>> HaveInner(
		Type innerExceptionType,
		Action<ThatExceptionShould<Exception?>> expectations)
		=> new(ExpectationBuilder
				.ForProperty<Exception, Exception?>(e => e.InnerException,
					$"have an inner {innerExceptionType.Name} which should ")
				.Validate(it
					=> new ThatExceptionShould.InnerExceptionIsTypeConstraint(it,
						innerExceptionType))
				.AddExpectations(e => expectations(new ThatExceptionShould<Exception?>(e))),
			this);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <paramref name="innerExceptionType" />.
	/// </summary>
	public AndOrResult<TException, ThatExceptionShould<TException>> HaveInner(
		Type innerExceptionType)
		=> new(ExpectationBuilder.AddConstraint(it
				=> new ThatExceptionShould.HasInnerExceptionValueConstraint(innerExceptionType,
					"have", it)),
			this);
}
