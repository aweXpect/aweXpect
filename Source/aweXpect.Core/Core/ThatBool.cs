﻿using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.Nodes;
using aweXpect.Core.TimeSystem;
using aweXpect.Results;

namespace aweXpect.Core;

/// <summary>
///     Wraps the <see cref="ExpectationBuilder" /> for a bool.
/// </summary>
[DebuggerDisplay("ThatBool: {ExpectationBuilder}")]
public class ThatBool : ExpectationResult<bool>, IExpectThat<bool>
{
	/// <inheritdoc cref="ThatBool" />
	public ThatBool(ExpectationBuilder expectationBuilder) : this(
		new WithDefaultExpectationBuilderProxy(expectationBuilder))
	{
	}

	private ThatBool(WithDefaultExpectationBuilderProxy expectationBuilder) : base(expectationBuilder)
	{
		ExpectationBuilder = expectationBuilder;
	}

	/// <inheritdoc cref="IExpectThat{T}.ExpectationBuilder" />
	public ExpectationBuilder ExpectationBuilder { get; }

	private class WithDefaultExpectationBuilderProxy(ExpectationBuilder inner)
		: ExpectationBuilder(inner.Subject, inner.ExpectationGrammars)
	{
		public override ExpectationBuilder UpdateContexts(Action<ResultContexts> callback)
			=> inner.UpdateContexts(callback);

		internal override Task<ConstraintResult> IsMet(Node rootNode, EvaluationContext.EvaluationContext context,
			ITimeSystem timeSystem, TimeSpan? timeout,
			CancellationToken cancellationToken)
		{
			if (rootNode is ExpectationNode expectationNode && expectationNode.IsEmpty())
			{
				rootNode.AddConstraint(new IsTrueConstraint(inner.ExpectationGrammars));
			}

			return inner.IsMet(rootNode, context, timeSystem, timeout, cancellationToken);
		}

		private sealed class IsTrueConstraint(ExpectationGrammars grammars)
			: ConstraintResult.WithEqualToValue<bool>("it", grammars, false),
				IValueConstraint<bool>
		{
			public ConstraintResult IsMetBy(bool actual)
			{
				Actual = actual;
				Outcome = true.Equals(actual) ? Outcome.Success : Outcome.Failure;
				return this;
			}

			protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			{
				stringBuilder.Append("is ");
				Formatter.Format(stringBuilder, true, FormattingOptions.Indented(indentation));
			}

			protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			{
				stringBuilder.Append(It);
				stringBuilder.Append(" was ");
				Formatter.Format(stringBuilder, Actual, FormattingOptions.Indented(indentation));
			}

			protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			{
				stringBuilder.Append("is not ");
				Formatter.Format(stringBuilder, true, FormattingOptions.Indented(indentation));
			}

			protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			{
				stringBuilder.Append(It);
				stringBuilder.Append(" was");
			}
		}
	}
}
