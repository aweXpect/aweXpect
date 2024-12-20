#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
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
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		StartWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new StartWithConstraint<TItem, object?>(it, doNotPopulateThisValue, expected.ToArray(),
						options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		StartWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			params TItem[] expected)
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new StartWithConstraint<TItem, object?>(it, Formatter.Format(expected), expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>
		StartWith(
			this IThat<IAsyncEnumerable<string>> source,
			IEnumerable<string> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new StartWithConstraint<string, string>(it, doNotPopulateThisValue, expected.ToArray(),
						options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>
		StartWith(
			this IThat<IAsyncEnumerable<string>> source,
			params string[] expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new StartWithConstraint<string, string>(it, Formatter.Format(expected), expected, options)),
			source,
			options);
	}

	private readonly struct StartWithConstraint<TItem, TMatch>(
		string it,
		string expectedExpression,
		TItem[] expected,
		IOptionsEquality<TMatch> options)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
		where TItem : TMatch
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(expected, nameof(expected));

			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was <null>");
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			if (expected.Length == 0)
			{
				return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
			}

			int index = 0;
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				TItem expectedItem = expected[index++];
				if (!options.AreConsideredEqual(item, expectedItem))
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
						$"{it} contained {Formatter.Format(item)} at index {index - 1} instead of {Formatter.Format(expectedItem)}");
				}

				if (expected.Length == index)
				{
					return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
				}
			}


			return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
				$"{it} contained only {index} items and misses {expected.Length - index} items: {Formatter.Format(expected.Skip(index), FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> $"start with {expectedExpression}";
	}
}
#endif
