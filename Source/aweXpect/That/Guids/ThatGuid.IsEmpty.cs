using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGuid
{
	/// <summary>
	///     Verifies that the subject is empty.
	/// </summary>
	public static AndOrResult<Guid, IThat<Guid>> IsEmpty(this IThat<Guid> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsEmptyConstraint(it, grammars)),
			source);

	/// <summary>
	///     Verifies that the subject is not empty.
	/// </summary>
	public static AndOrResult<Guid, IThat<Guid>> IsNotEmpty(this IThat<Guid> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsEmptyConstraint(it, grammars).Invert()),
			source);

	private sealed class IsEmptyConstraint(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithNotNullValue<Guid>(it, grammars),
			IValueConstraint<Guid>
	{
		public ConstraintResult IsMetBy(Guid actual)
		{
			Actual = actual;
			Outcome = actual == Guid.Empty ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is empty");

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is not empty");

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}
	}
}
