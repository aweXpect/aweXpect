using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Constraints;

/// <summary>
///     The result of the check if an expectation is met.
/// </summary>
public abstract class ConstraintResult
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
	///     The outcome of the <see cref="ConstraintResult" />.
	/// </summary>
	public Outcome Outcome { get; }

	/// <summary>
	///     Specifies if further processing of chained constraints should be ignored.
	/// </summary>
	public FurtherProcessingStrategy FurtherProcessingStrategy { get; }

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

	/// <summary>
	///     The actual value met the expectation.
	/// </summary>
	public class Success : TextBasedConstraintResult
	{
		/// <summary>
		///     Initializes a new instance of <see cref="ConstraintResult.Success" />.
		/// </summary>
		public Success(
			string expectationText,
			FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
			: base(
				Outcome.Success,
				expectationText,
				furtherProcessingStrategy)
		{
		}
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
			FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
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

		/// <inheritdoc />
		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
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
	public class Failure : TextBasedConstraintResult
	{
		/// <summary>
		///     Initializes a new instance of <see cref="ConstraintResult.Failure" />.
		/// </summary>
		public Failure(
			string expectationText,
			string resultText,
			FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
			: base(
				Outcome.Failure,
				expectationText,
				furtherProcessingStrategy)
		{
			ResultText = resultText;
		}

		/// <summary>
		///     The result text.
		/// </summary>
		public string ResultText { get; }

		/// <inheritdoc />
		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(ResultText.Indent(indentation, false));
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
			FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
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

		/// <inheritdoc />
		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
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
