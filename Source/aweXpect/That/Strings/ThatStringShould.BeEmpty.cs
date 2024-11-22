using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Formatting;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject is empty.
	/// </summary>
	public static AndOrResult<string?, IThat<string?>> BeEmpty(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeEmptyConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not empty.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> NotBeEmpty(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeEmptyConstraint(it)),
			source);

	private readonly struct BeEmptyConstraint(string it) : IValueConstraint<string?>
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
			=> "be empty";
	}

	private readonly struct NotBeEmptyConstraint(string it) : IValueConstraint<string?>
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
			=> "not be empty";
	}
}
