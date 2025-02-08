using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies that the subject is of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrWhichResult<TType, IThat<object?>> Is<TType>(
		this IThat<object?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form)
				=> new IsOfTypeConstraint<TType>(it)),
			source);

	/// <summary>
	///     Verifies that the subject is of type <paramref name="type" />.
	/// </summary>
	public static AndOrWhichResult<T?, IThat<T?>> Is<T>(
		this IThat<T?> source,
		Type type)
		where T : class
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form)
				=> new IsOfTypeConstraint(it, type)),
			source);

	/// <summary>
	///     Verifies that the subject is not of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrWhichResult<object?, IThat<object?>> IsNot<TType>(
		this IThat<object?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form)
				=> new IsNotOfTypeConstraint<TType>(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not of type <paramref name="type" />.
	/// </summary>
	public static AndOrWhichResult<T?, IThat<T?>> IsNot<T>(
		this IThat<T?> source,
		Type type)
		where T : class
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form)
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
			=> $"is type {Formatter.Format(typeof(TType))}";
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
			=> $"is type {Formatter.Format(type)}";
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
			=> $"is not type {Formatter.Format(typeof(TType))}";
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
			=> $"is not type {Formatter.Format(type)}";
	}
}
