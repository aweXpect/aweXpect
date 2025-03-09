using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

/// <summary>
///     Expectations on numeric values.
/// </summary>
public static partial class ThatNumber
{
	private class GenericConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		T? expected,
		Func<T?, string> expectation,
		Func<T, T?, bool> condition,
		Func<T, T?, string, string> failureMessageFactory)
		: ConstraintResult.WithValue<T>(grammars),
			IValueConstraint<T>
		where T : struct
	{
		public ConstraintResult IsMetBy(T actual)
		{
			Actual = actual;
			Outcome = condition(actual, expected) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectation(expected));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(failureMessageFactory(Actual, expected, it));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();
	}

	private class NullableGenericConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		T? expected,
		Func<T?, string> expectation,
		Func<T?, T?, bool> condition,
		Func<T?, T?, string, string> failureMessageFactory)
		: ConstraintResult.WithValue<T?>(grammars),
			IValueConstraint<T?>
		where T : struct
	{
		public ConstraintResult IsMetBy(T? actual)
		{
			Actual = actual;
			Outcome = condition(actual, expected) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectation(expected));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(failureMessageFactory(Actual, expected, it));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();
	}

	private class GenericArrayConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		T[] expected,
		Func<T[], string> expectation,
		Func<T, T[], bool> condition,
		Func<T, T[], string, string> failureMessageFactory)
		: ConstraintResult.WithValue<T>(grammars),
			IValueConstraint<T>
		where T : struct
	{
		public ConstraintResult IsMetBy(T actual)
		{
			Actual = actual;
			Outcome = condition(actual, expected) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectation(expected));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(failureMessageFactory(Actual, expected, it));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();
	}

	private class GenericArrayConstraintWithNullableValues<T>(
		string it,
		ExpectationGrammars grammars,
		T?[] expected,
		Func<T?[], string> expectation,
		Func<T, T?[], bool> condition,
		Func<T, T?[], string, string> failureMessageFactory)
		: ConstraintResult.WithValue<T>(grammars),
			IValueConstraint<T>
		where T : struct
	{
		public ConstraintResult IsMetBy(T actual)
		{
			Actual = actual;
			Outcome = condition(actual, expected) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectation(expected));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(failureMessageFactory(Actual, expected, it));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();
	}

	private class NullableGenericArrayConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		T[] expected,
		Func<T[], string> expectation,
		Func<T?, T[], bool> condition,
		Func<T?, T[], string, string> failureMessageFactory)
		: ConstraintResult.WithValue<T?>(grammars),
			IValueConstraint<T?>
		where T : struct
	{
		public ConstraintResult IsMetBy(T? actual)
		{
			Actual = actual;
			Outcome = condition(actual, expected) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectation(expected));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(failureMessageFactory(Actual, expected, it));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();
	}

	private class NullableGenericArrayConstraintWithNullableValues<T>(
		string it,
		ExpectationGrammars grammars,
		T?[] expected,
		Func<T?[], string> expectation,
		Func<T?, T?[], bool> condition,
		Func<T?, T?[], string, string> failureMessageFactory)
		: ConstraintResult.WithValue<T?>(grammars),
			IValueConstraint<T?>
		where T : struct
	{
		public ConstraintResult IsMetBy(T? actual)
		{
			Actual = actual;
			Outcome = condition(actual, expected) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectation(expected));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(failureMessageFactory(Actual, expected, it));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();
	}
}
