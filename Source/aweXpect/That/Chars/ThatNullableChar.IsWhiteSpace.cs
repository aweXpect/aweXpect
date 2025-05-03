using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableChar
{
	/// <summary>
	///     Verifies that the subject is white-space.
	/// </summary>
	/// <remarks>
	///     This means, that the specified Unicode character is categorized as white-space.<br />
	///     <seealso cref="char.IsWhiteSpace(char)" />
	/// </remarks>
	public static AndOrResult<char?, IThat<char?>> IsWhiteSpace(this IThat<char?> source)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsWhiteSpaceConstraint(it, grammars)),
			source);

	private sealed class IsWhiteSpaceConstraint(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithNotNullValue<char?>(it, grammars),
			IValueConstraint<char?>
	{
		public ConstraintResult IsMetBy(char? actual)
		{
			Actual = actual;
			Outcome = actual is not null && char.IsWhiteSpace(actual.Value) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is white-space");

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is not white-space");

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
}
