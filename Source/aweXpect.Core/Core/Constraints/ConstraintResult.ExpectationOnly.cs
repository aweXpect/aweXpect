using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace aweXpect.Core.Constraints;

public abstract partial class ConstraintResult
{
	/// <summary>
	///     A <see cref="ConstraintResult" /> with only an expectation text.
	/// </summary>
	public class ExpectationOnly<T>(
		ExpectationGrammars grammars,
		string? expectation = null,
		string? negatedExpectation = null)
		: ConstraintResult(grammars), IValueConstraint<T>
	{
		/// <inheritdoc />
		public override Outcome Outcome
		{
			get;
			protected set;
		} = Outcome.Success;

		/// <inheritdoc cref="IValueConstraint{TValue}.IsMetBy(TValue)" />
		public ConstraintResult IsMetBy(T actual) => this;

		/// <inheritdoc cref="ConstraintResult.AppendExpectation(StringBuilder, string?)" />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Grammars.IsNegated())
			{
				AppendNegatedExpectation(stringBuilder, indentation);
			}
			else
			{
				AppendNormalExpectation(stringBuilder, indentation);
			}
		}

		/// <summary>
		///     Appends the expectation to the <paramref name="stringBuilder" /> when the <see cref="ExpectationGrammars" /> are
		///     not negated.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (expectation != null)
			{
				stringBuilder.Append(expectation);
			}
		}

		/// <summary>
		///     Appends the expectation to the <paramref name="stringBuilder" /> when the <see cref="ExpectationGrammars" /> are
		///     negated.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (expectation != null)
			{
				stringBuilder.Append(negatedExpectation);
			}
		}

		/// <inheritdoc cref="ConstraintResult.AppendResult(StringBuilder, string?)" />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
		}

		/// <inheritdoc cref="ConstraintResult.TryGetValue{TValue}(out TValue)" />
		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			value = default;
			return false;
		}

		/// <inheritdoc cref="ConstraintResult.Negate()" />
		public override ConstraintResult Negate()
		{
			Grammars = Grammars.Negate();
			return this;
		}
	}
}
