﻿#if NET8_0_OR_GREATER
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

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that the collection is empty.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		BeEmpty<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source)
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new BeEmptyConstraint<TItem>(it)),
			source);

	/// <summary>
	///     Verifies that the collection is not empty.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		NotBeEmpty<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source)
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new NotBeEmptyConstraint<TItem>(it)),
			source);

	private readonly struct BeEmptyConstraint<TItem>(string it)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem> actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual!, ToString(), $"{it} was <null>");
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			await using IAsyncEnumerator<TItem> enumerator =
				materializedEnumerable.GetAsyncEnumerator(cancellationToken);
			if (await enumerator.MoveNextAsync())
			{
				List<TItem> items = new(Customize.Formatting.MaximumNumberOfCollectionItems + 1)
				{
					enumerator.Current
				};
				while (await enumerator.MoveNextAsync())
				{
					items.Add(enumerator.Current);
					if (items.Count > Customize.Formatting.MaximumNumberOfCollectionItems)
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
			=> "be empty";
	}

	private readonly struct NotBeEmptyConstraint<TItem>(string it)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem> actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual!, ToString(), $"{it} was <null>");
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
			=> "not be empty";
	}
}
#endif
