using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect.Delegates;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the thrown exception has an inner exception which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> WithInnerException(
		Action<IThat<Exception?>> expectations)
		=> new(ExpectationBuilder
				.ForMember<Exception, Exception?>(e => e.InnerException,
					"with an inner exception which should ")
				.AddExpectations(e => expectations(new ThatSubject<Exception?>(e))),
			this);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <typeparamref name="TException" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> WithInnerException()
		=> new(ExpectationBuilder
				.AddConstraint(it =>
					new HasInnerExceptionValueConstraint<Exception>("with",
						it)),
			this);
}
