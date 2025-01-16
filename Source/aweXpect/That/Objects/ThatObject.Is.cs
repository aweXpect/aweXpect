using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies that the subject is...
	/// </summary>
	public static IThatIs<object?> Is(
		this IExpectSubject<object?> source)
		=> source.ThatIs();

	/// <summary>
	///     Verifies that the subject is of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrWhichResult<TType, IExpectSubject<object?>> Is<TType>(
		this IExpectSubject<object?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsOfTypeConstraint<TType>(it)),
			source);

	/// <summary>
	///     Verifies that the subject is of type <paramref name="type" />.
	/// </summary>
	public static AndOrWhichResult<object?, IExpectSubject<object?>> Is(
		this IExpectSubject<object?> source,
		Type type)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsOfTypeConstraint(it, type)),
			source);

	/// <summary>
	///     Verifies that the subject is not of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrWhichResult<object?, IExpectSubject<object?>> IsNot<TType>(
		this IExpectSubject<object?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsNotOfTypeConstraint<TType>(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not of type <paramref name="type" />.
	/// </summary>
	public static AndOrWhichResult<object?, IExpectSubject<object?>> IsNot(
		this IExpectSubject<object?> source,
		Type type)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsNotOfTypeConstraint(it, type)),
			source);

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
