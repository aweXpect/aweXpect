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
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		StartWith<TItem>(
			this IThat<IEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(source
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
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		StartWith<TItem>(
			this IThat<IEnumerable<TItem>> source,
			params TItem[] expected)
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(source
				.ExpectationBuilder
				.AddConstraint(it
					=> new StartWithConstraint<TItem, object?>(it, Formatter.Format(expected), expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection starts with the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringEqualityResult<IEnumerable<string>, IThat<IEnumerable<string>>>
		StartWith(
			this IThat<IEnumerable<string>> source,
			IEnumerable<string> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string>, IThat<IEnumerable<string>>>(source
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
	public static StringEqualityResult<IEnumerable<string>, IThat<IEnumerable<string>>>
		StartWith(
			this IThat<IEnumerable<string>> source,
			params string[] expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string>, IThat<IEnumerable<string>>>(source
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
		: IContextConstraint<IEnumerable<TItem>>
		where TItem : TMatch
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			if (expected == null)
			{
				throw new ArgumentNullException(nameof(expected));
			}

			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was <null>");
			}

			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			if (expected.Length == 0)
			{
				return new ConstraintResult.Success<IEnumerable<TItem>>(actual, ToString());
			}

			int index = 0;
			foreach (TItem item in materializedEnumerable)
			{
				TItem expectedItem = expected[index++];
				if (!options.AreConsideredEqual(item, expectedItem))
				{
					return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
						$"{it} contained {Formatter.Format(item)} at index {index - 1} instead of {Formatter.Format(expectedItem)}");
				}

				if (expected.Length == index)
				{
					return new ConstraintResult.Success<IEnumerable<TItem>>(actual, ToString());
				}
			}


			return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
				$"{it} contained only {index} items and misses {expected.Length - index} items: {Formatter.Format(expected.Skip(index), FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> $"start with {expectedExpression}";
	}
}
