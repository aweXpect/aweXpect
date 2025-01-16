using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject ends with the <paramref name="expected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThatShould<string?>> EndWith(
		this IThatShould<string?> source,
		string expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThatShould<string?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new EndWithConstraint(it, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject does not end with the <paramref name="unexpected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThatShould<string?>> NotEndWith(
		this IThatShould<string?> source,
		string unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThatShould<string?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotEndWithConstraint(it, unexpected, options)),
			source,
			options);
	}

	private readonly struct EndWithConstraint(
		string it,
		string expected,
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

			if (expected.Length > actual.Length)
			{
				return new ConstraintResult.Failure<string?>(actual, ToString(),
					$"{it} had only length {actual.Length} which is shorter than the expected length of {expected.Length}");
			}

			if (options.AreConsideredEqual(
				    actual.Substring(actual.Length - expected.Length, expected.Length), expected))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure<string?>(actual, ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"end with {Formatter.Format(expected)}{options}";
	}

	private readonly struct NotEndWithConstraint(
		string it,
		string unexpected,
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

			if (unexpected.Length <= actual.Length &&
			    options.AreConsideredEqual(
				    actual.Substring(actual.Length - unexpected.Length, unexpected.Length),
				    unexpected))
			{
				return new ConstraintResult.Failure<string?>(actual, ToString(),
					$"{it} was {Formatter.Format(actual)}");
			}

			return new ConstraintResult.Success<string?>(actual, ToString());
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"not end with {Formatter.Format(unexpected)}{options}";
	}
}
