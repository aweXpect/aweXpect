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
