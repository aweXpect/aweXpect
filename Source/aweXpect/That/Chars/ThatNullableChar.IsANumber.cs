using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableChar
{
	/// <summary>
	///     Verifies that the subject is a number.
	/// </summary>
	/// <remarks>
	///     This means, that the specified Unicode character is categorized as a number.<br />
	///     <seealso cref="char.IsNumber(char)" />
	/// </remarks>
	public static AndOrResult<char?, IThat<char?>> IsANumber(this IThat<char?> source)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsANumberConstraint(it, grammars)),
			source);

	private sealed class IsANumberConstraint(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithNotNullValue<char?>(it, grammars),
			IValueConstraint<char?>
	{
		public ConstraintResult IsMetBy(char? actual)
		{
			Actual = actual;
			Outcome = actual is not null && char.IsNumber(actual.Value) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is a number");

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is not a number");

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
}
