using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the thrown exception has an inner exception which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> WithInnerException(
		Action<ThatExceptionShould<Exception?>> expectations)
		=> new(ExpectationBuilder
				.ForProperty<Exception, Exception?>(e => e.InnerException,
					"with an inner exception which should ")
				.AddExpectations(e => expectations(new ThatExceptionShould<Exception?>(e))),
			this);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <typeparamref name="TException" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> WithInnerException()
		=> new(ExpectationBuilder
				.AddConstraint(it =>
					new ThatExceptionShould.HasInnerExceptionValueConstraint<Exception>("with",
						it)),
			this);
}
