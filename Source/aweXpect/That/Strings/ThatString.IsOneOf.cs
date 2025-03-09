using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> IsOneOf(
		this IThat<string?> source,
		params string?[] expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsOneOfConstraint(it, grammars, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> IsNotOneOf(
		this IThat<string?> source,
		params string?[] unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsOneOfConstraint(it, grammars, unexpected, options).Invert()),
			source,
			options);
	}

	private sealed class IsOneOfConstraint(
		string it,
		ExpectationGrammars grammars,
		string?[] expectedValues,
		StringEqualityOptions options)
		: ConstraintResult.WithNotNullValue<string?>(it, grammars),
			IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			Actual = actual;
			StringEqualityOptions stringEqualityOptions = options;
			Outcome = expectedValues.Any(expectedValue => stringEqualityOptions
				.AreConsideredEqual(actual, expectedValue))
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is one of ");
			Formatter.Format(stringBuilder, expectedValues);
			stringBuilder.Append(options);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not one of ");
			Formatter.Format(stringBuilder, expectedValues);
			stringBuilder.Append(options);
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}
	}
}
