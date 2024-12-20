using System;
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

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that the collection does not start with the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		NotStartWith<TItem>(
			this IThat<IEnumerable<TItem>> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(source
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
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		NotStartWith<TItem>(
			this IThat<IEnumerable<TItem>> source,
			params TItem[] unexpected)
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(source
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
	public static StringEqualityResult<IEnumerable<string>, IThat<IEnumerable<string>>>
		NotStartWith(
			this IThat<IEnumerable<string>> source,
			IEnumerable<string> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string>, IThat<IEnumerable<string>>>(source
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
	public static StringEqualityResult<IEnumerable<string>, IThat<IEnumerable<string>>>
		NotStartWith(
			this IThat<IEnumerable<string>> source,
			params string[] unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string>, IThat<IEnumerable<string>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new NotStartWithConstraint<string, string>(it, Formatter.Format(unexpected), unexpected,
						options)),
			source,
			options);
	}

	private readonly struct NotStartWithConstraint<TItem, TMatch> : IContextConstraint<IEnumerable<TItem>>
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
			_it = it;
			_unexpectedExpression = unexpectedExpression;
			_unexpected = unexpected ?? throw new ArgumentNullException(nameof(unexpected));
			_options = options;
		}

		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
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
			=> $"not start with {_unexpectedExpression}{_options}";
	}
}
