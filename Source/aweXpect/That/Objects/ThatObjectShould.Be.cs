using System;
using System.Runtime.CompilerServices;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Formatting;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatObjectShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IThat<object?>> Be(
		this IThat<object?> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<object?, IThat<object?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new IsEqualValueConstraint(it, expected, doNotPopulateThisValue, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrWhichResult<TType, IThat<object?>> Be<TType>(
		this IThat<object?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new IsValueConstraint<TType>(it)),
			source);

	/// <summary>
	///     Verifies that the subject is of type <paramref name="type" />.
	/// </summary>
	public static AndOrWhichResult<object?, IThat<object?>> Be(
		this IThat<object?> source,
		Type type)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new IsValueConstraint(it, type)),
			source);

	private readonly struct IsEqualValueConstraint(
		string it,
		object? expected,
		string expectedExpression,
		ObjectEqualityOptions options)
		: IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			ObjectEqualityOptions.Result result = options.AreConsideredEqual(actual, expected, it);

			if (!result.AreConsideredEqual)
			{
				return new ConstraintResult.Failure(ToString(), result.Failure);
			}

			return new ConstraintResult.Success<object?>(actual, ToString());
		}

		public override string ToString()
			=> options.GetExpectation(expectedExpression);
	}

	private readonly struct IsValueConstraint<TType>(string it) : IValueConstraint<object?>
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

	private readonly struct IsValueConstraint(string it, Type type) : IValueConstraint<object?>
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
}
