using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that all cased characters in the subject are lower-case.
	/// </summary>
	/// <remarks>
	///     That is, that the string could be the result of a call to <see cref="string.ToLowerInvariant()" />.
	/// </remarks>
	public static AndOrResult<string?, IThat<string?>> IsLowerCased(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLowerCasedConstraint(it)),
			source);

	/// <summary>
	///     Verifies that of all cased characters in the subject at least one is upper-case.
	/// </summary>
	/// <remarks>
	///     That is, that the string could not be the result of a call to <see cref="string.ToLowerInvariant()" />.
	/// </remarks>
	public static AndOrResult<string, IThat<string?>> IsNotLowerCased(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsNotLowerCasedConstraint(it)),
			source);

	private readonly struct IsLowerCasedConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual != null && actual == actual.ToLowerInvariant())
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.SingleLine)}");
		}

		public override string ToString()
			=> "is lower-cased";
	}

	private readonly struct IsNotLowerCasedConstraint(string it) : IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual == null || actual != actual.ToLowerInvariant())
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.SingleLine)}");
		}

		public override string ToString()
			=> "is not lower-cased";
	}
}
