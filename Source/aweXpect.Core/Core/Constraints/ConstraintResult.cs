using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Constraints;

/// <summary>
///     A typed <see cref="ConstraintResult" />.
/// </summary>
public abstract class ConstraintResult<T>(ExpectationGrammars grammars) : ConstraintResult
{
	/// <summary>
	///     The actual value.
	/// </summary>
	protected T? Actual { get; set; }

	/// <summary>
	///     Appends the expectation to the <paramref name="stringBuilder" /> when the <see cref="ExpectationGrammars" /> are not negated.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected abstract void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null);

	/// <summary>
	///     Appends the result to the <paramref name="stringBuilder" /> when the <see cref="ExpectationGrammars" /> are not negated.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected abstract void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null);

	/// <summary>
	///     Appends the expectation to the <paramref name="stringBuilder" /> when the <see cref="ExpectationGrammars" /> are negated.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected abstract void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null);

	/// <summary>
	///     Appends the result to the <paramref name="stringBuilder" /> when the <see cref="ExpectationGrammars" /> are negated.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected abstract void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null);

	/// <inheritdoc cref="ConstraintResult.AppendExpectation(StringBuilder, string?)" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
	{
		if (grammars.HasFlag(ExpectationGrammars.Negated))
		{
			AppendNegatedExpectation(stringBuilder, indentation);
		}
		else
		{
			AppendNormalExpectation(stringBuilder, indentation);
		}
	}

	/// <inheritdoc cref="ConstraintResult.AppendResult(StringBuilder, string?)" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
	{
		if (grammars.HasFlag(ExpectationGrammars.Negated))
		{
			AppendNegatedResult(stringBuilder, indentation);
		}
		else
		{
			AppendNormalResult(stringBuilder, indentation);
		}
	}

	/// <inheritdoc cref="ConstraintResult.TryGetValue{TValue}(out TValue)" />
	public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
	{
		if (Actual is TValue typedValue)
		{
			value = typedValue;
			return true;
		}

		value = default;
		return typeof(TValue).IsAssignableFrom(typeof(T));
	}
}

/// <summary>
///     The result of the check if an expectation is met.
/// </summary>
public abstract partial class ConstraintResult
{
	/// <summary>
	///     Initializes a new instance of <see cref="ConstraintResult" />.
	/// </summary>
	protected ConstraintResult(
		Outcome outcome,
		FurtherProcessingStrategy furtherProcessingStrategy)
	{
		Outcome = outcome;
		FurtherProcessingStrategy = furtherProcessingStrategy;
	}

	/// <summary>
	///     Initializes a new instance of <see cref="ConstraintResult" />.
	/// </summary>
	protected ConstraintResult()
	{
		Outcome = Outcome.Undecided;
		FurtherProcessingStrategy = FurtherProcessingStrategy.Continue;
	}

	/// <summary>
	///     The outcome of the <see cref="ConstraintResult" />.
	/// </summary>
	public Outcome Outcome { get; protected set; }

	/// <summary>
	///     Specifies if further processing of chained constraints should be ignored.
	/// </summary>
	public FurtherProcessingStrategy FurtherProcessingStrategy { get; protected set; }

	/// <summary>
	///     Appends the expectation to the <paramref name="stringBuilder" />.
	/// </summary>
	public abstract void AppendExpectation(StringBuilder stringBuilder, string? indentation = null);

	/// <summary>
	///     Appends the result text to the <paramref name="stringBuilder" />.
	/// </summary>
	public abstract void AppendResult(StringBuilder stringBuilder, string? indentation = null);

	/// <summary>
	///     Tries to extract the <paramref name="value" /> that is stored in the constraint result.
	/// </summary>
	public virtual bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
	{
		value = default;
		return false;
	}

	/// <summary>
	///     A <see cref="ConstraintResult" /> with a text-based expectation.
	/// </summary>
	public class TextBasedConstraintResult : ConstraintResult
	{
		private readonly string? _expectationText;

		/// <summary>
		///     Initializes a new instance of <see cref="ConstraintResult" />.
		/// </summary>
		protected TextBasedConstraintResult(
			Outcome outcome,
			string expectationText,
			FurtherProcessingStrategy furtherProcessingStrategy)
			: base(outcome, furtherProcessingStrategy)
		{
			_expectationText = expectationText;
		}

		/// <inheritdoc />
		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(_expectationText.Indent(indentation, false));

		/// <inheritdoc />
		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			// Do nothing
		}
	}
}
