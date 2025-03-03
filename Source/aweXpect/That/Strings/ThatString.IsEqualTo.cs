using aweXpect.Core;
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
			source.ThatIs().ExpectationBuilder.AddConstraint((expectationBuilder, it, grammars) =>
				new IsEqualToConstraint(expectationBuilder, it, grammars, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to <paramref name="unexpected" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> IsNotEqualTo(
		this IThat<string?> source,
		string? unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((expectationBuilder, it, grammars) =>
				new IsNotEqualToConstraint(expectationBuilder, it, grammars, unexpected, options)),
			source,
			options);
	}

	private readonly struct IsEqualToConstraint(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		string? expected,
		StringEqualityOptions options)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (options.AreConsideredEqual(actual, expected))
			{
				StringEqualityOptions? o = options;
				string? i = it;
				ExpectationGrammars g = grammars;
				string? e = expected;
				return new ConstraintResult.Success<string?>(actual, ToString(),
					() => o.GetExtendedFailure(i, g, actual, e));
			}

			expectationBuilder.UpdateContexts(contexts => contexts
				.Add(new ResultContext("Actual", actual)));
			return new ConstraintResult.Failure<string?>(actual, ToString(),
				options.GetExtendedFailure(it, grammars, actual, expected));
		}

		/// <inheritdoc />
		public override string ToString()
			=> options.GetExpectation(expected, grammars | ExpectationGrammars.Active);
	}

	private readonly struct IsNotEqualToConstraint(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		string? unexpected,
		StringEqualityOptions options)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (!options.AreConsideredEqual(actual, unexpected))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			expectationBuilder.UpdateContexts(contexts => contexts
				.Add(new ResultContext("Actual", actual)));
			return new ConstraintResult.Failure<string?>(actual, ToString(),
				$"{it} did match");
		}

		/// <inheritdoc />
		public override string ToString()
			=> options.GetExpectation(unexpected, grammars.Negate() | ExpectationGrammars.Active);
	}
}
