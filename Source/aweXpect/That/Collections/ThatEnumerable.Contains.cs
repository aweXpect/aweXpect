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
					it, grammars,
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
					it, grammars,
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
					it, grammars,
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
				new IsConstraint<TItem, TItem>(it, grammars, doNotPopulateThisValue.TrimCommonWhiteSpace(), expected, options, matchOptions)),
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
				new IsConstraint<string?, string?>(it, grammars, doNotPopulateThisValue.TrimCommonWhiteSpace(), expected, options, matchOptions)),
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
				new ContainConstraint<TItem>(it, grammars,
					_ => $"does not contain {Formatter.Format(unexpected)}",
					a => options.AreConsideredEqual(a, unexpected),
					Quantifier.Never())),
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
				new ContainConstraint<string?>(it, grammars,
					_ => $"does not contain {Formatter.Format(unexpected)}{options}",
					a => options.AreConsideredEqual(a, unexpected),
					Quantifier.Never())),
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
				new ContainConstraint<TItem>(it, grammars,
					_ => $"does not contain item matching {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
					predicate,
					Quantifier.Never())),
			source);

	private class ContainConstraint<TItem>(
		string it,
		ExpectationGrammars grammars,
		Func<Quantifier, string> expectationText,
		Func<TItem, bool> predicate,
		Quantifier quantifier)
		: ConstraintResult.WithNotNullValue<IEnumerable<TItem>?>(it, grammars),
			IContextConstraint<IEnumerable<TItem>?>
	{
		private int _count;
		private IEnumerable<TItem>? _materializedEnumerable;
		private bool _isFinished;

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			_materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			_count = 0;
			foreach (TItem item in _materializedEnumerable)
			{
				if (predicate(item))
				{
					_count++;
					bool? check = quantifier.Check(_count, false);
					switch (check)
					{
						case false:
							Outcome = Outcome.Failure;
							return this;
						case true:
							Outcome = Outcome.Success;
							return this;
					}
				}
			}

			if (quantifier.Check(_count, true) == true)
			{
				Outcome = Outcome.Success;
				return this;
			}

			_isFinished = true;
			Outcome = Outcome.Failure;
			return this;
		}
		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectationText.Invoke(quantifier));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_isFinished)
			{
				stringBuilder.Append(It).Append(" contained it ").Append(_count).Append(" times in ");
				Formatter.Format(stringBuilder, _materializedEnumerable, FormattingOptions.MultipleLines);
			}
			else
			{
				stringBuilder.Append(It).Append(" contained it at least ").Append(_count).Append(" times in ");
				Formatter.Format(stringBuilder, _materializedEnumerable, FormattingOptions.MultipleLines);
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectationText.Invoke(quantifier));

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(It).Append(" did");

		public override ConstraintResult Negate()
		{
			quantifier.Negate();
			return base.Negate();
		}
	}
}
