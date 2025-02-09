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

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
		StartsWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new StartsWithConstraint<TItem, TItem>(it, doNotPopulateThisValue, expected.ToArray(),
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
		StartsWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			params TItem[] expected)
	{
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new StartsWithConstraint<TItem, TItem>(it, Formatter.Format(expected), expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>
		StartsWith(
			this IThat<IAsyncEnumerable<string?>?> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new StartsWithConstraint<string?, string?>(it, doNotPopulateThisValue, expected.ToArray(),
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>
		StartsWith(
			this IThat<IAsyncEnumerable<string?>?> source,
			params string[] expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new StartsWithConstraint<string, string>(it, Formatter.Format(expected), expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
		DoesNotStartWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new DoesNotStartWithConstraint<TItem, TItem>(it, doNotPopulateThisValue, unexpected.ToArray(),
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
		DoesNotStartWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			params TItem[] unexpected)
	{
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new DoesNotStartWithConstraint<TItem, TItem>(it, Formatter.Format(unexpected), unexpected,
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>
		DoesNotStartWith(
			this IThat<IAsyncEnumerable<string?>?> source,
			IEnumerable<string?> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new DoesNotStartWithConstraint<string?, string?>(it, doNotPopulateThisValue, unexpected.ToArray(),
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>
		DoesNotStartWith(
			this IThat<IAsyncEnumerable<string?>?> source,
			params string[] unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new DoesNotStartWithConstraint<string?, string?>(it, Formatter.Format(unexpected), unexpected,
					options)),
			source,
			options);
	}

	private readonly struct StartsWithConstraint<TItem, TMatch> : IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
		where TItem : TMatch
	{
		private readonly string _it;
		private readonly string _expectedExpression;
		private readonly TItem[] _expected;
		private readonly IOptionsEquality<TMatch> _options;

		public StartsWithConstraint(string it,
			string expectedExpression,
			TItem[] expected,
			IOptionsEquality<TMatch> options)
		{
			ArgumentNullException.ThrowIfNull(expected);
			_it = it;
			_expectedExpression = expectedExpression;
			_expected = expected;
			_options = options;
		}

		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem>? actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{_it} was <null>");
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			if (_expected.Length == 0)
			{
				return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
			}

			int index = 0;
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				TItem expectedItem = _expected[index++];
				if (!_options.AreConsideredEqual(item, expectedItem))
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
						$"{_it} contained {Formatter.Format(item)} at index {index - 1} instead of {Formatter.Format(expectedItem)}");
				}

				if (_expected.Length == index)
				{
					return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
				}
			}


			return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
				$"{_it} contained only {index} items and misses {_expected.Length - index} items: {Formatter.Format(_expected.Skip(index), FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> $"starts with {_expectedExpression}{_options}";
	}

	private readonly struct DoesNotStartWithConstraint<TItem, TMatch> : IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
		where TItem : TMatch
	{
		private readonly string _it;
		private readonly string _unexpectedExpression;
		private readonly TItem[] _unexpected;
		private readonly IOptionsEquality<TMatch> _options;

		public DoesNotStartWithConstraint(string it,
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

		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem>? actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{_it} was <null>");
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			if (_unexpected.Length == 0)
			{
				int maximumNumberOfCollectionItems =
					Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
				List<TItem> displayValues = new(maximumNumberOfCollectionItems + 1);
				await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
				{
					displayValues.Add(item);
					if (displayValues.Count == maximumNumberOfCollectionItems)
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
			=> $"does not start with {_unexpectedExpression}{_options}";
	}
}
#endif
