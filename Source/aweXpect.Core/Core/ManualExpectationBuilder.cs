﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Helpers;
using aweXpect.Core.Nodes;
using aweXpect.Core.TimeSystem;

namespace aweXpect.Core;

/// <summary>
///     A manual expectation builder can be used for manually evaluating inner expectations.
/// </summary>
public class ManualExpectationBuilder<TValue>(
	ExpectationBuilder? inner,
	ExpectationGrammars grammars = ExpectationGrammars.None)
	: ExpectationBuilder("", grammars), IEqualityComparer<ManualExpectationBuilder<TValue>>
{
	/// <inheritdoc cref="IEqualityComparer{T}.Equals(T, T)" />
	public bool Equals(ManualExpectationBuilder<TValue>? x, ManualExpectationBuilder<TValue>? y)
	{
		if (x is null && y is null)
		{
			return true;
		}

		if (x is null || y is null)
		{
			return false;
		}

		return x.Equals(y);
	}

	/// <inheritdoc cref="IEqualityComparer{T}.GetHashCode(T)" />
	public int GetHashCode(ManualExpectationBuilder<TValue> obj)
		=> obj.GetHashCode();

	/// <inheritdoc cref="object.Equals(object?)" />
	public override bool Equals(object? obj) => obj is ManualExpectationBuilder<TValue> other && Equals(other);

	/// <summary>
	///     Determines whether the <paramref name="other" /> object is equal to the current object.
	/// </summary>
	protected virtual bool Equals(ManualExpectationBuilder<TValue> other) => GetRootNode().Equals(other.GetRootNode());

	/// <inheritdoc cref="object.GetHashCode()" />
	public override int GetHashCode() => GetRootNode().GetHashCode();

	/// <summary>
	///     Appends the expectation of the root node to the <paramref name="stringBuilder" />.
	/// </summary>
	public void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		=> GetRootNode().AppendExpectation(stringBuilder, indentation);

	/// <summary>
	///     Evaluate if the expectations are met by the <paramref name="value" />.
	/// </summary>
	public async Task<ConstraintResult> IsMetBy(
		TValue value,
		IEvaluationContext context,
		CancellationToken cancellationToken)
		=> await GetRootNode().IsMetBy(value, context, cancellationToken);

	/// <inheritdoc />
	internal override Task<ConstraintResult> IsMet(Node rootNode,
		EvaluationContext.EvaluationContext context,
		ITimeSystem timeSystem,
		TimeSpan? timeout,
		CancellationToken cancellationToken)
		=> throw new NotSupportedException($"Use {nameof(IsMetBy)} for ManualExpectationBuilder!")
			.LogTrace();

	/// <inheritdoc cref="ExpectationBuilder.UpdateContexts(Action{ResultContexts})" />
	public override ExpectationBuilder UpdateContexts(Action<ResultContexts> callback)
	{
		inner?.UpdateContexts(callback);
		base.UpdateContexts(callback);
		return this;
	}

	/// <inheritdoc cref="object.ToString()" />
	public override string ToString()
	{
		StringBuilder sb = new();
		sb.Append("it ");
		AppendExpectation(sb);
		return sb.ToString();
	}
}
