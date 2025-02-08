using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<string?, IThat<string?>> IsNull(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new IsNullConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> IsNotNull(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new IsNotNullConstraint(it)),
			source);

	private readonly struct IsNullConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Success<string?>(null, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.SingleLine)}");
		}

		public override string ToString()
			=> "is null";
	}

	private readonly struct IsNotNullConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual is not null)
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was");
		}

		public override string ToString()
			=> "is not null";
	}
}
