using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Constraints;

/// <summary>
///     The result of the check if an expectation is met.
/// </summary>
public abstract class ConstraintResult
{
	private readonly string? _expectationText;

	private Action<StringBuilder>? _appendExpectationText;
	private Action<StringBuilder>? _prependExpectationText;

	/// <summary>
	///     Initializes a new instance of <see cref="ConstraintResult" />.
	/// </summary>
	private ConstraintResult(
		Outcome outcome,
		string expectationText,
		FurtherProcessingStrategy furtherProcessingStrategy)
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
	///     Appends the expectation text to the <paramref name="stringBuilder" />.
	/// </summary>
	public virtual void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
	{
		_prependExpectationText?.Invoke(stringBuilder);
		stringBuilder.Append(_expectationText.Indent(indentation, false));
		_appendExpectationText?.Invoke(stringBuilder);
	}

	/// <summary>
	///     Appends the result text to the <paramref name="stringBuilder" />.
	/// </summary>
	public virtual void AppendResult(StringBuilder stringBuilder, string? indentation = null)
	{
		// Do nothing
	}

	/// <summary>
	///     Gets the contexts for the result.
	/// </summary>
	public virtual IEnumerable<Context> GetContexts()
	{
		yield break;
	}

	internal virtual string GetResultText()
	{
		StringBuilder sb = new();
		AppendResult(sb);
		return sb.ToString();
	}

	internal virtual bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
	{
		value = default;
		return false;
	}

	/// <summary>
	///     Updates the expectation text of the current <see cref="ConstraintResult" />.
	/// </summary>
	internal virtual void UpdateExpectationText(
		Action<StringBuilder>? prependExpectationText = null,
		Action<StringBuilder>? appendExpectationText = null)
	{
		_prependExpectationText = prependExpectationText ?? _prependExpectationText;
		_appendExpectationText = appendExpectationText ?? _appendExpectationText;
	}

	/// <summary>
	///     A result context that is appended to a result error.
	/// </summary>
	public record Context(string Title, string Content)
	{
		/// <summary>
		///     The comparer for contexts that only considers the title.
		/// </summary>
		public static IEqualityComparer<Context> Comparer { get; } = new ContextComparer();

		private sealed class ContextComparer : IEqualityComparer<Context>
		{
			public bool Equals(Context? x, Context? y)
			{
				if (x is null || y is null)
				{
					return false;
				}

				return x.Title == y.Title;
			}

			public int GetHashCode(Context obj) => obj.Title.GetHashCode();
		}
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
		private readonly string _resultText;

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
			_resultText = resultText;
		}

		/// <inheritdoc />
		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(_resultText.Indent(indentation, false));

		internal override string GetResultText() => _resultText;
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
