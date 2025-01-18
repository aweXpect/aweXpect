using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" /> or <see cref="string.Empty" />.
	/// </summary>
	public static AndOrResult<string?, IThat<string?>> IsNullOrEmpty(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeNullOrEmptyConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" /> or <see cref="string.Empty" />.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> IsNotNullOrEmpty(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NotBeNullOrEmptyConstraint(it)),
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
