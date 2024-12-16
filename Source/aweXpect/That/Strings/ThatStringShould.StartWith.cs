using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject does not start with the <paramref name="unexpected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> NotStartWith(
		this IThat<string?> source,
		string unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotStartWithConstraint(it, unexpected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject starts with the <paramref name="expected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> StartWith(
		this IThat<string?> source,
		string expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new StartWithConstraint(it, expected, options)),
			source,
			options);
	}

	private readonly struct NotStartWithConstraint(
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
			    options.AreConsideredEqual(actual[..unexpected.Length], unexpected))
			{
				return new ConstraintResult.Failure<string?>(actual, ToString(),
					$"{it} was {Formatter.Format(actual)}");
			}

			return new ConstraintResult.Success<string?>(actual, ToString());
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"not start with {Formatter.Format(unexpected)}{options}";
	}

	private readonly struct StartWithConstraint(
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

			if (options.AreConsideredEqual(actual[..expected.Length], expected))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure<string?>(actual, ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"start with {Formatter.Format(expected)}{options}";
	}
}
