using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Constraints;

/// <summary>
///     Extensions methods on <see cref="ConstraintResult" />
/// </summary>
public static class ConstraintResultExtensions
{
	/// <summary>
	///     Creates a new <see cref="ConstraintResult" /> from the <paramref name="inner" /> using the given
	///     <paramref name="value" />.
	/// </summary>
	public static ConstraintResult UseValue<T>(this ConstraintResult inner, T value)
		=> new ConstraintResultWrapper<T>(inner, value);

	/// <summary>
	///     Creates a new <see cref="ConstraintResult" /> with <see cref="Outcome.Failure" /> from
	///     the <paramref name="inner" /> using the given <paramref name="value" />.
	/// </summary>
	public static ConstraintResult Fail<T>(this ConstraintResult inner, string failure, T value)
		=> new ConstraintResultFailure<T>(inner, failure, true, value);

	private class ConstraintResultWrapper<T>(ConstraintResult inner, T value)
		: ConstraintResult(inner.Outcome, inner.FurtherProcessingStrategy)
	{
		private readonly T _value = value;

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> inner.AppendExpectation(stringBuilder, indentation);

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> inner.AppendResult(stringBuilder, indentation);

		internal override void UpdateExpectationText(
			Action<StringBuilder>? prependExpectationText = null,
			Action<StringBuilder>? appendExpectationText = null)
			=> inner.UpdateExpectationText(prependExpectationText, appendExpectationText);

		internal override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_value is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			value = default;
			return false;
		}
	}

	private class ConstraintResultFailure<T>(ConstraintResult inner, string failure, bool useValue, T value)
		: ConstraintResult(Outcome.Failure, inner.FurtherProcessingStrategy)
	{
		private readonly T _value = value;

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> inner.AppendExpectation(stringBuilder, indentation);

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(failure.Indent(indentation));

		internal override void UpdateExpectationText(
			Action<StringBuilder>? prependExpectationText = null,
			Action<StringBuilder>? appendExpectationText = null)
			=> inner.UpdateExpectationText(prependExpectationText, appendExpectationText);

		internal override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (!useValue)
			{
				return inner.TryGetValue(out value);
			}

			if (_value is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			value = default;
			return false;
		}
	}
}
