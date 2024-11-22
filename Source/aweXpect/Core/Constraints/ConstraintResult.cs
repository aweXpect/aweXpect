using System;
using System.Diagnostics.CodeAnalysis;

namespace Testably.Expectations.Core.Constraints;

/// <summary>
///     The result of the check if an expectation is met.
/// </summary>
public abstract class ConstraintResult
{
	/// <summary>
	///     A human-readable representation of the expectation.
	/// </summary>
	public string ExpectationText { get; }

	/// <summary>
	///     Specifies if further processing of chained constraints should be ignored.
	/// </summary>
	public FurtherProcessing FurtherProcessingStrategy { get; }

	/// <summary>
	///     Initializes a new instance of <see cref="ConstraintResult" />.
	/// </summary>
	protected ConstraintResult(
		string expectationText,
		FurtherProcessing furtherProcessingStrategy)
	{
		ExpectationText = expectationText;
		FurtherProcessingStrategy = furtherProcessingStrategy;
	}

	/// <summary>
	///     Combines the result with the provided <paramref name="expectationText" /> and <paramref name="resultText" />.
	/// </summary>
	public abstract ConstraintResult CombineWith(string expectationText, string resultText);

	/// <summary>
	///     Updates the expectation text of the current <see cref="ConstraintResult" />.
	/// </summary>
	internal abstract ConstraintResult UpdateExpectationText(
		Func<ConstraintResult, string> expectationText);

	internal abstract ConstraintResult UseValue<T>(T value);

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
				expectationText,
				furtherProcessingStrategy)
		{
		}

		/// <inheritdoc cref="ConstraintResult.CombineWith(string, string)" />
		public override ConstraintResult CombineWith(string expectationText, string resultText)
		{
			return new Success(expectationText);
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"SUCCEEDED {ExpectationText}";

		internal virtual bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
		{
			value = default;
			return false;
		}

		/// <inheritdoc />
		internal override ConstraintResult UpdateExpectationText(
			Func<ConstraintResult, string> expectationText)
			=> new Success(expectationText.Invoke(this));

		/// <inheritdoc />
		internal override ConstraintResult UseValue<T>(T value)
		{
			return new Success<T>(value, ExpectationText, FurtherProcessingStrategy);
		}
	}

	/// <summary>
	///     The actual value met the expectation and a <see cref="Value" /> is stored for further processing.
	/// </summary>
	public class Success<T> : Success
	{
		/// <summary>
		///     A value for further processing.
		/// </summary>
		public T Value { get; }

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

		/// <inheritdoc cref="ConstraintResult.CombineWith(string, string)" />
		public override ConstraintResult CombineWith(string expectationText, string resultText)
		{
			return new Success<T>(Value, expectationText);
		}

		internal override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
			where TValue : default
		{
			if (Value is TValue v)
			{
				value = v;
				return true;
			}

			value = default;
			return false;
		}

		/// <inheritdoc />
		internal override ConstraintResult UpdateExpectationText(
			Func<ConstraintResult, string> expectationText)
			=> new Success<T>(Value, expectationText.Invoke(this));
	}

	/// <summary>
	///     The actual value did not meet the expectation.
	/// </summary>
	public class Failure : ConstraintResult
	{
		/// <summary>
		///     A human-readable representation of the reason for the failure.
		/// </summary>
		public string ResultText { get; }

		/// <summary>
		///     Initializes a new instance of <see cref="ConstraintResult.Failure" />.
		/// </summary>
		public Failure(
			string expectationText,
			string resultText,
			FurtherProcessing furtherProcessingStrategy = FurtherProcessing.Continue)
			: base(
				expectationText,
				furtherProcessingStrategy)
		{
			ResultText = resultText;
		}

		/// <inheritdoc cref="ConstraintResult.CombineWith(string, string)" />
		public override ConstraintResult CombineWith(string expectationText, string resultText)
		{
			return new Failure(expectationText, resultText);
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"FAILED {ExpectationText}";

		/// <inheritdoc />
		internal override ConstraintResult UpdateExpectationText(
			Func<ConstraintResult, string> expectationText)
			=> new Failure(expectationText.Invoke(this), ResultText);

		/// <inheritdoc />
		internal override ConstraintResult UseValue<T>(T value)
		{
			return new Failure<T>(value, ExpectationText, ResultText, FurtherProcessingStrategy);
		}
	}

	/// <summary>
	///     The actual value did not meet the expectation and a <see cref="Value" /> is stored for further processing.
	/// </summary>
	public class Failure<T> : Failure
	{
		/// <summary>
		///     A value for further processing.
		/// </summary>
		public T Value { get; }

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

		/// <inheritdoc cref="ConstraintResult.CombineWith(string, string)" />
		public override ConstraintResult CombineWith(string expectationText, string resultText)
		{
			return new Failure<T>(Value, expectationText, resultText);
		}

		/// <inheritdoc />
		internal override ConstraintResult UpdateExpectationText(
			Func<ConstraintResult, string> expectationText)
			=> new Failure<T>(Value, expectationText.Invoke(this), ResultText);
	}
}
