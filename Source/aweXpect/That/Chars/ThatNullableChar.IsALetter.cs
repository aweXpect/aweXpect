using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableChar
{
	/// <summary>
	///     Verifies that the subject is a letter.
	/// </summary>
	/// <remarks>
	///     This means, that the specified Unicode character is categorized as a Unicode letter.<br />
	///     <seealso cref="char.IsLetter(char)" />
	/// </remarks>
	public static AndOrResult<char?, IThat<char?>> IsALetter(this IThat<char?> source)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsALetterConstraint(it, grammars)),
			source);

	private sealed class IsALetterConstraint(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithNotNullValue<char?>(it, grammars),
			IValueConstraint<char?>
	{
		public ConstraintResult IsMetBy(char? actual)
		{
			Actual = actual;
			Outcome = actual is not null && char.IsLetter(actual.Value) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is a letter");

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is not a letter");

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
}
