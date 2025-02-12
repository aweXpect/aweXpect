using System;
using System.Collections.Generic;
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
		=> new ConstraintResultFailure<T>(inner, failure, value);

	/// <summary>
	///     Creates a new <see cref="ConstraintResult" /> with a context with
	///     <paramref name="title" /> and <paramref name="content" />.
	/// </summary>
	public static ConstraintResult WithContext(this ConstraintResult inner, string title, string content)
		=> new ConstraintResultContextWrapper(inner, [new ConstraintResult.Context(title, content)]);

	/// <summary>
	///     Creates a new <see cref="ConstraintResult" /> with <paramref name="contexts" />.
	/// </summary>
	public static ConstraintResult WithContexts(this ConstraintResult inner,
		params ConstraintResult.Context[] contexts)
		=> new ConstraintResultContextWrapper(inner, contexts);

	private sealed class ConstraintResultWrapper<T> : ConstraintResult
	{
		private readonly T _value;
		private readonly ConstraintResult _inner;

		public ConstraintResultWrapper(ConstraintResult inner, T value)
			: base(inner.Outcome, inner.FurtherProcessingStrategy)
		{
			_inner = inner;
			_value = value;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> _inner.AppendExpectation(stringBuilder, indentation);

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> _inner.AppendResult(stringBuilder, indentation);

		public override IEnumerable<Context> GetContexts()
			=> _inner.GetContexts();

		internal override void UpdateExpectationText(
			Action<StringBuilder>? prependExpectationText = null,
			Action<StringBuilder>? appendExpectationText = null)
			=> _inner.UpdateExpectationText(prependExpectationText, appendExpectationText);

		internal override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_value is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			value = default;
			return _value is null;
		}
	}

	private sealed class ConstraintResultContextWrapper(
		ConstraintResult inner,
		ConstraintResult.Context[] contexts)
		: ConstraintResult(inner.Outcome, inner.FurtherProcessingStrategy)
	{
		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> inner.AppendExpectation(stringBuilder, indentation);

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> inner.AppendResult(stringBuilder, indentation);

		public override IEnumerable<Context> GetContexts()
		{
			foreach (Context c in contexts)
			{
				yield return c;
			}

			foreach (Context c in inner.GetContexts())
			{
				yield return c;
			}
		}

		internal override void UpdateExpectationText(
			Action<StringBuilder>? prependExpectationText = null,
			Action<StringBuilder>? appendExpectationText = null)
			=> inner.UpdateExpectationText(prependExpectationText, appendExpectationText);

		internal override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
			=> inner.TryGetValue(out value);
	}

	private sealed class ConstraintResultFailure<T>(ConstraintResult inner, string failure, T value)
		: ConstraintResult(Outcome.Failure, inner.FurtherProcessingStrategy)
	{
		private readonly T _value = value;

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> inner.AppendExpectation(stringBuilder, indentation);

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(failure.Indent(indentation));

		public override IEnumerable<Context> GetContexts()
			=> inner.GetContexts();

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
			return _value is null;
		}
	}
}
