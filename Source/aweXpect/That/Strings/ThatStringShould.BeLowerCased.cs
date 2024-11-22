using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Formatting;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that all cased characters in the subject are lower-case.
	/// </summary>
	/// <remarks>
	///     That is, that the string could be the result of a call to <see cref="string.ToLowerInvariant()" />.
	/// </remarks>
	public static AndOrResult<string?, IThat<string?>> BeLowerCased(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeLowerCasedConstraint(it)),
			source);

	/// <summary>
	///     Verifies that of all cased characters in the subject at least one is upper-case.
	/// </summary>
	/// <remarks>
	///     That is, that the string could not be the result of a call to <see cref="string.ToLowerInvariant()" />.
	/// </remarks>
	public static AndOrResult<string, IThat<string?>> NotBeLowerCased(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeLowerCasedConstraint(it)),
			source);

	private readonly struct BeLowerCasedConstraint(string it) : IValueConstraint<string?>
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
			=> "be lower-cased";
	}

	private readonly struct NotBeLowerCasedConstraint(string it) : IValueConstraint<string?>
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
			=> "not be lower-cased";
	}
}
