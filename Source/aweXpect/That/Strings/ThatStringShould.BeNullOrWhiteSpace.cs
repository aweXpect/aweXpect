using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Formatting;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />, <see cref="string.Empty" /> or consists only of white-space
	///     characters.
	/// </summary>
	public static AndOrResult<string?, IThat<string?>> BeNullOrWhiteSpace(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeNullOrWhiteSpaceConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />, <see cref="string.Empty" /> or consists only of
	///     white-space characters.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> NotBeNullOrWhiteSpace(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeNullOrWhiteSpaceConstraint(it)),
			source);

	private readonly struct BeNullOrWhiteSpaceConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (string.IsNullOrWhiteSpace(actual))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.SingleLine)}");
		}

		public override string ToString()
			=> "be null or white-space";
	}

	private readonly struct NotBeNullOrWhiteSpaceConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (!string.IsNullOrWhiteSpace(actual))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.SingleLine)}");
		}

		public override string ToString()
			=> "not be null or white-space";
	}
}
