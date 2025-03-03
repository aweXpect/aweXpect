using System;
using System.Collections.Generic;
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
	///     Verifies that the collection contains the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectCountResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		Contains<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			TItem expected)
	{
		Quantifier quantifier = new();
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectCountResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<TItem>(
					it,
					q => $"contains {Formatter.Format(expected)}{options} {q}",
					a => options.AreConsideredEqual(a, expected),
					quantifier)),
			source,
			quantifier,
			options);
	}

	/// <summary>
	///     Verifies that the collection contains the <paramref name="expected" /> value.
	/// </summary>
	public static StringEqualityTypeCountResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>> Contains(
		this IThat<IEnumerable<string?>?> source,
		string? expected)
	{
		Quantifier quantifier = new();
		StringEqualityOptions options = new();
		return new StringEqualityTypeCountResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<string?>(
					it,
					q => $"contains {Formatter.Format(expected)}{options} {q}",
					a => options.AreConsideredEqual(a, expected),
					quantifier)),
			source,
			quantifier,
			options);
	}

	/// <summary>
	///     Verifies that the collection contains an item that satisfies the <paramref name="predicate" />.
	/// </summary>
	public static CountResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		Contains<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
	{
		Quantifier quantifier = new();
		return new CountResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<TItem>(
					it,
					q => $"contains item matching {doNotPopulateThisValue.TrimCommonWhiteSpace()} {q}",
					predicate,
					quantifier)),
			source,
			quantifier);
	}

	/// <summary>
	///     Verifies that the collection contains the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionContainResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		Contains<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.Contains);
		return new ObjectCollectionContainResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsConstraint<TItem, TItem>(it, doNotPopulateThisValue.TrimCommonWhiteSpace(), expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection contains the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionContainResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		Contains(this IThat<IEnumerable<string?>?> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.Contains);
		return new StringCollectionContainResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsConstraint<string?, string?>(it, doNotPopulateThisValue.TrimCommonWhiteSpace(), expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		DoesNotContain<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			TItem unexpected)
	{
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NotContainConstraint<TItem>(it,
					() => $"does not contain {Formatter.Format(unexpected)}",
					a => options.AreConsideredEqual(a, unexpected))),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		DoesNotContain(
			this IThat<IEnumerable<string?>?> source,
			string? unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NotContainConstraint<string?>(it,
					() => $"does not contain {Formatter.Format(unexpected)}{options}",
					a => options.AreConsideredEqual(a, unexpected))),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection contains no item that satisfies the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		DoesNotContain<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NotContainConstraint<TItem>(it,
					() => $"does not contain item matching {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
					predicate)),
			source);

	private readonly struct ContainConstraint<TItem>(
		string it,
		Func<Quantifier, string> expectationText,
		Func<TItem, bool> predicate,
		Quantifier quantifier)
		: IContextConstraint<IEnumerable<TItem>?>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			int count = 0;
			foreach (TItem item in materializedEnumerable)
			{
				if (predicate(item))
				{
					count++;
					bool? check = quantifier.Check(count, false);
					if (check == false)
					{
						return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
							$"{it} contained it at least {count} times in {Formatter.Format(materializedEnumerable, FormattingOptions.MultipleLines)}");
					}

					if (check == true)
					{
						return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
							ToString());
					}
				}
			}

			if (quantifier.Check(count, true) == true)
			{
				return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
					ToString());
			}

			return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
				$"{it} contained it {count} times in {Formatter.Format(materializedEnumerable, FormattingOptions.MultipleLines)}");
		}

		public override string ToString() => expectationText(quantifier);
	}

	private readonly struct NotContainConstraint<TItem>(
		string it,
		Func<string> expectationText,
		Func<TItem, bool> predicate)
		: IContextConstraint<IEnumerable<TItem>?>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			foreach (TItem item in materializedEnumerable)
			{
				if (predicate(item))
				{
					return new ConstraintResult.Failure(ToString(),
						$"{it} did");
				}
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		public override string ToString()
			=> expectationText();
	}
}
