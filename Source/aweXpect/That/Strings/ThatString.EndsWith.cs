using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject ends with the <paramref name="expected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> EndsWith(
		this IThat<string?> source,
		string expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new EndsWithConstraint(it, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject does not end with the <paramref name="unexpected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> DoesNotEndWith(
		this IThat<string?> source,
		string unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new DoesNotEndWithConstraint(it, unexpected, options)),
			source,
			options);
	}

	private readonly struct EndsWithConstraint(
		string it,
		string? expected,
		StringEqualityOptions options)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (expected is null)
			{
				return new ConstraintResult.Failure<string?>(null, ToString(),
					$"{Formatter.Format(actual)} cannot be validated against <null>");
			}

			if (actual is null)
			{
				return new ConstraintResult.Failure<string?>(null, ToString(),
					$"{it} was <null>");
			}

			if (expected.Length > actual.Length)
			{
				return new ConstraintResult.Failure<string?>(actual, ToString(),
					$"{it} was {Formatter.Format(actual)} and with length {actual.Length} is shorter than the expected length of {expected.Length}");
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
			=> $"ends with {Formatter.Format(expected)}{options}";
	}

	private readonly struct DoesNotEndWithConstraint(
		string it,
		string? unexpected,
		StringEqualityOptions options)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (unexpected is null)
			{
				return new ConstraintResult.Failure<string?>(null, ToString(),
					$"{Formatter.Format(actual)} cannot be validated against <null>");
			}

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
			=> $"does not end with {Formatter.Format(unexpected)}{options}";
	}
}
