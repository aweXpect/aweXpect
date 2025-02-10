using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Constraints;

public enum Outcome
{
	Success,
	Failure,
	Undecided
}
/// <summary>
///     The result of the check if an expectation is met.
/// </summary>
public abstract class ConstraintResult
{
	public Outcome Outcome { get; }

	/// <summary>
	///     The strategy how to continue processing after the current result.
	/// </summary>
	public enum FurtherProcessing
	{
		/// <summary>
		///     Continue processing.
		/// </summary>
		/// <remarks>
		///     This is the default behaviour.
		/// </remarks>
		Continue,

		/// <summary>
		///     Completely ignore all future constraints.
		/// </summary>
		IgnoreCompletely,

		/// <summary>
		///     Ignore the result of future constraints, but include their expectations.
		/// </summary>
		IgnoreResult,
	}

	private string? _expectationText;
	private Action<StringBuilder>? _prependExpectationText;
	private Action<StringBuilder>? _appendExpectationText;

	/// <summary>
	///     Initializes a new instance of <see cref="ConstraintResult" />.
	/// </summary>
	protected ConstraintResult(
		Outcome outcome,
		string expectationText,
		FurtherProcessing furtherProcessingStrategy)
	{
		Outcome = outcome;
		_expectationText = expectationText;
		FurtherProcessingStrategy = furtherProcessingStrategy;
	}

	/// <summary>
	///     Initializes a new instance of <see cref="ConstraintResult" />.
	/// </summary>
	protected ConstraintResult(
		Outcome outcome,
		FurtherProcessing furtherProcessingStrategy)
	{
		Outcome = outcome;
		FurtherProcessingStrategy = furtherProcessingStrategy;
	}

	public virtual void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
	{
		_prependExpectationText?.Invoke(stringBuilder);
		stringBuilder.Append(_expectationText.Indent(indentation, false));
		_appendExpectationText?.Invoke(stringBuilder);
	}

	public virtual void AppendResult(StringBuilder stringBuilder, string? indentation = null)
	{
		// Do nothing
	}

	internal virtual string GetResultText()
	{
		var sb = new StringBuilder();
		AppendResult(sb);
		return sb.ToString();
	}
	internal virtual bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
	{
		value = default;
		return false;
	}

	/// <summary>
	///     A human-readable representation of the expectation.
	/// </summary>
	public string ExpectationText => _expectationText ?? throw new NotImplementedException();

	/// <summary>
	///     Specifies if further processing of chained constraints should be ignored.
	/// </summary>
	public FurtherProcessing FurtherProcessingStrategy { get; }

	/// <summary>
	///     Combines the result with the provided <paramref name="expectationText" /> and <paramref name="resultText" />.
	/// </summary>
	// TODO VAB: Remove
	public virtual ConstraintResult CombineWith(string expectationText, string resultText) { return this;}

	/// <summary>
	///     Updates the expectation text of the current <see cref="ConstraintResult" />.
	/// </summary>
	// TODO VAB: Remove
	internal virtual ConstraintResult UpdateExpectationText(
		Action<StringBuilder>? prependExpectationText = null,
		Action<StringBuilder>? appendExpectationText = null)
	{
		_prependExpectationText = prependExpectationText ?? _prependExpectationText;
		_appendExpectationText = appendExpectationText ?? _appendExpectationText;
		return this;
	}

	// TODO VAB: Remove
	internal virtual ConstraintResult UseValue<T>(T value) { return this;}

	/// <summary>
	///     The actual value met the expectation.
	/// </summary>
	public class Success : ConstraintResult
	{
		/// <summary>
		///     Initializes a new instance of <see cref="ConstraintResult.Success" />.
		/// </summary>
		public Success(
			string expectationText,
			FurtherProcessing furtherProcessingStrategy = FurtherProcessing.Continue)
			: base(
				Outcome.Success,
				expectationText,
				furtherProcessingStrategy)
		{
		}

		/// <inheritdoc cref="ConstraintResult.CombineWith(string, string)" />
		public override ConstraintResult CombineWith(string expectationText, string resultText)
			=> new Success(expectationText);

		/// <inheritdoc />
		public override string ToString()
			=> $"SUCCEEDED {ExpectationText}";

		/// <inheritdoc />
		internal override ConstraintResult UseValue<T>(T value)
			=> new Success<T>(value, ExpectationText, FurtherProcessingStrategy);
	}

	/// <summary>
	///     The actual value met the expectation and a <see cref="Value" /> is stored for further processing.
	/// </summary>
	public class Success<T> : Success
	{
		/// <summary>
		///     Initializes a new instance of <see cref="ConstraintResult.Success{T}" />.
		/// </summary>
		public Success(
			T value,
			string expectationText,
			FurtherProcessing furtherProcessingStrategy = FurtherProcessing.Continue)
			: base(
				expectationText,
				furtherProcessingStrategy)
		{
			Value = value;
		}

		/// <summary>
		///     A value for further processing.
		/// </summary>
		public T Value { get; }

		/// <inheritdoc cref="ConstraintResult.CombineWith(string, string)" />
		public override ConstraintResult CombineWith(string expectationText, string resultText)
			=> new Success<T>(Value, expectationText);

		internal override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
			where TValue : default
		{
			if (Value is TValue v)
			{
				value = v;
				return true;
			}

			value = default;
			return Value is null;
		}
	}

	/// <summary>
	///     The actual value did not meet the expectation.
	/// </summary>
	public class Failure : ConstraintResult
	{
		/// <summary>
		///     Initializes a new instance of <see cref="ConstraintResult.Failure" />.
		/// </summary>
		public Failure(
			string expectationText,
			string resultText,
			FurtherProcessing furtherProcessingStrategy = FurtherProcessing.Continue)
			: base(
				Outcome.Failure,
				expectationText,
				furtherProcessingStrategy)
		{
			ResultText = resultText;
		}

		/// <summary>
		///     A human-readable representation of the reason for the failure.
		/// </summary>
		public string ResultText { get; }

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(ResultText.Indent(indentation, false));
		}

		/// <inheritdoc cref="ConstraintResult.CombineWith(string, string)" />
		public override ConstraintResult CombineWith(string expectationText, string resultText)
			=> new Failure(expectationText, resultText);

		/// <inheritdoc />
		public override string ToString()
			=> $"FAILED {ExpectationText}";

		/// <inheritdoc />
		internal override ConstraintResult UseValue<T>(T value)
			=> new Failure<T>(value, ExpectationText, ResultText, FurtherProcessingStrategy);

		internal override string GetResultText() => ResultText;
	}

	/// <summary>
	///     The actual value did not meet the expectation and a <see cref="Value" /> is stored for further processing.
	/// </summary>
	public class Failure<T> : Failure
	{
		/// <summary>
		///     Initializes a new instance of <see cref="ConstraintResult.Failure{T}" />.
		/// </summary>
		public Failure(
			T value,
			string expectationText,
			string resultText,
			FurtherProcessing furtherProcessingStrategy = FurtherProcessing.Continue)
			: base(
				expectationText,
				resultText,
				furtherProcessingStrategy)
		{
			Value = value;
		}

		/// <summary>
		///     A value for further processing.
		/// </summary>
		public T Value { get; }

		/// <inheritdoc cref="ConstraintResult.CombineWith(string, string)" />
		public override ConstraintResult CombineWith(string expectationText, string resultText)
			=> new Failure<T>(Value, expectationText, resultText);

		internal override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
			where TValue : default
		{
			if (Value is TValue v)
			{
				value = v;
				return true;
			}

			value = default;
			return Value is null;
		}
	}
}
