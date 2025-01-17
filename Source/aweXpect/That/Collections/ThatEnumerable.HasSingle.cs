using System.Collections.Generic;
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
		this IExpectSubject<IEnumerable<TItem>> source)
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint(it => new HaveSingleConstraint<TItem>(it)),
			f => f.FirstOrDefault()
		);

	private readonly struct HaveSingleConstraint<TItem>(string it) : IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem> materialized = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			TItem? singleItem = default;
			int count = 0;

			foreach (TItem item in materialized)
			{
				singleItem = item;
				if (++count > 1)
				{
					break;
				}
			}

			switch (count)
			{
				case 1:
					return new ConstraintResult.Success<TItem>(singleItem!, ToString());
				case 0:
					return new ConstraintResult.Failure<IEnumerable<TItem>>(materialized, ToString(),
						$"{it} was empty");
				default:
					return new ConstraintResult.Failure<IEnumerable<TItem>>(materialized, ToString(),
						$"{it} contained more than one item");
			}
		}

		public override string ToString() => "have a single item";
	}
}
