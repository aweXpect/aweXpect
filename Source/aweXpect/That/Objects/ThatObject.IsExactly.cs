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
	public static AndOrWhoseResult<TType, IThat<object?>> IsExactly<TType>(
		this IThat<object?> source)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsExactlyOfTypeConstraint<TType>(it, grammars)),
			source);

	/// <summary>
	///     Verifies that the subject is exactly of type <paramref name="type" />.
	/// </summary>
	public static AndOrResult<object?, IThat<object?>> IsExactly(
		this IThat<object?> source,
		Type type)
	{
		// ReSharper disable once LocalizableElement
		_ = type ?? throw new ArgumentNullException(nameof(type), "The type cannot be null.");
		return new AndOrResult<object?, IThat<object?>>(source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsExactlyOfTypeConstraint(it, grammars, type)),
			source);
	}

	/// <summary>
	///     Verifies that the subject is not exactly of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrResult<object?, IThat<object?>> IsNotExactly<TType>(
		this IThat<object?> source)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsExactlyOfTypeConstraint<TType>(it, grammars).Invert()),
			source);

	/// <summary>
	///     Verifies that the subject is not exactly of type <paramref name="type" />.
	/// </summary>
	public static AndOrResult<object?, IThat<object?>> IsNotExactly(
		this IThat<object?> source,
		Type type)
	{
		// ReSharper disable once LocalizableElement
		_ = type ?? throw new ArgumentNullException(nameof(type), "The type cannot be null.");
		return new AndOrResult<object?, IThat<object?>>(source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsExactlyOfTypeConstraint(it, grammars, type).Invert()),
			source);
	}

	private sealed class IsExactlyOfTypeConstraint<TType>(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithValue<object?>(grammars),
			IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			Actual = actual;
			Outcome = actual?.GetType() == typeof(TType) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is exactly type ");
			Formatter.Format(stringBuilder, typeof(TType));
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not exactly type ");
			Formatter.Format(stringBuilder, typeof(TType));
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class IsExactlyOfTypeConstraint(string it, ExpectationGrammars grammars, Type type)
		: ConstraintResult.WithValue<object>(grammars),
			IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			Actual = actual;
			Outcome = actual?.GetType() == type ||
			          type.IsGenericTypeDefinition && actual?.GetType().IsGenericType == true &&
			          actual.GetType().GetGenericTypeDefinition() == type
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is exactly type ");
			Formatter.Format(stringBuilder, type);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not exactly type ");
			Formatter.Format(stringBuilder, type);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
}
