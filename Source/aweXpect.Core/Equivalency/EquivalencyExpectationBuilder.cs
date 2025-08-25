using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Helpers;
using aweXpect.Core.Nodes;
using aweXpect.Core.TimeSystem;

namespace aweXpect.Equivalency;

internal class EquivalencyExpectationBuilder<T> : EquivalencyExpectationBuilder
{
	private ConstraintResult? _result;

	/// <inheritdoc cref="object.ToString()" />
	public override string ToString()
	{
		StringBuilder sb = new();
		sb.Append("is ");
		Formatter.Format(sb, typeof(T));
		sb.Append(" that ");
		int lengthBefore = sb.Length;
		_result?.AppendExpectation(sb);
		if (sb.Length == lengthBefore)
		{
			// Remove " that ", when no expectations were added
			sb.Length -= 6;
		}

		return sb.ToString();
	}

	public override async Task<ConstraintResult> IsMetBy(
		object? value,
		IEvaluationContext context,
		CancellationToken cancellationToken)
	{
		if (value is T typedValue)
		{
			_result = await GetRootNode().IsMetBy(typedValue, context, cancellationToken);
		}
		else if (value is null)
		{
			T? typedDefault = default;
			_result = new NotMatchingTypesResult(typedDefault,
				await GetRootNode().IsMetBy(typedDefault, context, cancellationToken));
		}
		else
		{
			_result = new NotMatchingTypesResult(value, null);
		}

		return _result;
	}

	private sealed class NotMatchingTypesResult : ConstraintResult
	{
		private readonly ConstraintResult? _inner;
		private readonly object? _value;

		public NotMatchingTypesResult(object? value, ConstraintResult? inner) : base(FurtherProcessingStrategy.Continue)
		{
			_value = value;
			_inner = inner;
			Outcome = Outcome.Failure;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> _inner?.AppendExpectation(stringBuilder, indentation);

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(" was ");
			Formatter.Format(stringBuilder, _value?.GetType());
		}

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_value is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			value = default;
			return false;
		}

		public override ConstraintResult Negate() => this;
	}
}

internal abstract class EquivalencyExpectationBuilder : ExpectationBuilder
{
	/// <summary>
	///     Appends the expectation of the root node to the <paramref name="stringBuilder" />.
	/// </summary>
	public void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		=> GetRootNode().AppendExpectation(stringBuilder, indentation);

	/// <summary>
	///     Evaluate if the expectations are met by the <paramref name="value" />.
	/// </summary>
	public abstract Task<ConstraintResult> IsMetBy(
		object? value,
		IEvaluationContext context,
		CancellationToken cancellationToken);

	/// <inheritdoc />
	internal override Task<ConstraintResult> IsMet(Node rootNode,
		EvaluationContext context,
		ITimeSystem timeSystem,
		TimeSpan? timeout,
		CancellationToken cancellationToken)
		=> throw new NotSupportedException($"Use {nameof(IsMetBy)} for EquivalencyExpectationBuilder!")
			.LogTrace();

	/// <inheritdoc cref="ExpectationBuilder.UpdateContexts(Action{ResultContexts})" />
	public override ExpectationBuilder UpdateContexts(Action<ResultContexts> callback) => this;
}
