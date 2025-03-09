using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGuid
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<Guid, IThat<Guid>> IsEqualTo(this IThat<Guid> source,
		Guid? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<Guid, IThat<Guid>> IsNotEqualTo(this IThat<Guid> source,
		Guid? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint(it, grammars, unexpected).Invert()),
			source);

	private sealed class IsEqualToConstraint(string it, ExpectationGrammars grammars, Guid? expected)
		: ConstraintResult.WithEqualToValue<Guid>(it, grammars, expected is null),
			IValueConstraint<Guid>
	{
		public ConstraintResult IsMetBy(Guid actual)
		{
			Actual = actual;
			Outcome = actual.Equals(expected) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not ");
			Formatter.Format(stringBuilder, expected);
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}
	}
}
