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
	///     Creates a new <see cref="ConstraintResult" /> where the expectation is prepended with the
	///     <paramref name="prefix" />
	/// </summary>
	public static ConstraintResult PrependExpectationText(this ConstraintResult inner, Action<StringBuilder>? prefix)
		=> new ConstraintResultExpectationWrapper(inner, prefix, null);

	/// <summary>
	///     Creates a new <see cref="ConstraintResult" /> where the expectation is appended with the <paramref name="suffix" />
	/// </summary>
	public static ConstraintResult AppendExpectationText(this ConstraintResult inner, Action<StringBuilder>? suffix)
		=> new ConstraintResultExpectationWrapper(inner, null, suffix);

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

	/// <summary>
	///     Checks if the two <see cref="ConstraintResult" />s have the same result text.
	/// </summary>
	public static bool HasSameResultTextAs(this ConstraintResult left, ConstraintResult right)
		=> left.GetResultText() == right.GetResultText();

	private static string GetResultText(this ConstraintResult result)
	{
		if (result is ConstraintResult.Failure failure)
		{
			return failure.ResultText;
		}

		StringBuilder sb = new();
		result.AppendResult(sb);
		return sb.ToString();
	}

	private sealed class ConstraintResultWrapper<T> : ConstraintResult
	{
		private readonly ConstraintResult _inner;
		private readonly T _value;

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

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
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

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
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

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
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

	private sealed class ConstraintResultExpectationWrapper(
		ConstraintResult inner,
		Action<StringBuilder>? prefix,
		Action<StringBuilder>? suffix)
		: ConstraintResult(inner.Outcome, inner.FurtherProcessingStrategy)
	{
		public override IEnumerable<Context> GetContexts()
			=> inner.GetContexts();

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
			=> inner.TryGetValue(out value);

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			prefix?.Invoke(stringBuilder);
			inner.AppendExpectation(stringBuilder, indentation);
			suffix?.Invoke(stringBuilder);
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> inner.AppendResult(stringBuilder, indentation);
	}
}
