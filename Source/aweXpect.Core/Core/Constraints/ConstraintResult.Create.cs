using System;
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

	private sealed class FullTypedConstraintResult<TActual, TExpected> : ConstraintResult
	{
		private readonly TActual _actual;
		private readonly TExpected _expected;
		private readonly string _it;
		private readonly ExpectationGrammars _grammars;
		private readonly Func<ExpectationGrammars, TExpected, string>? _expectationBuilder;
		private readonly Func<string>? _expectationBuilderWithoutParameters;
		private readonly Func<string, ExpectationGrammars, TActual, TExpected, string> _resultBuilder;

		public FullTypedConstraintResult(Outcome outcome,
			TActual actual,
			TExpected expected,
			string it,
			ExpectationGrammars grammars,
			Func<ExpectationGrammars, TExpected, string>? expectationBuilder,
			Func<string>? expectationBuilderWithoutParameters,
			Func<string, ExpectationGrammars, TActual, TExpected, string> resultBuilder,
			FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue) : base(furtherProcessingStrategy)
		{
			Outcome = outcome;
			_actual = actual;
			_expected = expected;
			_it = it;
			_grammars = grammars;
			_expectationBuilder = expectationBuilder;
			_expectationBuilderWithoutParameters = expectationBuilderWithoutParameters;
			_resultBuilder = resultBuilder;
		}

		/// <inheritdoc cref="ConstraintResult.AppendExpectation(StringBuilder, string?)" />
		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_expectationBuilderWithoutParameters != null)
			{
				stringBuilder.Append(_expectationBuilderWithoutParameters().Indent(indentation));
			}

			if (_expectationBuilder != null)
			{
				stringBuilder.Append(_expectationBuilder(_grammars, _expected).Indent(indentation));
			}
		}

		/// <inheritdoc cref="ConstraintResult.AppendResult(StringBuilder, string?)" />
		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(_resultBuilder(_it, _grammars, _actual, _expected).Indent(indentation));

		/// <inheritdoc cref="ConstraintResult.TryGetValue{TValue}(out TValue)" />
		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_actual is TValue actualValue)
			{
				value = actualValue;
				return true;
			}

			value = default;
			return typeof(TValue).IsAssignableFrom(typeof(TActual));
		}

		public override ConstraintResult Negate()
		{
			Outcome = Outcome switch
			{
				Outcome.Failure => Outcome.Success,
				Outcome.Success => Outcome.Failure,
				_ => Outcome,
			};
			return this;
		}
	}

	private sealed class FullTypedConstraintResultWithObjectsResultBuilder<TActual, TExpected> : ConstraintResult
	{
		private readonly TActual? _actual;
		private readonly TExpected _expected;
		private readonly string _it;
		private readonly ExpectationGrammars _grammars;
		private readonly Func<ExpectationGrammars, TExpected, string>? _expectationBuilder;
		private readonly Func<string>? _expectationBuilderWithoutParameters;
		private readonly Func<string, ExpectationGrammars, object?, object?, string> _resultBuilder;

		public FullTypedConstraintResultWithObjectsResultBuilder(Outcome outcome,
			TActual? actual,
			TExpected expected,
			string it,
			ExpectationGrammars grammars,
			Func<ExpectationGrammars, TExpected, string>? expectationBuilder,
			Func<string>? expectationBuilderWithoutParameters,
			Func<string, ExpectationGrammars, object?, object?, string> resultBuilder,
			FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue) : base(furtherProcessingStrategy)
		{
			Outcome = outcome;
			_actual = actual;
			_expected = expected;
			_it = it;
			_grammars = grammars;
			_expectationBuilder = expectationBuilder;
			_expectationBuilderWithoutParameters = expectationBuilderWithoutParameters;
			_resultBuilder = resultBuilder;
		}

		/// <inheritdoc cref="ConstraintResult.AppendExpectation(StringBuilder, string?)" />
		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_expectationBuilderWithoutParameters != null)
			{
				stringBuilder.Append(_expectationBuilderWithoutParameters().Indent(indentation));
			}

			if (_expectationBuilder != null)
			{
				stringBuilder.Append(_expectationBuilder(_grammars, _expected).Indent(indentation));
			}
		}

		/// <inheritdoc cref="ConstraintResult.AppendResult(StringBuilder, string?)" />
		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(_resultBuilder(_it, _grammars, _actual, _expected).Indent(indentation));

		/// <inheritdoc cref="ConstraintResult.TryGetValue{TValue}(out TValue)" />
		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_actual is TValue actualValue)
			{
				value = actualValue;
				return true;
			}

			value = default;
			return typeof(TValue).IsAssignableFrom(typeof(TActual));
		}

		public override ConstraintResult Negate()
		{
			Outcome = Outcome switch
			{
				Outcome.Failure => Outcome.Success,
				Outcome.Success => Outcome.Failure,
				_ => Outcome,
			};
			return this;
		}
	}
}
