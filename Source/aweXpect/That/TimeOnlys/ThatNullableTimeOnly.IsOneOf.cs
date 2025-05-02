#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableTimeOnly
{
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>> IsOneOf(
		this IThat<TimeOnly?> source,
		params TimeOnly?[] expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint(it, grammars, expected, tolerance)),
			source,
			tolerance);
	}
	
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>> IsOneOf(
		this IThat<TimeOnly?> source,
		IEnumerable<TimeOnly> expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint(it, grammars, expected.Cast<TimeOnly?>(), tolerance)),
			source,
			tolerance);
	}
	
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>> IsOneOf(
		this IThat<TimeOnly?> source,
		IEnumerable<TimeOnly?> expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint(it, grammars, expected, tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>> IsNotOneOf(
		this IThat<TimeOnly?> source,
		params TimeOnly?[] unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint(it, grammars, unexpected, tolerance).Invert()),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>> IsNotOneOf(
		this IThat<TimeOnly?> source,
		IEnumerable<TimeOnly> unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint(it, grammars, unexpected.Cast<TimeOnly?>(), tolerance).Invert()),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>> IsNotOneOf(
		this IThat<TimeOnly?> source,
		IEnumerable<TimeOnly?> unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsOneOfConstraint(it, grammars, unexpected, tolerance).Invert()),
			source,
			tolerance);
	}

	private sealed class IsOneOfConstraint(
		string it,
		ExpectationGrammars grammars,
		IEnumerable<TimeOnly?> expected,
		TimeTolerance tolerance)
		: ConstraintResult.WithValue<TimeOnly?>(grammars),
			IValueConstraint<TimeOnly?>
	{
		public ConstraintResult IsMetBy(TimeOnly? actual)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
			}
			else
			{
				TimeSpan timeTolerance = tolerance.Tolerance ??
				                         Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get();
				Outcome = expected.Any(value =>
					value != null &&
					Math.Abs(actual.Value.Ticks - value.Value.Ticks) <= timeTolerance.Ticks)
					? Outcome.Success
					: Outcome.Failure;
			}

			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is one of ");
			Formatter.Format(stringBuilder, expected);
			stringBuilder.Append(tolerance);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not one of ");
			Formatter.Format(stringBuilder, expected);
			stringBuilder.Append(tolerance);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
}
#endif
