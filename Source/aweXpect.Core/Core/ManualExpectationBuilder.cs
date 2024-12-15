﻿using System;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Nodes;
using aweXpect.Core.TimeSystem;

namespace aweXpect.Core;

/// <summary>
///     A manual expectation builder can be used for manually evaluating inner expectations.
/// </summary>
public class ManualExpectationBuilder<TValue>() : ExpectationBuilder("")
{
	private Node? _node;

	/// <summary>
	///     Evaluate if the expectations are met by the <paramref name="value" />.
	/// </summary>
	public async Task<ConstraintResult> IsMetBy(
		TValue value,
		IEvaluationContext context,
		CancellationToken cancellationToken)
	{
		_node ??= GetRootNode();
		return await _node.IsMetBy(value, context, cancellationToken);
	}

	/// <inheritdoc />
	internal override Task<ConstraintResult> IsMet(
		Node rootNode,
		EvaluationContext.EvaluationContext context,
		ITimeSystem timeSystem,
		CancellationToken cancellationToken)
		=> throw new NotSupportedException($"Use {nameof(IsMetBy)} for ManualExpectationBuilder!");
}