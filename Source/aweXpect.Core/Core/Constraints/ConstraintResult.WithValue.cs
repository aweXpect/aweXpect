using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace aweXpect.Core.Constraints;

public abstract partial class ConstraintResult
{
	/// <summary>
	///     A typed <see cref="ConstraintResult" />.
	/// </summary>
	public abstract class WithValue<T>(ExpectationGrammars grammars) : ConstraintResult
	{
		private bool _isNegated;
		private Outcome _outcome = Outcome.Undecided;

		/// <summary>
		///     The actual value.
		/// </summary>
		protected T? Actual { get; set; }

		/// <inheritdoc />
		public override Outcome Outcome
		{
			get => (_outcome, _isNegated) switch
			{
				(Outcome.Failure, true) => Outcome.Success,
				(Outcome.Success, true) => Outcome.Failure,
				(_,_) => _outcome,
			};
			protected set => _outcome = value;
		}

		/// <summary>
		///     Appends the expectation to the <paramref name="stringBuilder" /> when the <see cref="ExpectationGrammars" /> are
		///     not negated.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null);

		/// <summary>
		///     Appends the result to the <paramref name="stringBuilder" /> when the <see cref="ExpectationGrammars" /> are not
		///     negated.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null);

		/// <summary>
		///     Appends the expectation to the <paramref name="stringBuilder" /> when the <see cref="ExpectationGrammars" /> are
		///     negated.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null);

		/// <summary>
		///     Appends the result to the <paramref name="stringBuilder" /> when the <see cref="ExpectationGrammars" /> are
		///     negated.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null);

		/// <inheritdoc cref="ConstraintResult.AppendExpectation(StringBuilder, string?)" />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (grammars.IsNegated())
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
			if (grammars.IsNegated())
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

		/// <inheritdoc cref="ConstraintResult.Negate()" />
		public override ConstraintResult Negate()
		{
			_isNegated = !_isNegated;
			return this;
		}
	}
}
