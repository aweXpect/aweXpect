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

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection only contains unique items.
	/// </summary>
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem> AreAllUnique<TItem>(
		this IThat<IEnumerable<TItem>?> source)
	{
		ObjectEqualityOptions<TItem> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new AreAllUniqueConstraint<TItem, TItem>(expectationBuilder, it, grammars, options)),
			source, options
		);
	}

	/// <summary>
	///     Verifies that the collection only contains unique items.
	/// </summary>
	public static StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>> AreAllUnique(
		this IThat<IEnumerable<string?>?> source)
	{
		StringEqualityOptions options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new AreAllUniqueConstraint<string, string>(expectationBuilder, it, grammars, options)),
			source, options
		);
	}

	/// <summary>
	///     Verifies that the collection only contains items with unique members specified by the
	///     <paramref name="memberAccessor" />.
	/// </summary>
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TMember> AreAllUnique<TItem,
		TMember>(
		this IThat<IEnumerable<TItem>?> source,
		Func<TItem, TMember> memberAccessor,
		[CallerArgumentExpression("memberAccessor")]
		string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TMember> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TMember>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new AreAllUniqueWithPredicateConstraint<TItem, TMember, TMember>(
					expectationBuilder,
					it, grammars,
					memberAccessor,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					options)),
			source, options
		);
	}

	/// <summary>
	///     Verifies that the collection only contains items with unique members specified by the
	///     <paramref name="memberAccessor" />.
	/// </summary>
	public static StringEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>> AreAllUnique<TItem>(
		this IThat<IEnumerable<TItem>?> source,
		Func<TItem, string> memberAccessor,
		[CallerArgumentExpression("memberAccessor")]
		string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new AreAllUniqueWithPredicateConstraint<TItem, string, string>(
					expectationBuilder,
					it, grammars,
					memberAccessor,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					options)),
			source, options
		);
	}

	private sealed class AreAllUniqueConstraint<TItem, TMatch>(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		IOptionsEquality<TMatch> options)
		: ConstraintResult.WithNotNullValue<IEnumerable<TItem>?>(it, grammars),
			IContextConstraint<IEnumerable<TItem>?>
		where TItem : TMatch
	{
		private readonly List<TItem> _duplicates = [];

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IEnumerable<TItem> materialized = context
				.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			List<TItem> checkedItems = new();

			IOptionsEquality<TMatch> o = options;
			foreach (TItem item in materialized)
			{
				if (checkedItems.Any(compareWith =>
					    o.AreConsideredEqual(item, compareWith) &&
					    _duplicates.All(x => !o.AreConsideredEqual(item, x))))
				{
					_duplicates.Add(item);
				}

				checkedItems.Add(item);
			}

			Outcome = _duplicates.Any() ? Outcome.Failure : Outcome.Success;
			expectationBuilder.AddCollectionContext(materialized);
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("only has unique items");
			stringBuilder.Append(options);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(CollectionHelpers.CreateDuplicateFailureMessage(It, _duplicates));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("has duplicate items");
			stringBuilder.Append(options);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("all were unique");
	}

	private sealed class AreAllUniqueWithPredicateConstraint<TItem, TMember, TMatch>(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		Func<TItem, TMember> memberAccessor,
		string memberAccessorExpression,
		IOptionsEquality<TMatch> options)
		: ConstraintResult.WithNotNullValue<IEnumerable<TItem>?>(it, grammars),
			IContextConstraint<IEnumerable<TItem>?>
		where TMember : TMatch
	{
		private readonly List<TMember> _duplicates = [];

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IEnumerable<TItem> materialized = context
				.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			List<TMember> checkedItems = new();

			IOptionsEquality<TMatch> o = options;
			foreach (TItem item in materialized)
			{
				TMember itemMember = memberAccessor(item);
				if (checkedItems.Any(compareWith =>
					    o.AreConsideredEqual(itemMember, compareWith) &&
					    _duplicates.All(x => !o.AreConsideredEqual(itemMember, x))))
				{
					_duplicates.Add(itemMember);
				}

				checkedItems.Add(itemMember);
			}

			Outcome = _duplicates.Any() ? Outcome.Failure : Outcome.Success;
			expectationBuilder.AddCollectionContext(materialized);
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("only has unique items for ").Append(memberAccessorExpression);
			stringBuilder.Append(options);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(CollectionHelpers.CreateDuplicateFailureMessage(It, _duplicates));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("has duplicate items for ").Append(memberAccessorExpression);
			stringBuilder.Append(options);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("all were unique");
	}
}
