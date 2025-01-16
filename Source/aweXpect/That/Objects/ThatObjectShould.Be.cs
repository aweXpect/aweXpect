using System;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObjectShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IThatShould<object?>> Be(
		this IThatShould<object?> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<object?, IThatShould<object?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new IsEqualToConstraint(it, expected, doNotPopulateThisValue, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrWhichResult<TType, IThatShould<object?>> Be<TType>(
		this IThatShould<object?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new IsOfTypeConstraint<TType>(it)),
			source);

	/// <summary>
	///     Verifies that the subject is of type <paramref name="type" />.
	/// </summary>
	public static AndOrWhichResult<object?, IThatShould<object?>> Be(
		this IThatShould<object?> source,
		Type type)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new IsOfTypeConstraint(it, type)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IThatShould<object?>> NotBe(
		this IThatShould<object?> source,
		object? unexpected,
		[CallerArgumentExpression("unexpected")]
		string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<object?, IThatShould<object?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new IsNotEqualToConstraint(it, unexpected, doNotPopulateThisValue, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrWhichResult<object?, IThatShould<object?>> NotBe<TType>(
		this IThatShould<object?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new IsNotOfTypeConstraint<TType>(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not of type <paramref name="type" />.
	/// </summary>
	public static AndOrWhichResult<object?, IThatShould<object?>> NotBe(
		this IThatShould<object?> source,
		Type type)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new IsNotOfTypeConstraint(it, type)),
			source);

	private readonly struct IsEqualToConstraint(
		string it,
		object? expected,
		string expectedExpression,
		ObjectEqualityOptions options)
		: IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (options.AreConsideredEqual(actual, expected))
			{
				return new ConstraintResult.Success<object?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), options.GetExtendedFailure(it, actual, expected));
		}

		public override string ToString()
			=> options.GetExpectation(expectedExpression);
	}

	private readonly struct IsNotEqualToConstraint(
		string it,
		object? unexpected,
		string unexpectedExpression,
		ObjectEqualityOptions options)
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
			=> "not " + options.GetExpectation(unexpectedExpression);
	}

	private readonly struct IsOfTypeConstraint<TType>(string it) : IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (actual is TType typedActual)
			{
				return new ConstraintResult.Success<TType>(typedActual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> $"be type {Formatter.Format(typeof(TType))}";
	}

	private readonly struct IsOfTypeConstraint(string it, Type type) : IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (type.IsAssignableFrom(actual?.GetType()))
			{
				return new ConstraintResult.Success<object?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> $"be type {Formatter.Format(type)}";
	}

	private readonly struct IsNotOfTypeConstraint<TType>(string it) : IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (actual is TType typedActual)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was {Formatter.Format(typedActual, FormattingOptions.MultipleLines)}");
			}

			return new ConstraintResult.Success<object?>(actual, ToString());
		}

		public override string ToString()
			=> $"not be type {Formatter.Format(typeof(TType))}";
	}

	private readonly struct IsNotOfTypeConstraint(string it, Type type) : IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (type.IsAssignableFrom(actual?.GetType()))
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");
			}

			return new ConstraintResult.Success<object?>(actual, ToString());
		}

		public override string ToString()
			=> $"not be type {Formatter.Format(type)}";
	}
}
