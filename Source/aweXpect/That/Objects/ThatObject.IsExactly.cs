using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies that the subject is exactly of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrWhichResult<TType, IExpectSubject<object?>> IsExactly<TType>(
		this IExpectSubject<object?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsExactlyOfTypeConstraint<TType>(it)),
			source);

	/// <summary>
	///     Verifies that the subject is exactly of type <paramref name="type" />.
	/// </summary>
	public static AndOrWhichResult<object?, IExpectSubject<object?>> IsExactly(
		this IExpectSubject<object?> source,
		Type type)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsExactlyOfTypeConstraint(it, type)),
			source);

	/// <summary>
	///     Verifies that the subject is not exactly of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrWhichResult<object?, IExpectSubject<object?>> IsNotExactly<TType>(
		this IExpectSubject<object?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsNotExactlyOfTypeConstraint<TType>(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not exactly of type <paramref name="type" />.
	/// </summary>
	public static AndOrWhichResult<object?, IExpectSubject<object?>> IsNotExactly(
		this IExpectSubject<object?> source,
		Type type)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsNotExactlyOfTypeConstraint(it, type)),
			source);

	private readonly struct IsExactlyOfTypeConstraint<TType>(string it) : IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (actual is TType typedActual && actual.GetType() == typeof(TType))
			{
				return new ConstraintResult.Success<TType>(typedActual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> $"be exactly type {Formatter.Format(typeof(TType))}";
	}

	private readonly struct IsExactlyOfTypeConstraint(string it, Type type) : IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (actual?.GetType() == type)
			{
				return new ConstraintResult.Success<object?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> $"be exactly type {Formatter.Format(type)}";
	}

	private readonly struct IsNotExactlyOfTypeConstraint<TType>(string it) : IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (actual is TType typedActual && actual.GetType() == typeof(TType))
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was {Formatter.Format(typedActual, FormattingOptions.MultipleLines)}");
			}

			return new ConstraintResult.Success<object?>(actual, ToString());
		}

		public override string ToString()
			=> $"not be exactly type {Formatter.Format(typeof(TType))}";
	}

	private readonly struct IsNotExactlyOfTypeConstraint(string it, Type type) : IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (actual?.GetType() == type)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");
			}

			return new ConstraintResult.Success<object?>(actual, ToString());
		}

		public override string ToString()
			=> $"not be exactly type {Formatter.Format(type)}";
	}
}
