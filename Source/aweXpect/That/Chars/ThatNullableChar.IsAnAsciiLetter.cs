using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableChar
{
#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the subject is an ASCII letter.
	/// </summary>
	/// <remarks>
	///     <seealso cref="char.IsAsciiLetter(char)" />
	/// </remarks>
#else
	/// <summary>
	///     Verifies that the subject is an ASCII letter.
	/// </summary>
#endif
	public static AndOrResult<char?, IThat<char?>> IsAnAsciiLetter(this IThat<char?> source)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsAnAsciiLetterConstraint(it, grammars)),
			source);

	private sealed class IsAnAsciiLetterConstraint(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithNotNullValue<char?>(it, grammars),
			IValueConstraint<char?>
	{
		public ConstraintResult IsMetBy(char? actual)
		{
			Actual = actual;
#if NET8_0_OR_GREATER
			Outcome = actual is not null && char.IsAsciiLetter(actual.Value) ? Outcome.Success : Outcome.Failure;
#else
			Outcome = actual is >= 'a' and <= 'z' or >= 'A' and <= 'Z' ? Outcome.Success : Outcome.Failure;
#endif
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is an ASCII letter");

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is not an ASCII letter");

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
}
