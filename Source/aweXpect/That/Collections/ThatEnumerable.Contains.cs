using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<TItem>(
					it, grammars,
					q => q.ToString() == "never"
						? $"does not contain {Formatter.Format(expected)}{options}"
						: $"contains {Formatter.Format(expected)}{options} {q}",
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
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<string?>(
					it, grammars,
					q => q.ToString() == "never"
						? $"does not contain {Formatter.Format(expected)}{options}"
						: $"contains {Formatter.Format(expected)}{options} {q}",
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
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<TItem>(
					it, grammars,
					q => q.ToString() == "never"
						? $"does not contain item matching {doNotPopulateThisValue.TrimCommonWhiteSpace()}"
						: $"contains item matching {doNotPopulateThisValue.TrimCommonWhiteSpace()} {q}",
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
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsConstraint<TItem, TItem>(it, grammars, doNotPopulateThisValue.TrimCommonWhiteSpace(), expected,
					options, matchOptions)),
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
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsConstraint<string?, string?>(it, grammars, doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectCountResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		DoesNotContain<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			TItem unexpected)
	{
		Quantifier quantifier = new();
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectCountResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<TItem>(it, grammars,
					q => q.ToString() == "never"
						? $"does not contain {Formatter.Format(unexpected)}{options}"
						: $"does not contain {Formatter.Format(unexpected)}{options} {q.ToNegatedString()}",
					a => options.AreConsideredEqual(a, unexpected),
					quantifier).Invert()),
			source,
			quantifier,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static StringEqualityTypeCountResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		DoesNotContain(
			this IThat<IEnumerable<string?>?> source,
			string? unexpected)
	{
		Quantifier quantifier = new();
		StringEqualityOptions options = new();
		return new StringEqualityTypeCountResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<string?>(it, grammars,
					q => q.ToString() == "never"
						? $"does not contain {Formatter.Format(unexpected)}{options}"
						: $"does not contain {Formatter.Format(unexpected)}{options} {q.ToNegatedString()}",
					a => options.AreConsideredEqual(a, unexpected),
					quantifier).Invert()),
			source,
			quantifier,
			options);
	}

	/// <summary>
	///     Verifies that the collection contains no item that satisfies the <paramref name="predicate" />.
	/// </summary>
	public static CountResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		DoesNotContain<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
	{
		Quantifier quantifier = new();
		return new CountResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<TItem>(it, grammars,
					q => q.ToString() == "never"
						? $"does not contain item matching {doNotPopulateThisValue.TrimCommonWhiteSpace()}"
						: $"does not contain item matching {doNotPopulateThisValue.TrimCommonWhiteSpace()} {q.ToNegatedString()}",
					predicate,
					quantifier).Invert()),
			source,
			quantifier);
	}

	private sealed class ContainConstraint<TItem>(
		string it,
		ExpectationGrammars grammars,
		Func<Quantifier, string> expectationText,
		Func<TItem, bool> predicate,
		Quantifier quantifier)
		: ConstraintResult(grammars),
			IContextConstraint<IEnumerable<TItem>?>
	{
		private IEnumerable<TItem>? _actual;
		private int _count;
		private bool _isFinished;
		private bool _isNegated;
		private IEnumerable<TItem>? _materializedEnumerable;

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			_actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			_materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			_count = 0;
			foreach (TItem item in _materializedEnumerable.Where(predicate))
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

			if (quantifier.Check(_count, true) ?? _isNegated)
			{
				Outcome = Outcome.Success;
				return this;
			}

			_isFinished = true;
			Outcome = Outcome.Failure;
			return this;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectationText.Invoke(quantifier));

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual == null)
			{
				stringBuilder.Append(it).Append(" was <null>");
			}
			else if (_isFinished)
			{
				stringBuilder.Append(it).Append(" contained it ").Append(_count).Append(" times in ");
				Formatter.Format(stringBuilder, _materializedEnumerable, FormattingOptions.MultipleLines);
			}
			else
			{
				stringBuilder.Append(it).Append(" contained it at least ").Append(_count).Append(" times in ");
				Formatter.Format(stringBuilder, _materializedEnumerable, FormattingOptions.MultipleLines);
			}
		}

		/// <inheritdoc cref="ConstraintResult.TryGetValue{TValue}(out TValue)" />
		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_actual is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			value = default;
			return typeof(TValue).IsAssignableFrom(typeof(IEnumerable<TItem>));
		}

		public override ConstraintResult Negate()
		{
			_isNegated = !_isNegated;
			quantifier.Negate();
			Outcome = Outcome switch
			{
				Outcome.Failure => Outcome.Success,
				Outcome.Success => Outcome.Failure,
				_ => Outcome,
			};
			return this;
		}
	}
}
