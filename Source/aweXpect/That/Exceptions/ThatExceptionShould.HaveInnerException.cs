﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

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
				.ForMember<Exception, Exception?>(e => e.InnerException,
					"have an inner exception which should ",
					false)
				.Validate(it
					=> new ThatExceptionShould.InnerExceptionIsTypeConstraint<Exception>(it))
				.AddExpectations(e => expectations(new ThatExceptionShould<Exception?>(e))),
			this);
}