using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Formatting;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<string?, IThat<string?>> BeNull(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeNullConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> NotBeNull(
		this IThat<string?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeNullConstraint(it)),
			source);

	private readonly struct BeNullConstraint(string it) : IValueConstraint<string?>
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
			=> "be null";
	}

	private readonly struct NotBeNullConstraint(string it) : IValueConstraint<string?>
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
			=> "not be null";
	}
}
