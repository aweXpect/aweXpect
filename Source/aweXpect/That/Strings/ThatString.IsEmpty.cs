using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject is empty.
	/// </summary>
	public static AndOrResult<string?, IThat<string?>> IsEmpty(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new IsEmptyConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not empty.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> IsNotEmpty(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new IsNotEmptyConstraint(it)),
			source);

	private readonly struct IsEmptyConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual == string.Empty)
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.SingleLine)}");
		}

		public override string ToString()
			=> "is empty";
	}

	private readonly struct IsNotEmptyConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual != string.Empty)
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was");
		}

		public override string ToString()
			=> "is not empty";
	}
}
