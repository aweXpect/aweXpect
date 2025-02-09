#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that the collection is empty.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>
		IsEmpty<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new IsEmptyConstraint<TItem>(it)),
			source);

	/// <summary>
	///     Verifies that the collection is not empty.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>
		IsNotEmpty<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NotIsEmptyConstraint<TItem>(it)),
			source);

	private readonly struct IsEmptyConstraint<TItem>(string it)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem>? actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			await using IAsyncEnumerator<TItem> enumerator =
				materializedEnumerable.GetAsyncEnumerator(cancellationToken);
			if (await enumerator.MoveNextAsync())
			{
				int maximumNumberOfCollectionItems =
					Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
				List<TItem> items = new(maximumNumberOfCollectionItems + 1)
				{
					enumerator.Current
				};
				while (await enumerator.MoveNextAsync())
				{
					items.Add(enumerator.Current);
					if (items.Count > maximumNumberOfCollectionItems)
					{
						break;
					}
				}

				return new ConstraintResult.Failure(ToString(),
					$"{it} was {Formatter.Format(items, FormattingOptions.MultipleLines)}");
			}

			if (cancellationToken.IsCancellationRequested)
			{
				return new ConstraintResult.Failure(ToString(),
					$"could not evaluate {it}, because it was already cancelled");
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		public override string ToString()
			=> "is empty";
	}

	private readonly struct NotIsEmptyConstraint<TItem>(string it)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem>? actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			await using IAsyncEnumerator<TItem> enumerator =
				materializedEnumerable.GetAsyncEnumerator(cancellationToken);
			if (await enumerator.MoveNextAsync())
			{
				return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
					ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was");
		}

		public override string ToString()
			=> "is not empty";
	}
}
#endif
