﻿using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject is equal to <paramref name="expected" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> IsEqualTo(
		this IThat<string?> source,
		string? expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((expectationBuilder, it, grammar) =>
				new IsEqualToConstraint(expectationBuilder, it, expected, options)),
			source,
			options);
	}

	private readonly struct IsEqualToConstraint(
		ExpectationBuilder expectationBuilder,
		string it,
		string? expected,
		StringEqualityOptions options)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (options.AreConsideredEqual(actual, expected))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			expectationBuilder.UpdateContexts(contexts => contexts
				.Add(new ResultContext("Actual", actual)));
			return new ConstraintResult.Failure<string?>(actual, ToString(),
					options.GetExtendedFailure(it, actual, expected));
		}

		/// <inheritdoc />
		public override string ToString()
			=> options.GetExpectation(expected, ExpectationGrammars.Active);
	}
}
