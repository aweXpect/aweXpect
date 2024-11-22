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
	///     Verifies that the actual exception has an inner exception.
	/// </summary>
	public AndOrResult<TException, ThatExceptionShould<TException>> HaveInnerException()
		=> new(ExpectationBuilder
				.AddConstraint(it =>
					new ThatExceptionShould.HasInnerExceptionValueConstraint<TException>("have",
						it)),
			this);

	/// <summary>
	///     Verifies that the actual exception has an inner exception which satisfies the <paramref name="expectations" />.
	/// </summary>
	public AndOrResult<TException?, ThatExceptionShould<TException>> HaveInnerException(
		Action<ThatExceptionShould<Exception?>> expectations)
		=> new(ExpectationBuilder
				.ForProperty<Exception, Exception?>(e => e.InnerException,
					"have an inner exception which should ",
					replaceIt: false)
				.Validate(it
					=> new ThatExceptionShould.InnerExceptionIsTypeConstraint<Exception>(it))
				.AddExpectations(e => expectations(new ThatExceptionShould<Exception?>(e))),
			this);
}
