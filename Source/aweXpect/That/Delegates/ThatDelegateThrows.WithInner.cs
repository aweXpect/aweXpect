using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the thrown exception has an inner exception of type <typeparamref name="TInnerException" /> which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>>
		WithInner<TInnerException>(
			Action<IExpectSubject<TInnerException?>> expectations)
		where TInnerException : Exception
		=> new(ExpectationBuilder
				.ForMember<Exception, Exception?>(e => e.InnerException,
					$"with an inner {typeof(TInnerException).Name} which should ")
				.Validate(it
					=> new ThatException.InnerExceptionIsTypeConstraint<TInnerException>(it))
				.AddExpectations(e => expectations(new That.Subject<TInnerException?>(e))),
			this);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <typeparamref name="TException" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> WithInner<
		TInnerException>()
		where TInnerException : Exception?
		=> new(ExpectationBuilder
				.AddConstraint(it =>
					new ThatException.HasInnerExceptionValueConstraint<TInnerException>(
						"with", it)),
			this);

	/// <summary>
	///     Verifies that the thrown exception has an inner exception of type <paramref name="innerExceptionType" /> which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> WithInner(
		Type innerExceptionType,
		Action<IExpectSubject<Exception?>> expectations)
		=> new(ExpectationBuilder
				.ForMember<Exception, Exception?>(e => e.InnerException,
					$"with an inner {innerExceptionType.Name} which should ")
				.Validate(it
					=> new ThatException.InnerExceptionIsTypeConstraint(it,
						innerExceptionType))
				.AddExpectations(e => expectations(new That.Subject<Exception?>(e))),
			this);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <paramref name="innerExceptionType" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> WithInner(
		Type innerExceptionType)
		=> new(ExpectationBuilder
				.AddConstraint(it =>
					new ThatException.HasInnerExceptionValueConstraint(innerExceptionType,
						"with", it)),
			this);
}
