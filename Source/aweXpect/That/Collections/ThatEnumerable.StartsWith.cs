﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		StartsWith<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new StartsWithConstraint<TItem, TItem>(it, doNotPopulateThisValue.TrimCommonWhiteSpace(), expected.ToArray(),
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		StartsWith<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			params TItem[] expected)
	{
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new StartsWithConstraint<TItem, TItem>(it, Formatter.Format(expected), expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		StartsWith(
			this IThat<IEnumerable<string?>?> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new StartsWithConstraint<string?, string?>(it, doNotPopulateThisValue.TrimCommonWhiteSpace(), expected.ToArray(),
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		StartsWith(
			this IThat<IEnumerable<string?>?> source,
			params string[] expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new StartsWithConstraint<string, string>(it, Formatter.Format(expected), expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		DoesNotStartWith<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new NotStartsWithConstraint<TItem, TItem>(it, doNotPopulateThisValue.TrimCommonWhiteSpace(), unexpected.ToArray(),
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		DoesNotStartWith<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			params TItem[] unexpected)
	{
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new NotStartsWithConstraint<TItem, TItem>(it, Formatter.Format(unexpected), unexpected,
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		DoesNotStartWith(
			this IThat<IEnumerable<string?>?> source,
			IEnumerable<string?> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new NotStartsWithConstraint<string?, string?>(it, doNotPopulateThisValue.TrimCommonWhiteSpace(), unexpected.ToArray(),
					options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		DoesNotStartWith(
			this IThat<IEnumerable<string?>?> source,
			params string[] unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new NotStartsWithConstraint<string, string>(it, Formatter.Format(unexpected), unexpected,
					options)),
			source,
			options);
	}

	private readonly struct StartsWithConstraint<TItem, TMatch> : IContextConstraint<IEnumerable<TItem>?>
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
			_it = it;
			_expectedExpression = expectedExpression;
			_expected = expected ?? throw new ArgumentNullException(nameof(expected));
			_options = options;
		}

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{_it} was <null>");
			}

			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			if (_expected.Length == 0)
			{
				return new ConstraintResult.Success<IEnumerable<TItem>>(actual, ToString());
			}

			int index = 0;
			foreach (TItem item in materializedEnumerable)
			{
				TItem expectedItem = _expected[index++];
				if (!_options.AreConsideredEqual(item, expectedItem))
				{
					return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
						$"{_it} contained {Formatter.Format(item)} at index {index - 1} instead of {Formatter.Format(expectedItem)}");
				}

				if (_expected.Length == index)
				{
					return new ConstraintResult.Success<IEnumerable<TItem>>(actual, ToString());
				}
			}


			return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
				$"{_it} contained only {index} items and misses {_expected.Length - index} items: {Formatter.Format(_expected.Skip(index), FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> $"starts with {_expectedExpression}{_options}";
	}

	private readonly struct NotStartsWithConstraint<TItem, TMatch> : IContextConstraint<IEnumerable<TItem>?>
		where TItem : TMatch
	{
		private readonly string _it;
		private readonly string _unexpectedExpression;
		private readonly TItem[] _unexpected;
		private readonly IOptionsEquality<TMatch> _options;

		public NotStartsWithConstraint(string it,
			string unexpectedExpression,
			TItem[] unexpected,
			IOptionsEquality<TMatch> options)
		{
			_it = it;
			_unexpectedExpression = unexpectedExpression;
			_unexpected = unexpected ?? throw new ArgumentNullException(nameof(unexpected));
			_options = options;
		}

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{_it} was <null>");
			}

			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			if (_unexpected.Length == 0)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
					$"{_it} was {Formatter.Format(materializedEnumerable, FormattingOptions.MultipleLines)}");
			}

			int index = 0;
			List<TItem> foundValues = new();
			foreach (TItem item in materializedEnumerable)
			{
				foundValues.Add(item);
				TItem unexpectedItem = _unexpected[index++];
				if (!_options.AreConsideredEqual(item, unexpectedItem))
				{
					return new ConstraintResult.Success<IEnumerable<TItem>>(actual, ToString());
				}

				if (_unexpected.Length == index)
				{
					return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
						$"{_it} did start with {Formatter.Format(foundValues, FormattingOptions.MultipleLines)}");
				}
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(actual, ToString());
		}

		public override string ToString()
			=> $"does not start with {_unexpectedExpression}{_options}";
	}
}
