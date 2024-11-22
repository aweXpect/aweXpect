using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Formatting;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" /> or <see cref="string.Empty" />.
	/// </summary>
	public static AndOrResult<string?, IThat<string?>> BeNullOrEmpty(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeNullOrEmptyConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" /> or <see cref="string.Empty" />.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> NotBeNullOrEmpty(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeNullOrEmptyConstraint(it)),
			source);

	private readonly struct BeNullOrEmptyConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (string.IsNullOrEmpty(actual))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.SingleLine)}");
		}

		public override string ToString()
			=> "be null or empty";
	}

	private readonly struct NotBeNullOrEmptyConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (!string.IsNullOrEmpty(actual))
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.SingleLine)}");
		}

		public override string ToString()
			=> "not be null or empty";
	}
}
