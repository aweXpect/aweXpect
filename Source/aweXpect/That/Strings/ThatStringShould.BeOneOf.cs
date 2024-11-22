using System.Linq;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static StringEqualityResult<string?, IThat<string?>> BeOneOf(
		this IThat<string?> source,
		params string?[] expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<string?, IThat<string?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeOneOfConstraint(it, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static StringEqualityResult<string?, IThat<string?>> NotBeOneOf(
		this IThat<string?> source,
		params string?[] unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<string?, IThat<string?>>(
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
			if (expectedValues.Any(expectedValue => stringEqualityOptions.Comparer
				.Equals(actual, expectedValue)))
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
			if (unexpectedValues.Any(unexpectedValue => stringEqualityOptions.Comparer
				.Equals(actual, unexpectedValue)))
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
