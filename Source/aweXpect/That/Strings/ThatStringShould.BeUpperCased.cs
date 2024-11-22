using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Formatting;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that all cased characters in the subject are upper-case.
	/// </summary>
	/// <remarks>
	///     That is, that the string could be the result of a call to <see cref="string.ToUpperInvariant()" />.
	/// </remarks>
	public static AndOrResult<string?, IThat<string?>> BeUpperCased(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeUpperCasedConstraint(it)),
			source);

	/// <summary>
	///     Verifies that of all cased characters in the subject at least one is lower-case.
	/// </summary>
	/// <remarks>
	///     That is, that the string could not be the result of a call to <see cref="string.ToUpperInvariant()" />.
	/// </remarks>
	public static AndOrResult<string, IThat<string?>> NotBeUpperCased(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeUpperCasedConstraint(it)),
			source);

	private readonly struct BeUpperCasedConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual != null && actual == actual.ToUpperInvariant())
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.SingleLine)}");
		}

		public override string ToString()
			=> "be upper-cased";
	}

	private readonly struct NotBeUpperCasedConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual == null || actual != actual.ToUpperInvariant())
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.SingleLine)}");
		}

		public override string ToString()
			=> "not be upper-cased";
	}
}
