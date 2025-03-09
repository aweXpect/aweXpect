using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatTimeSpan
{
	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<TimeSpan, IThat<TimeSpan>> IsNegative(this IThat<TimeSpan> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsNegativeConstraint(it, grammars)),
			source);

	/// <summary>
	///     Verifies that the subject is not negative.
	/// </summary>
	public static AndOrResult<TimeSpan, IThat<TimeSpan>> IsNotNegative(this IThat<TimeSpan> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsNegativeConstraint(it, grammars).Invert()),
			source);

	private sealed class IsNegativeConstraint(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithNotNullValue<TimeSpan>(it, grammars),
			IValueConstraint<TimeSpan>
	{
		public ConstraintResult IsMetBy(TimeSpan actual)
		{
			Actual = actual;
			Outcome = actual < TimeSpan.Zero ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is negative");

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is not negative");

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}
	}
}
