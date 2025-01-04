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
	///     Verifies that the collection ends with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		EndWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new EndWithConstraint<TItem, object?>(it, doNotPopulateThisValue, expected.ToArray(),
						options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection ends with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		EndWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			params TItem[] expected)
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new EndWithConstraint<TItem, object?>(it, Formatter.Format(expected), expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection ends with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>
		EndWith(
			this IThat<IAsyncEnumerable<string>> source,
			IEnumerable<string> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new EndWithConstraint<string, string>(it, doNotPopulateThisValue, expected.ToArray(),
						options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection ends with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>
		EndWith(
			this IThat<IAsyncEnumerable<string>> source,
			params string[] expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new EndWithConstraint<string, string>(it, Formatter.Format(expected), expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not end with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		NotEndWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new NotEndWithConstraint<TItem, object?>(it, doNotPopulateThisValue, unexpected.ToArray(),
						options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not end with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		NotEndWith<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			params TItem[] unexpected)
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new NotEndWithConstraint<TItem, object?>(it, Formatter.Format(unexpected), unexpected,
						options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not end with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>
		NotEndWith(
			this IThat<IAsyncEnumerable<string>> source,
			IEnumerable<string> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new NotEndWithConstraint<string, string>(it, doNotPopulateThisValue, unexpected.ToArray(),
						options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not end with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>
		NotEndWith(
			this IThat<IAsyncEnumerable<string>> source,
			params string[] unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new NotEndWithConstraint<string, string>(it, Formatter.Format(unexpected), unexpected,
						options)),
			source,
			options);
	}

	private readonly struct EndWithConstraint<TItem, TMatch> : IAsyncContextConstraint<IAsyncEnumerable<TItem>>
		where TItem : TMatch
	{
		private readonly string _it;
		private readonly string _expectedExpression;
		private readonly TItem[] _expected;
		private readonly IOptionsEquality<TMatch> _options;

		public EndWithConstraint(string it,
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

		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{_it} was <null>");
			}

			if (_expected.Length == 0)
			{
				return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			List<TItem> items = new();
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				items.Add(item);
			}

			int offset = items.Count - _expected.Length;
			for (int index = _expected.Length - 1; index >= 0; index--)
			{
				if (index + offset < 0)
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
						$"{_it} contained only {items.Count} items and misses {-offset} items: {Formatter.Format(_expected.Take(-offset), FormattingOptions.MultipleLines)}");
				}

				TItem item = items[index + offset];
				TItem expectedItem = _expected[index];
				if (!_options.AreConsideredEqual(item, expectedItem))
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
						$"{_it} contained {Formatter.Format(item)} at index {index + offset} instead of {Formatter.Format(expectedItem)}");
				}
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
		}

		public override string ToString()
			=> $"end with {_expectedExpression}{_options}";
	}

	private readonly struct NotEndWithConstraint<TItem, TMatch> : IAsyncContextConstraint<IAsyncEnumerable<TItem>>
		where TItem : TMatch
	{
		private readonly string _it;
		private readonly string _unexpectedExpression;
		private readonly TItem[] _unexpected;
		private readonly IOptionsEquality<TMatch> _options;

		public NotEndWithConstraint(string it,
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
			List<TItem> items = new();
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				items.Add(item);
			}

			if (_unexpected.Length == 0)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
					$"{_it} did in {Formatter.Format(items, FormattingOptions.MultipleLines)}");
			}

			int offset = items.Count - _unexpected.Length;
			for (int index = _unexpected.Length - 1; index >= 0; index--)
			{
				if (index + offset < 0)
				{
					return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
				}

				TItem item = items[index + offset];
				TItem unexpectedItem = _unexpected[index];
				if (!_options.AreConsideredEqual(item, unexpectedItem))
				{
					return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
				}
			}

			return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
				$"{_it} did in {Formatter.Format(items, FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> $"not end with {_unexpectedExpression}{_options}";
	}
}
#endif
