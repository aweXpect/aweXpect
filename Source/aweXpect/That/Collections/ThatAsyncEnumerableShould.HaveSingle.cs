using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Results;
// ReSharper disable PossibleMultipleEnumeration

#if NET6_0_OR_GREATER
namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that the collection contains exactly one element.
	/// </summary>
	public static SingleItemResult<IAsyncEnumerable<TItem>, TItem>.Async HaveSingle<TItem>(
		this IThat<IAsyncEnumerable<TItem>> source)
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new HaveSingleConstraint<TItem>(it)),
			async f =>
			{
				await using IAsyncEnumerator<TItem> enumerator = f.GetAsyncEnumerator();
				return await enumerator.MoveNextAsync() ? enumerator.Current : default;
			});

	private readonly struct HaveSingleConstraint<TItem>(string it) : IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materialized =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			TItem? singleItem = default;
			int count = 0;

			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
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
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(materialized, ToString(),
						$"{it} was empty");
				default:
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(materialized, ToString(),
						$"{it} contained more than one item");
			}
		}

		public override string ToString() => "have a single item";
	}
}
#endif
