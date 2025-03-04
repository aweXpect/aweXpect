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
				=> new IsEqualToConstraint<object?, object?>(it, grammars, expected, doNotPopulateThisValue.TrimCommonWhiteSpace(), options)),
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
				=> new IsNullableEqualToConstraint<T>(it, grammars, expected, doNotPopulateThisValue.TrimCommonWhiteSpace(), options)),
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
				=> new IsEqualToConstraint<T>(it, grammars, expected, doNotPopulateThisValue.TrimCommonWhiteSpace(), options)),
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
				=> new IsNotEqualToConstraint(it, grammars, unexpected, doNotPopulateThisValue.TrimCommonWhiteSpace(), options)),
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
				=> new IsNullableNotEqualToConstraint<T>(it, grammars, unexpected, doNotPopulateThisValue.TrimCommonWhiteSpace(), options)),
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
				=> new IsNotEqualToConstraint<T>(it, grammars, unexpected, doNotPopulateThisValue.TrimCommonWhiteSpace(), options)),
			source,
			options);
	}

	private readonly struct IsEqualToConstraint<TSubject, TExpected>(
		string it,
		ExpectationGrammars grammars,
		TExpected expected,
		string expectedExpression,
		ObjectEqualityOptions<TSubject> options)
		: IValueConstraint<TSubject>
	{
		public ConstraintResult IsMetBy(TSubject actual)
		{
			if (options.AreConsideredEqual(actual, expected))
			{
				return ConstraintResult.CreateWithObjects(Outcome.Success, actual, expected, it, grammars, ToString,
					options.GetExtendedFailure);
			}

			return ConstraintResult.CreateWithObjects(Outcome.Failure, actual, expected, it, grammars, ToString,
				options.GetExtendedFailure);
		}

		public override string ToString()
			=> options.GetExpectation(expectedExpression.TrimCommonWhiteSpace(), grammars);
	}

	private readonly struct IsEqualToConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		T? expected,
		string expectedExpression,
		ObjectEqualityOptions<T> options)
		: IValueConstraint<T>
		where T : struct
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (options.AreConsideredEqual(actual, expected))
			{
				return ConstraintResult.CreateWithObjects(Outcome.Success, actual, expected, it, grammars, ToString,
					options.GetExtendedFailure);
			}

			return ConstraintResult.CreateWithObjects(Outcome.Failure, actual, expected, it, grammars, ToString,
				options.GetExtendedFailure);
		}

		public override string ToString()
			=> options.GetExpectation(expectedExpression, grammars);
	}

	private readonly struct IsNullableEqualToConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		T? expected,
		string expectedExpression,
		ObjectEqualityOptions<T?> options)
		: IValueConstraint<T?>
		where T : struct
	{
		public ConstraintResult IsMetBy(T? actual)
		{
			if (options.AreConsideredEqual(actual, expected))
			{
				return ConstraintResult.CreateWithObjects(Outcome.Success, actual, expected, it, grammars, ToString,
					options.GetExtendedFailure);
			}

			return ConstraintResult.CreateWithObjects(Outcome.Failure, actual, expected, it, grammars, ToString,
				options.GetExtendedFailure);
		}

		public override string ToString()
			=> options.GetExpectation(expectedExpression, grammars);
	}

	private readonly struct IsNotEqualToConstraint(
		string it,
		ExpectationGrammars grammars,
		object? unexpected,
		string unexpectedExpression,
		ObjectEqualityOptions<object?> options)
		: IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (options.AreConsideredEqual(actual, unexpected))
			{
				return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
			}

			return new ConstraintResult.Success<object?>(actual, ToString());
		}

		public override string ToString()
			=> options.GetExpectation(unexpectedExpression, grammars.Negate());
	}

	private readonly struct IsNotEqualToConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		T? unexpected,
		string unexpectedExpression,
		ObjectEqualityOptions<T> options)
		: IValueConstraint<T>
		where T : struct
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (options.AreConsideredEqual(actual, unexpected))
			{
				return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
			}

			return new ConstraintResult.Success<T>(actual, ToString());
		}

		public override string ToString()
			=> options.GetExpectation(unexpectedExpression, grammars.Negate());
	}

	private readonly struct IsNullableNotEqualToConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		T? unexpected,
		string unexpectedExpression,
		ObjectEqualityOptions<T?> options)
		: IValueConstraint<T?>
		where T : struct
	{
		public ConstraintResult IsMetBy(T? actual)
		{
			if (options.AreConsideredEqual(actual, unexpected))
			{
				return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
			}

			return new ConstraintResult.Success<T?>(actual, ToString());
		}

		public override string ToString()
			=> options.GetExpectation(unexpectedExpression, grammars.Negate());
	}
}
