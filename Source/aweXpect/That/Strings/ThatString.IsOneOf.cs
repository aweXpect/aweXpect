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
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new IsOneOfConstraint(it, expected, options)),
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
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new IsNotOneOfConstraint(it, unexpected, options)),
			source,
			options);
	}

	private readonly struct IsOneOfConstraint(
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
			=> $"is one of {Formatter.Format(expectedValues)}{options}";
	}

	private readonly struct IsNotOneOfConstraint(
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
			=> $"is not one of {Formatter.Format(unexpectedValues)}{options}";
	}
}
