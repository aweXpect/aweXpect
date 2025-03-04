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
	///     The actual value met the expectation.
	/// </summary>
	public class Success : TextBasedConstraintResult
	{
		/// <summary>
		///     The result text.
		/// </summary>
		private readonly Func<string>? _resultText;

		/// <summary>
		///     Initializes a new instance of <see cref="ConstraintResult.Success" />.
		/// </summary>
		public Success(
			string expectationText,
			Func<string>? resultText = null,
			FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
			: base(
				Outcome.Success,
				expectationText,
				furtherProcessingStrategy)
		{
			_resultText = resultText;
		}

		/// <inheritdoc />
		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_resultText != null)
			{
				stringBuilder.Append(_resultText().Indent(indentation, false));
			}
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
			Func<string>? resultText = null,
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
