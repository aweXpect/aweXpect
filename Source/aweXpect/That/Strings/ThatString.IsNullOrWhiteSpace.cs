﻿using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />, <see cref="string.Empty" /> or consists only of white-space
	///     characters.
	/// </summary>
	public static AndOrResult<string?, IThat<string?>> IsNullOrWhiteSpace(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsNullOrWhiteSpaceConstraint(it, grammars)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />, <see cref="string.Empty" /> or consists only of
	///     white-space characters.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> IsNotNullOrWhiteSpace(
		this IThat<string?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsNullOrWhiteSpaceConstraint(it, grammars).Invert()),
			source);

	private class IsNullOrWhiteSpaceConstraint(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithValue<string?>(grammars),
			IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			Actual = actual;
			Outcome = string.IsNullOrWhiteSpace(actual) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is null or white-space");

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("is not null or white-space");

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}
	}
}
