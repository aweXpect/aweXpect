﻿using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject ends with the <paramref name="expected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> EndsWith(
		this IThat<string?> source,
		string expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new EndsWithConstraint(it, grammars, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject does not end with the <paramref name="unexpected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> DoesNotEndWith(
		this IThat<string?> source,
		string unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new EndsWithConstraint(it, grammars, unexpected, options).Invert()),
			source,
			options);
	}

	private sealed class EndsWithConstraint(
		string it,
		ExpectationGrammars grammars,
		string? expected,
		StringEqualityOptions options)
		: ConstraintResult.WithNotNullValue<string?>(it, grammars),
			IValueConstraint<string?>
	{
		public ConstraintResult IsMetBy(string? actual)
		{
			Actual = actual;
			if (expected is null)
			{
				Outcome = IsNegated ? Outcome.Success : Outcome.Failure;
				return this;
			}

			Outcome = expected.Length <= actual?.Length && options.AreConsideredEqual(
				actual.Substring(actual.Length - expected.Length, expected.Length), expected)
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("ends with ");
			Formatter.Format(stringBuilder, expected);
			stringBuilder.Append(options);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (expected is null)
			{
				Formatter.Format(stringBuilder, Actual);
				stringBuilder.Append(" cannot be validated against <null>");
			}
			else if (expected.Length > Actual?.Length)
			{
				stringBuilder.Append(It).Append(" was ");
				Formatter.Format(stringBuilder, Actual);
				stringBuilder.Append(" and with length ").Append(Actual?.Length)
					.Append(" is shorter than the expected length of ").Append(expected.Length);
			}
			else
			{
				stringBuilder.Append(It).Append(" was ");
				Formatter.Format(stringBuilder, Actual);
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("does not end with ");
			Formatter.Format(stringBuilder, expected);
			stringBuilder.Append(options);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (expected is null)
			{
				Formatter.Format(stringBuilder, Actual);
				stringBuilder.Append(" cannot be validated against <null>");
			}
			else
			{
				stringBuilder.Append(It).Append(" was ");
				Formatter.Format(stringBuilder, Actual);
			}
		}
	}
}
