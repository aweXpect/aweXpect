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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsNullOrEmptyConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" /> or <see cref="string.Empty" />.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> IsNotNullOrEmpty(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsNotNullOrEmptyConstraint(it)),
			source);

	private readonly struct IsNullOrEmptyConstraint(string it) : IValueConstraint<string?>
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
			=> "is null or empty";
	}

	private readonly struct IsNotNullOrEmptyConstraint(string it) : IValueConstraint<string?>
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
			=> "is not null or empty";
	}
}
