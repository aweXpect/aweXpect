﻿using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public partial class ThatException
{
	/// <summary>
	///     Verifies that the actual exception has an inner exception.
	/// </summary>
	public static AndOrResult<Exception?, IThat<Exception?>> HasInnerException(
		this IThat<Exception?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new HasInnerExceptionValueConstraint<Exception?>("has",
					it)),
			source);

	/// <summary>
	///     Verifies that the actual exception has an inner exception which satisfies the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<Exception?, IThat<Exception?>> HasInnerException(
		this IThat<Exception?> source,
		Action<IThat<Exception?>> expectations)
		=> new(source.ThatIs().ExpectationBuilder
				.ForMember<Exception, Exception?>(e => e.InnerException,
					"has an inner exception whose",
					false)
				.Validate(it
					=> new InnerExceptionIsTypeConstraint<Exception>(it))
				.AddExpectations(e => expectations(new ThatSubject<Exception?>(e)), ExpectationGrammars.Nested),
			source);
}
