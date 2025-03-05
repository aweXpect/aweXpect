using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Constraints;

/// <summary>
///     The result of the check if an expectation is met.
/// </summary>
public abstract partial class ConstraintResult
{
	/// <summary>
	///     Initializes a new instance of <see cref="ConstraintResult" />.
	/// </summary>
	protected ConstraintResult(FurtherProcessingStrategy furtherProcessingStrategy)
	{
		FurtherProcessingStrategy = furtherProcessingStrategy;
	}

	/// <summary>
	///     Initializes a new instance of <see cref="ConstraintResult" />.
	/// </summary>
	protected ConstraintResult()
	{
		FurtherProcessingStrategy = FurtherProcessingStrategy.Continue;
	}

	/// <summary>
	///     The outcome of the <see cref="ConstraintResult" />.
	/// </summary>
	public virtual Outcome Outcome { get; protected set; } = Outcome.Undecided;

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
	///     Negate the current <see cref="ConstraintResult" />.
	/// </summary>
	public abstract ConstraintResult Negate();

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
			: base(furtherProcessingStrategy)
		{
			Outcome = outcome;
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

		/// <inheritdoc />
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
