using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThatShould<string?>> BeOneOf(
		this IThatShould<string?> source,
		params string?[] expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThatShould<string?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeOneOfConstraint(it, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThatShould<string?>> NotBeOneOf(
		this IThatShould<string?> source,
		params string?[] unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThatShould<string?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotBeOneOfConstraint(it, unexpected, options)),
			source,
			options);
	}

	private readonly struct BeOneOfConstraint(
		string it,
		string?[] expectedValues,
		StringEqualityOptions options)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<string?>(null, ToString(),
					$"{it} was <null>");
			}

			StringEqualityOptions stringEqualityOptions = options;
			if (expectedValues.Any(expectedValue => stringEqualityOptions.AreConsideredEqual(actual, expectedValue)))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure<string?>(actual, ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"be one of {Formatter.Format(expectedValues)}{options}";
	}

	private readonly struct NotBeOneOfConstraint(
		string it,
		string?[] unexpectedValues,
		StringEqualityOptions options)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<string?>(null, ToString(),
					$"{it} was <null>");
			}

			StringEqualityOptions stringEqualityOptions = options;
			if (unexpectedValues.Any(unexpectedValue
				    => stringEqualityOptions.AreConsideredEqual(actual, unexpectedValue)))
			{
				return new ConstraintResult.Failure<string?>(actual, ToString(),
					$"{it} was {Formatter.Format(actual)}");
			}

			return new ConstraintResult.Success<string?>(actual, ToString());
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"not be one of {Formatter.Format(unexpectedValues)}{options}";
	}
}
