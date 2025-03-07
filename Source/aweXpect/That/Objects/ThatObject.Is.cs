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
	public static AndOrWhoseResult<TType, IThat<object?>> Is<TType>(
		this IThat<object?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsOfTypeConstraint<TType>(it, grammars)),
			source);

	/// <summary>
	///     Verifies that the subject is of type <paramref name="type" />.
	/// </summary>
	public static AndOrResult<T?, IThat<T?>> Is<T>(
		this IThat<T?> source,
		Type type)
		where T : class
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsOfTypeConstraint(it, grammars, type)),
			source);

	/// <summary>
	///     Verifies that the subject is not of type <typeparamref name="TType" />.
	/// </summary>
	public static AndOrResult<object?, IThat<object?>> IsNot<TType>(
		this IThat<object?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsOfTypeConstraint<TType>(it, grammars).Invert()),
			source);

	/// <summary>
	///     Verifies that the subject is not of type <paramref name="type" />.
	/// </summary>
	public static AndOrResult<T?, IThat<T?>> IsNot<T>(
		this IThat<T?> source,
		Type type)
		where T : class
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsOfTypeConstraint(it, grammars, type).Invert()),
			source);

	private class IsOfTypeConstraint<TType>(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithValue<object?>(grammars),
			IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			Actual = actual;
			Outcome = actual is TType ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is type ");
			Formatter.Format(stringBuilder, typeof(TType));
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not type ");
			Formatter.Format(stringBuilder, typeof(TType));
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.Indented(indentation));
		}
	}

	private class IsOfTypeConstraint(string it, ExpectationGrammars grammars, Type type)
		: ConstraintResult.WithValue<object>(grammars),
			IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			Actual = actual;
			Outcome = type.IsInstanceOfType(actual) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is type ");
			Formatter.Format(stringBuilder, type);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.Indented(indentation));
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not type ");
			Formatter.Format(stringBuilder, type);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.Indented(indentation));
		}
	}
}
