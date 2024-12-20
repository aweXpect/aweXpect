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
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		NotStartWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new NotStartWithConstraint<TItem, object?>(it, doNotPopulateThisValue, unexpected.ToArray(),
						options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		NotStartWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			params TItem[] unexpected)
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new NotStartWithConstraint<TItem, object?>(it, Formatter.Format(unexpected), unexpected,
						options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>
		NotStartWith(
			this IThat<IAsyncEnumerable<string>> source,
			IEnumerable<string> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new NotStartWithConstraint<string, string>(it, doNotPopulateThisValue, unexpected.ToArray(),
						options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>
		NotStartWith(
			this IThat<IAsyncEnumerable<string>> source,
			params string[] unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new NotStartWithConstraint<string, string>(it, Formatter.Format(unexpected), unexpected,
						options)),
			source,
			options);
	}

	private readonly struct NotStartWithConstraint<TItem, TMatch> : IAsyncContextConstraint<IAsyncEnumerable<TItem>>
		where TItem : TMatch
	{
		private readonly string _it;
		private readonly string _unexpectedExpression;
		private readonly TItem[] _unexpected;
		private readonly IOptionsEquality<TMatch> _options;

		public NotStartWithConstraint(string it,
			string unexpectedExpression,
			TItem[] unexpected,
			IOptionsEquality<TMatch> options)
		{
			ArgumentNullException.ThrowIfNull(unexpected);
			_it = it;
			_unexpectedExpression = unexpectedExpression;
			_unexpected = unexpected;
			_options = options;
		}

		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{_it} was <null>");
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			if (_unexpected.Length == 0)
			{
				List<TItem> displayValues = new(Customize.Formatting.MaximumNumberOfCollectionItems + 1);
				await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
				{
					displayValues.Add(item);
					if (displayValues.Count == Customize.Formatting.MaximumNumberOfCollectionItems)
					{
						break;
					}
				}

				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
					$"{_it} was {Formatter.Format(displayValues, FormattingOptions.MultipleLines)}");
			}

			int index = 0;
			List<TItem> foundValues = new();
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				foundValues.Add(item);
				TItem unexpectedItem = _unexpected[index++];
				if (!_options.AreConsideredEqual(item, unexpectedItem))
				{
					return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
				}

				if (_unexpected.Length == index)
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
						$"{_it} did start with {Formatter.Format(foundValues, FormattingOptions.MultipleLines)}");
				}
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
		}

		public override string ToString()
			=> $"not start with {_unexpectedExpression}{_options}";
	}
}
#endif
