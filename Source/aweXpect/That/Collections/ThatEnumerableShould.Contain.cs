using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Formatting;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that the actual enumerable contains the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		Contain<TItem>(
			this IThat<IEnumerable<TItem>> source,
			TItem expected)
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new ContainConstraint<TItem>(it, expected)),
			source);

	private readonly struct ContainConstraint<TItem>(string it, TItem expected)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem>? materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			foreach (TItem item in materializedEnumerable)
			{
				if (item?.Equals(expected) == true)
				{
					return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
						ToString());
				}
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(materializedEnumerable.Take(10).ToArray())}");
		}

		public override string ToString()
			=> $"contain {Formatter.Format(expected)}";
	}
}
