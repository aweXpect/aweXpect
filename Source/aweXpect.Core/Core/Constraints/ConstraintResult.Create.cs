﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Constraints;

/// <summary>
///     The result of the check if an expectation is met.
/// </summary>
public abstract partial class ConstraintResult
{
	/// <summary>
	///     Creates a new <see cref="ConstraintResult" />.
	/// </summary>
	/// <param name="outcome">The outcome of the constraint result.</param>
	/// <param name="actual">The actual value.</param>
	/// <param name="expected">The expected value.</param>
	/// <param name="it">The "it" expression.</param>
	/// <param name="grammars">The <see cref="ExpectationGrammars" /> to use.</param>
	/// <param name="expectationBuilder">The builder for creating the expectation string.</param>
	/// <param name="resultBuilder">The builder for creating the result string.</param>
	/// <param name="furtherProcessingStrategy">The strategy for further processing additional expectations.</param>
	public static ConstraintResult Create<TActual, TExpected>(
		Outcome outcome,
		TActual actual,
		TExpected expected,
		string it,
		ExpectationGrammars grammars,
		Func<ExpectationGrammars, TExpected, string> expectationBuilder,
		Func<string, ExpectationGrammars, TActual, TExpected, string> resultBuilder,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
		=> new FullTypedConstraintResult<TActual, TExpected>(
			outcome,
			actual,
			expected,
			it,
			grammars,
			expectationBuilder, null,
			resultBuilder,
			furtherProcessingStrategy);

	/// <summary>
	///     Creates a new <see cref="ConstraintResult" />.
	/// </summary>
	/// <param name="outcome">The outcome of the constraint result.</param>
	/// <param name="actual">The actual value.</param>
	/// <param name="expected">The expected value.</param>
	/// <param name="it">The "it" expression.</param>
	/// <param name="grammars">The <see cref="ExpectationGrammars" /> to use.</param>
	/// <param name="expectationBuilder">The builder for creating the expectation string.</param>
	/// <param name="successResultBuilder">The builder for creating the result string when <paramref name="outcome"/> is <see cref="Outcome.Success"/>.</param>
	/// <param name="failureResultBuilder">The builder for creating the result string when <paramref name="outcome"/> is <see cref="Outcome.Failure"/>.</param>
	/// <param name="furtherProcessingStrategy">The strategy for further processing additional expectations.</param>
	public static ConstraintResult FromOutcome<TActual, TExpected>(
		Outcome outcome,
		TActual actual,
		TExpected expected,
		string it,
		ExpectationGrammars grammars,
		Func<ExpectationGrammars, TExpected, string> expectationBuilder,
		Func<string, ExpectationGrammars, TActual, TExpected, string> failureResultBuilder,
		Func<string, ExpectationGrammars, TActual, TExpected, string> successResultBuilder,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
		=> new FullTypedConstraintResult<TActual, TExpected>(
			outcome,
			actual,
			expected,
			it,
			grammars,
			expectationBuilder, null,
			outcome == Outcome.Success ? successResultBuilder : failureResultBuilder,
			furtherProcessingStrategy);

	/// <summary>
	///     Creates a new <see cref="ConstraintResult" />.
	/// </summary>
	/// <param name="outcome">The outcome of the constraint result.</param>
	/// <param name="actual">The actual value.</param>
	/// <param name="expected">The expected value.</param>
	/// <param name="it">The "it" expression.</param>
	/// <param name="grammars">The <see cref="ExpectationGrammars" /> to use.</param>
	/// <param name="expectationBuilder">The builder for creating the expectation string.</param>
	/// <param name="successResultBuilder">The builder for creating the result string when <paramref name="outcome"/> is <see cref="Outcome.Success"/>.</param>
	/// <param name="failureResultBuilder">The builder for creating the result string when <paramref name="outcome"/> is <see cref="Outcome.Failure"/>.</param>
	/// <param name="furtherProcessingStrategy">The strategy for further processing additional expectations.</param>
	public static ConstraintResult FromOutcome<TActual, TExpected>(
		Outcome outcome,
		TActual actual,
		TExpected expected,
		string it,
		ExpectationGrammars grammars,
		Func<string> expectationBuilder,
		Func<string, ExpectationGrammars, TActual, TExpected, string> failureResultBuilder,
		Func<string, ExpectationGrammars, TActual, TExpected, string> successResultBuilder,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
		=> new FullTypedConstraintResult<TActual, TExpected>(
			outcome,
			actual,
			expected,
			it,
			grammars,
			null, expectationBuilder,
			outcome == Outcome.Success ? successResultBuilder : failureResultBuilder,
			furtherProcessingStrategy);
	/// <summary>
	///     Creates a new <see cref="ConstraintResult" />.
	/// </summary>
	/// <param name="outcome">The outcome of the constraint result.</param>
	/// <param name="actual">The actual value.</param>
	/// <param name="expected">The expected value.</param>
	/// <param name="it">The "it" expression.</param>
	/// <param name="grammars">The <see cref="ExpectationGrammars" /> to use.</param>
	/// <param name="expectationBuilder">The builder for creating the expectation string.</param>
	/// <param name="resultBuilder">The builder for creating the result string.</param>
	/// <param name="furtherProcessingStrategy">The strategy for further processing additional expectations.</param>
	public static ConstraintResult Create<TActual, TExpected>(
		Outcome outcome,
		TActual actual,
		TExpected expected,
		string it,
		ExpectationGrammars grammars,
		Func<string> expectationBuilder,
		Func<string, ExpectationGrammars, TActual, TExpected, string> resultBuilder,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
		=> new FullTypedConstraintResult<TActual, TExpected>(
			outcome,
			actual,
			expected,
			it,
			grammars,
			null, expectationBuilder,
			resultBuilder,
			furtherProcessingStrategy);

	/// <summary>
	///     Creates a new <see cref="ConstraintResult" /> with objects in the <paramref name="resultBuilder" />.
	/// </summary>
	/// <param name="outcome">The outcome of the constraint result.</param>
	/// <param name="actual">The actual value.</param>
	/// <param name="expected">The expected value.</param>
	/// <param name="it">The "it" expression.</param>
	/// <param name="grammars">The <see cref="ExpectationGrammars" /> to use.</param>
	/// <param name="expectationBuilder">The builder for creating the expectation string.</param>
	/// <param name="resultBuilder">The builder for creating the result string.</param>
	/// <param name="furtherProcessingStrategy">The strategy for further processing additional expectations.</param>
	public static ConstraintResult CreateWithObjects<TActual, TExpected>(
		Outcome outcome,
		TActual actual,
		TExpected expected,
		string it,
		ExpectationGrammars grammars,
		Func<ExpectationGrammars, TExpected, string> expectationBuilder,
		Func<string, ExpectationGrammars, object?, object?, string> resultBuilder,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
		=> new FullTypedConstraintResultWithObjectsResultBuilder<TActual, TExpected>(
			outcome,
			actual,
			expected,
			it,
			grammars,
			expectationBuilder, null,
			resultBuilder,
			furtherProcessingStrategy);

	/// <summary>
	///     Creates a new <see cref="ConstraintResult" /> with objects in the <paramref name="resultBuilder" />.
	/// </summary>
	/// <param name="outcome">The outcome of the constraint result.</param>
	/// <param name="actual">The actual value.</param>
	/// <param name="expected">The expected value.</param>
	/// <param name="it">The "it" expression.</param>
	/// <param name="grammars">The <see cref="ExpectationGrammars" /> to use.</param>
	/// <param name="expectationBuilder">The builder for creating the expectation string.</param>
	/// <param name="resultBuilder">The builder for creating the result string.</param>
	/// <param name="furtherProcessingStrategy">The strategy for further processing additional expectations.</param>
	public static ConstraintResult CreateWithObjects<TActual, TExpected>(
		Outcome outcome,
		TActual actual,
		TExpected expected,
		string it,
		ExpectationGrammars grammars,
		Func<string> expectationBuilder,
		Func<string, ExpectationGrammars, object?, object?, string> resultBuilder,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
		=> new FullTypedConstraintResultWithObjectsResultBuilder<TActual, TExpected>(
			outcome,
			actual,
			expected,
			it,
			grammars,
			null, expectationBuilder,
			resultBuilder,
			furtherProcessingStrategy);

	private sealed class FullTypedConstraintResult<TActual, TExpected>(
		Outcome outcome,
		TActual actual,
		TExpected expected,
		string it,
		ExpectationGrammars grammars,
		Func<ExpectationGrammars, TExpected, string>? expectationBuilder,
		Func<string>? expectationBuilderWithoutParameters,
		Func<string, ExpectationGrammars, TActual, TExpected, string> resultBuilder,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
		: ConstraintResult(outcome, furtherProcessingStrategy)
	{
		/// <inheritdoc cref="ConstraintResult.AppendExpectation(StringBuilder, string?)" />
		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (expectationBuilderWithoutParameters != null)
			{
				stringBuilder.Append(expectationBuilderWithoutParameters().Indent(indentation));
			}

			if (expectationBuilder != null)
			{
				stringBuilder.Append(expectationBuilder(grammars, expected).Indent(indentation));
			}
		}

		/// <inheritdoc cref="ConstraintResult.AppendResult(StringBuilder, string?)" />
		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(resultBuilder(it, grammars, actual, expected).Indent(indentation));

		/// <inheritdoc cref="ConstraintResult.TryGetValue{TValue}(out TValue)" />
		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (actual is TValue actualValue)
			{
				value = actualValue;
				return true;
			}

			value = default;
			return typeof(TValue).IsAssignableFrom(typeof(TActual));
		}
	}

	private sealed class FullTypedConstraintResultWithObjectsResultBuilder<TActual, TExpected>(
		Outcome outcome,
		TActual? actual,
		TExpected expected,
		string it,
		ExpectationGrammars grammars,
		Func<ExpectationGrammars, TExpected, string>? expectationBuilder,
		Func<string>? expectationBuilderWithoutParameters,
		Func<string, ExpectationGrammars, object?, object?, string> resultBuilder,
		FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
		: ConstraintResult(outcome, furtherProcessingStrategy)
	{
		/// <inheritdoc cref="ConstraintResult.AppendExpectation(StringBuilder, string?)" />
		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (expectationBuilderWithoutParameters != null)
			{
				stringBuilder.Append(expectationBuilderWithoutParameters().Indent(indentation));
			}

			if (expectationBuilder != null)
			{
				stringBuilder.Append(expectationBuilder(grammars, expected).Indent(indentation));
			}
		}

		/// <inheritdoc cref="ConstraintResult.AppendResult(StringBuilder, string?)" />
		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(resultBuilder(it, grammars, actual, expected).Indent(indentation));

		/// <inheritdoc cref="ConstraintResult.TryGetValue{TValue}(out TValue)" />
		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (actual is TValue actualValue)
			{
				value = actualValue;
				return true;
			}

			value = default;
			return typeof(TValue).IsAssignableFrom(typeof(TActual));
		}
	}
}
