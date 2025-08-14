using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		IsContainedIn<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<TItem, TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		IsContainedIn(this IThat<IEnumerable<string?>?> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringCollectionBeContainedInResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<string?, string?>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<IEnumerable, IThat<IEnumerable>, TItem>
		IsContainedIn<TItem>(
			this IThat<IEnumerable> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionBeContainedInResult<IEnumerable, IThat<IEnumerable>, TItem>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToForEnumerableConstraint<IEnumerable, TItem, TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>, TItem>
		IsContainedIn<TItem>(
			this IThat<ImmutableArray<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionBeContainedInResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>, TItem>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToForEnumerableConstraint<ImmutableArray<TItem>, TItem, TItem>(expectationBuilder, it,
					grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<ImmutableArray<string?>, IThat<ImmutableArray<string?>>>
		IsContainedIn(this IThat<ImmutableArray<string?>> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringCollectionBeContainedInResult<ImmutableArray<string?>, IThat<ImmutableArray<string?>>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToForEnumerableConstraint<ImmutableArray<string?>, string?, string?>(expectationBuilder, it,
					grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}
#endif

	/// <summary>
	///     Verifies that the collection is not contained in the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		IsNotContainedIn<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<TItem, TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection is not contained in the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		IsNotContainedIn(this IThat<IEnumerable<string?>?> source,
			IEnumerable<string?> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringCollectionBeContainedInResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<string?, string?>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection is not contained in the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<IEnumerable, IThat<IEnumerable>, TItem>
		IsNotContainedIn<TItem>(
			this IThat<IEnumerable> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionBeContainedInResult<IEnumerable, IThat<IEnumerable>, TItem>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToForEnumerableConstraint<IEnumerable, TItem, TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is not contained in the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>, TItem>
		IsNotContainedIn<TItem>(
			this IThat<ImmutableArray<TItem>> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionBeContainedInResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>, TItem>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToForEnumerableConstraint<ImmutableArray<TItem>, TItem, TItem>(expectationBuilder, it,
					grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<ImmutableArray<string?>, IThat<ImmutableArray<string?>>>
		IsNotContainedIn(this IThat<ImmutableArray<string?>> source,
			IEnumerable<string?> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringCollectionBeContainedInResult<ImmutableArray<string?>, IThat<ImmutableArray<string?>>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToForEnumerableConstraint<ImmutableArray<string?>, string?, string?>(expectationBuilder, it,
					grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}
#endif

	/// <summary>
	///     Verifies that the collection is contained within a set defined by the provided <paramref name="predicates" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsContainedIn<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<Func<TItem, bool>> predicates,
			[CallerArgumentExpression("predicates")] string doNotPopulateThisValue = "")
	{
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsContainedInPredicatesConstraint<TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					predicates)),
			source);
	}

	/// <summary>
	///     Verifies that the collection is contained within a set defined by the provided <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsContainedIn<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<Action<IThat<TItem>>> expectations,
			[CallerArgumentExpression("expectations")] string doNotPopulateThisValue = "")
	{
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsContainedInExpectationsConstraint<TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expectations)),
			source);
	}

	private sealed class IsContainedInPredicatesConstraint<TItem>(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		string predicatesExpression,
		IEnumerable<Func<TItem, bool>> predicates)
		: ConstraintResult(grammars),
			IContextConstraint<IEnumerable<TItem>?>
	{
		private IEnumerable<TItem>? _actual;
		private IList<Func<TItem, bool>>? _materializedPredicates;
		private IEnumerable<TItem>? _materializedEnumerable;
		private readonly List<(int Index, TItem Item)> _itemsNotSatisfyingAnyPredicate = new();

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			_actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			_materializedEnumerable = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			_materializedPredicates = predicates.ToList();
			
			var actualList = _materializedEnumerable.ToList();
			
			// Check that each item satisfies at least one predicate
			for (int i = 0; i < actualList.Count; i++)
			{
				bool itemSatisfiesAnyPredicate = false;
				foreach (var predicate in _materializedPredicates)
				{
					if (predicate(actualList[i]))
					{
						itemSatisfiesAnyPredicate = true;
						break;
					}
				}

				if (!itemSatisfiesAnyPredicate)
				{
					_itemsNotSatisfyingAnyPredicate.Add((i, actualList[i]));
				}
			}

			if (_itemsNotSatisfyingAnyPredicate.Any())
			{
				Outcome = Outcome.Failure;
				expectationBuilder.AddCollectionContext(_materializedEnumerable);
				return this;
			}

			Outcome = Outcome.Success;
			return this;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append($"is contained in set defined by predicates {predicatesExpression}");

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual == null)
			{
				stringBuilder.Append(it).Append(" was <null>");
			}
			else if (_materializedPredicates == null)
			{
				stringBuilder.Append(it).Append(" was not evaluated");
			}
			else if (_itemsNotSatisfyingAnyPredicate.Any())
			{
				stringBuilder.Append(it).Append($" had {_itemsNotSatisfyingAnyPredicate.Count} item(s) that did not satisfy any predicate");
				foreach (var item in _itemsNotSatisfyingAnyPredicate.Take(3))
				{
					stringBuilder.Append($"\n  - Item at index {item.Index}: {Formatter.Format(item.Item)}");
				}
				if (_itemsNotSatisfyingAnyPredicate.Count > 3)
				{
					stringBuilder.Append($"\n  - (and {_itemsNotSatisfyingAnyPredicate.Count - 3} more)");
				}
			}
		}

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
			Outcome = Outcome switch
			{
				Outcome.Failure => Outcome.Success,
				Outcome.Success => Outcome.Failure,
				_ => Outcome,
			};
			return this;
		}
	}

	private sealed class IsContainedInExpectationsConstraint<TItem>(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		string expectationsExpression,
		IEnumerable<Action<IThat<TItem>>> expectations)
		: ConstraintResult(grammars),
			IAsyncContextConstraint<IEnumerable<TItem>?>
	{
		private readonly ExpectationGrammars _grammars = grammars;
		private IEnumerable<TItem>? _actual;
		private IList<Action<IThat<TItem>>>? _materializedExpectations;
		private IEnumerable<TItem>? _materializedEnumerable;
		private readonly List<(int Index, TItem Item, List<string> FailureMessages)> _itemsNotSatisfyingAnyExpectation = new();

		public async Task<ConstraintResult> IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context, CancellationToken cancellationToken)
		{
			_actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			_materializedEnumerable = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			_materializedExpectations = expectations.ToList();
			
			var actualList = _materializedEnumerable.ToList();
			
			// Check that each item satisfies at least one expectation
			for (int i = 0; i < actualList.Count; i++)
			{
				bool itemSatisfiesAnyExpectation = false;
				var failureMessages = new List<string>();

				foreach (var expectation in _materializedExpectations)
				{
					try
					{
						var itemExpectationBuilder = new ManualExpectationBuilder<TItem>(expectationBuilder, _grammars);
						expectation(new ThatSubject<TItem>(itemExpectationBuilder));
						var isMatch = await itemExpectationBuilder.IsMetBy(actualList[i], context, cancellationToken);
						if (isMatch.Outcome == Outcome.Success)
						{
							itemSatisfiesAnyExpectation = true;
							break;
						}
						else
						{
							failureMessages.Add(isMatch.ToString() ?? "Unknown failure");
						}
					}
					catch (Exception ex)
					{
						failureMessages.Add(ex.Message);
					}
				}

				if (!itemSatisfiesAnyExpectation)
				{
					_itemsNotSatisfyingAnyExpectation.Add((i, actualList[i], failureMessages));
				}
			}

			if (_itemsNotSatisfyingAnyExpectation.Any())
			{
				Outcome = Outcome.Failure;
				expectationBuilder.AddCollectionContext(_materializedEnumerable);
				return this;
			}

			Outcome = Outcome.Success;
			return this;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append($"is contained in set defined by expectations {expectationsExpression}");

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual == null)
			{
				stringBuilder.Append(it).Append(" was <null>");
			}
			else if (_materializedExpectations == null)
			{
				stringBuilder.Append(it).Append(" was not evaluated");
			}
			else if (_itemsNotSatisfyingAnyExpectation.Any())
			{
				stringBuilder.Append(it).Append($" had {_itemsNotSatisfyingAnyExpectation.Count} item(s) that did not satisfy any expectation");
				foreach (var item in _itemsNotSatisfyingAnyExpectation.Take(3))
				{
					stringBuilder.Append($"\n  - Item at index {item.Index}: {Formatter.Format(item.Item)}");
					if (item.FailureMessages.Any())
					{
						stringBuilder.Append($" (failures: {string.Join("; ", item.FailureMessages.Take(2))})");
					}
				}
				if (_itemsNotSatisfyingAnyExpectation.Count > 3)
				{
					stringBuilder.Append($"\n  - (and {_itemsNotSatisfyingAnyExpectation.Count - 3} more)");
				}
			}
		}

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
