using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IThat<object?>, object?> IsEqualTo(
		this IThat<object?> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<object?> options = new();
		return new ObjectEqualityResult<object?, IThat<object?>, object?>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<object?, object?>(it, grammars, expected,
					doNotPopulateThisValue.TrimCommonWhiteSpace(), options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<T?, IThat<T?>, T?> IsEqualTo<T>(
		this IThat<T?> source,
		T? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		where T : struct
	{
		ObjectEqualityOptions<T?> options = new();
		return new ObjectEqualityResult<T?, IThat<T?>, T?>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsNullableEqualToConstraint<T>(it, grammars, expected,
					doNotPopulateThisValue.TrimCommonWhiteSpace(), options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<T, IThat<T>, T> IsEqualTo<T>(
		this IThat<T> source,
		T? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		where T : struct
	{
		ObjectEqualityOptions<T> options = new();
		return new ObjectEqualityResult<T, IThat<T>, T>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<T>(it, grammars, expected, doNotPopulateThisValue.TrimCommonWhiteSpace(),
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IThat<object?>, object?> IsNotEqualTo(
		this IThat<object?> source,
		object? unexpected,
		[CallerArgumentExpression("unexpected")]
		string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<object?> options = new();
		return new ObjectEqualityResult<object?, IThat<object?>, object?>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<object?, object?>(it, grammars, unexpected,
						doNotPopulateThisValue.TrimCommonWhiteSpace(), options)
					.Invert()),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<T?, IThat<T?>, T?> IsNotEqualTo<T>(
		this IThat<T?> source,
		T? unexpected,
		[CallerArgumentExpression("unexpected")]
		string doNotPopulateThisValue = "")
		where T : struct
	{
		ObjectEqualityOptions<T?> options = new();
		return new ObjectEqualityResult<T?, IThat<T?>, T?>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsNullableEqualToConstraint<T>(it, grammars, unexpected,
						doNotPopulateThisValue.TrimCommonWhiteSpace(), options)
					.Invert()),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<T, IThat<T>, T> IsNotEqualTo<T>(
		this IThat<T> source,
		T? unexpected,
		[CallerArgumentExpression("unexpected")]
		string doNotPopulateThisValue = "")
		where T : struct
	{
		ObjectEqualityOptions<T> options = new();
		return new ObjectEqualityResult<T, IThat<T>, T>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<T>(it, grammars, unexpected, doNotPopulateThisValue.TrimCommonWhiteSpace(),
						options)
					.Invert()),
			source,
			options);
	}

	private sealed class IsEqualToConstraint<TSubject, TExpected>(
		string it,
		ExpectationGrammars grammars,
		TExpected expected,
		string expectedExpression,
		ObjectEqualityOptions<TSubject> options)
		: ConstraintResult.WithEqualToValue<TSubject>(it, grammars, expected is null),
			IValueConstraint<TSubject>
	{
		public ConstraintResult IsMetBy(TSubject actual)
		{
			Actual = actual;
			Outcome = options.AreConsideredEqual(actual, expected) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExpectation(expectedExpression.TrimCommonWhiteSpace(), Grammars));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(It, Grammars, Actual, expected));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExpectation(expectedExpression.TrimCommonWhiteSpace(), Grammars));

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(It, Grammars, Actual, expected));
	}

	private sealed class IsEqualToConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		T? expected,
		string expectedExpression,
		ObjectEqualityOptions<T> options)
		: ConstraintResult.WithValue<T>(grammars),
			IValueConstraint<T>
		where T : struct
	{
		public ConstraintResult IsMetBy(T actual)
		{
			Actual = actual;
			Outcome = options.AreConsideredEqual(actual, expected) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExpectation(expectedExpression.TrimCommonWhiteSpace(), Grammars));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(it, Grammars, Actual, expected));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExpectation(expectedExpression.TrimCommonWhiteSpace(), Grammars));

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(it, Grammars, Actual, expected));
	}

	private sealed class IsNullableEqualToConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		T? expected,
		string expectedExpression,
		ObjectEqualityOptions<T?> options)
		: ConstraintResult.WithEqualToValue<T?>(it, grammars, expected is null),
			IValueConstraint<T?>
		where T : struct
	{
		public ConstraintResult IsMetBy(T? actual)
		{
			Actual = actual;
			Outcome = options.AreConsideredEqual(actual, expected) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExpectation(expectedExpression.TrimCommonWhiteSpace(), Grammars));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(It, Grammars, Actual, expected));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExpectation(expectedExpression.TrimCommonWhiteSpace(), Grammars));

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(It, Grammars, Actual, expected));
	}
}
