using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection contains exactly one element.
	/// </summary>
	public static SingleItemResult<IEnumerable<TItem>, TItem> HasSingle<TItem>(
		this IThat<IEnumerable<TItem>?> source)
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, grammars) => new HaveSingleConstraint<TItem>(it, grammars)),
			f => f.FirstOrDefault()
		);

	private sealed class HaveSingleConstraint<TItem>(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithNotNullValue<TItem?>(it, grammars),
			IContextConstraint<IEnumerable<TItem>?>
	{
		private IEnumerable<TItem>? _actual;
		private int _count;

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			_actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IEnumerable<TItem> materialized = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			_count = 0;

			foreach (TItem item in materialized)
			{
				Actual = item;
				if (++_count > 1)
				{
					break;
				}
			}

			Outcome = _count == 1 ? Outcome.Success : Outcome.Failure;
			return this;
		}

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_actual is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			return base.TryGetValue(out value);
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("has a single item");

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_count == 0)
			{
				stringBuilder.Append(It).Append(" was empty");
			}
			else
			{
				stringBuilder.Append(It).Append(" contained more than one item");
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("does not have a single item");

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(It).Append(" did");
	}
}
