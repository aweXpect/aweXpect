﻿using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that all cased characters in the subject are upper-case.
	/// </summary>
	/// <remarks>
	///     That is, that the string could be the result of a call to <see cref="string.ToUpperInvariant()" />.
	/// </remarks>
	public static AndOrResult<string?, IThat<string?>> IsUpperCased(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsUpperCasedConstraint(it, grammars)),
			source);

	/// <summary>
	///     Verifies that of all cased characters in the subject at least one is lower-case.
	/// </summary>
	/// <remarks>
	///     That is, that the string could not be the result of a call to <see cref="string.ToUpperInvariant()" />.
	/// </remarks>
	public static AndOrResult<string, IThat<string?>> IsNotUpperCased(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsUpperCasedConstraint(it, grammars).Invert()),
			source);

	private sealed class IsUpperCasedConstraint(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithNotNullValue<string?>(it, grammars),
			IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			Actual = actual;
			Outcome = actual != null && actual == actual.ToUpperInvariant() ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is upper-cased");

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.SingleLine);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is not upper-cased");

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.SingleLine);
		}
	}
}
