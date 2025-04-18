using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection contains exactly one item.
	/// </summary>
	public static SingleItemResult<IEnumerable<TItem>, TItem> HasSingle<TItem>(
		this IThat<IEnumerable<TItem>?> source)
	{
		PredicateOptions<TItem> options = new();
		return new SingleItemResult<IEnumerable<TItem>, TItem>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) => new HasSingleConstraint<TItem>(it, grammars, options)),
			options,
			f => f.FirstOrDefault(item => options.Matches(item))
		);
	}

	private sealed class HasSingleConstraint<TItem>(
		string it,
		ExpectationGrammars grammars,
		PredicateOptions<TItem> options)
		: ConstraintResult.WithValue<TItem?>(grammars),
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
				if (!options.Matches(item))
				{
					continue;
				}

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
			=> stringBuilder.Append("has a single item").Append(options.GetDescription());

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual is null)
			{
				stringBuilder.ItWasNull(it);
			}
			else if (_count == 0)
			{
				stringBuilder.Append(it).Append(" was empty");
			}
			else
			{
				stringBuilder.Append(it).Append(" contained more than one item");
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("does not have a single item").Append(options.GetDescription());

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual is null)
			{
				stringBuilder.ItWasNull(it);
			}
			else
			{
				stringBuilder.Append(it).Append(" did");
			}
		}
	}
}
