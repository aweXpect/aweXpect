﻿using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnum
{
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> IsOneOf<TEnum>(this IThat<TEnum> source,
		params TEnum?[] expected)
		where TEnum : struct, Enum
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint<TEnum>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> IsOneOf<TEnum>(this IThat<TEnum> source,
		IEnumerable<TEnum?> expected)
		where TEnum : struct, Enum
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint<TEnum>(it, grammars, expected)),
			source);
	
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> IsOneOf<TEnum>(this IThat<TEnum> source,
		IEnumerable<TEnum> expected)
		where TEnum : struct, Enum
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint<TEnum>(it, grammars, expected.Cast<TEnum?>())),
			source);

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> IsNotOneOf<TEnum>(this IThat<TEnum> source,
		params TEnum?[] unexpected)
		where TEnum : struct, Enum
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint<TEnum>(it, grammars, unexpected).Invert()),
			source);

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> IsNotOneOf<TEnum>(this IThat<TEnum> source,
		IEnumerable<TEnum?> unexpected)
		where TEnum : struct, Enum
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint<TEnum>(it, grammars, unexpected).Invert()),
			source);

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> IsNotOneOf<TEnum>(this IThat<TEnum> source,
		IEnumerable<TEnum> unexpected)
		where TEnum : struct, Enum
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint<TEnum>(it, grammars, unexpected.Cast<TEnum?>()).Invert()),
			source);

	private sealed class IsOneOfConstraint<TEnum>(string it, ExpectationGrammars grammars, IEnumerable<TEnum?> expected)
		: ConstraintResult.WithNotNullValue<TEnum>(it, grammars),
			IValueConstraint<TEnum>
		where TEnum : struct, Enum
	{
		public ConstraintResult IsMetBy(TEnum actual)
		{
			Actual = actual;
			Outcome = expected.Any(value => actual.Equals(value)) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is one of ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not one of ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
}
