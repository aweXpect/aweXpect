﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnum
{
	/// <summary>
	///     Verifies that the subject has the <paramref name="expectedFlag" /> value.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> HasFlag<TEnum>(
		this IThat<TEnum> source,
		TEnum? expectedFlag)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new HasFlagConstraint<TEnum>(it, grammars, expectedFlag)),
			source);

	/// <summary>
	///     Verifies that the subject does not have the <paramref name="unexpectedFlag" /> value.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> DoesNotHaveFlag<TEnum>(
		this IThat<TEnum> source,
		TEnum? unexpectedFlag)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new HasFlagConstraint<TEnum>(it, grammars, unexpectedFlag).Invert()),
			source);

	private sealed class HasFlagConstraint<TEnum>(string it, ExpectationGrammars grammars, TEnum? expectedFlag)
		: ConstraintResult.WithNotNullValue<TEnum>(it, grammars),
			IValueConstraint<TEnum>
		where TEnum : struct, Enum
	{
		public ConstraintResult IsMetBy(TEnum actual)
		{
			Actual = actual;
			Outcome = expectedFlag != null && actual.HasFlag(expectedFlag) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("has flag ");
			Formatter.Format(stringBuilder, expectedFlag);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("does not have flag ");
			Formatter.Format(stringBuilder, expectedFlag);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}
	}
}
