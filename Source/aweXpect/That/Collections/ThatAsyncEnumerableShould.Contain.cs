﻿#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that the actual enumerable contains the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectCountResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		Contain<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			TItem expected)
	{
		Quantifier quantifier = new();
		ObjectEqualityOptions options = new();
		return new ObjectCountResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source.ExpectationBuilder
				.AddConstraint(it => new ContainConstraint<TItem>(
					it,
					q => $"contain {Formatter.Format(expected)} {q}",
					a => options.AreConsideredEqual(a, expected, it).AreConsideredEqual,
					quantifier)),
			source,
			quantifier,
			options);
	}

	/// <summary>
	///     Verifies that the actual enumerable contains an item that satisfies the <paramref name="predicate" />.
	/// </summary>
	public static CountResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		Contain<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
	{
		Quantifier quantifier = new();
		return new CountResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source.ExpectationBuilder
				.AddConstraint(it
					=> new ContainConstraint<TItem>(
						it,
						q => $"contain item matching {doNotPopulateThisValue} {q}",
						predicate,
						quantifier)),
			source,
			quantifier);
	}

	/// <summary>
	///     Verifies that the actual enumerable does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		NotContain<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			TItem unexpected)
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
				.AddConstraint(it => new NotContainConstraint<TItem>(it, unexpected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the actual enumerable contains no item that satisfies the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		NotContain<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new NotContainPredicateConstraint<TItem>(it, predicate, doNotPopulateThisValue)),
			source);

	private readonly struct ContainConstraint<TItem>(
		string it,
		Func<Quantifier, string> expectationText,
		Func<TItem, bool> predicate,
		Quantifier quantifier)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			List<TItem> items = new(CollectionFormatCount + 1);
			int count = 0;
			bool isFailed = false;
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				if (items.Count <= CollectionFormatCount)
				{
					items.Add(item);
				}

				if (predicate(item))
				{
					count++;
					bool? check = quantifier.Check(count, false);
					if (check == false)
					{
						isFailed = true;
					}

					if (check == true)
					{
						return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
							ToString());
					}
				}

				if (items.Count > CollectionFormatCount && isFailed)
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
						$"{it} contained it at least {count} times in {Formatter.Format(items.ToArray())}");
				}
			}

			if (quantifier.Check(count, true) == true)
			{
				return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
					ToString());
			}

			return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
				$"{it} contained it {count} times in {Formatter.Format(items.ToArray())}");
		}

		public override string ToString()
			=> expectationText(quantifier);
	}

	private readonly struct NotContainConstraint<TItem>(string it, TItem unexpected, ObjectEqualityOptions options)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				if (options.AreConsideredEqual(item, unexpected, it).AreConsideredEqual)
				{
					return new ConstraintResult.Failure(ToString(),
						$"{it} did");
				}
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		public override string ToString()
			=> $"not contain {Formatter.Format(unexpected)}";
	}

	private readonly struct NotContainPredicateConstraint<TItem>(
		string it,
		Func<TItem, bool> predicate,
		string predicateExpression)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				if (predicate(item))
				{
					return new ConstraintResult.Failure(ToString(),
						$"{it} did");
				}
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		public override string ToString()
			=> $"not contain item matching {predicateExpression}";
	}
}
#endif
