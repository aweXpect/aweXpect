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
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new OnlyHasUniqueItemsConstraint<TItem, TItem>(it, options)),
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
		return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new OnlyHasUniqueItemsConstraint<string, string>(it, options)),
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
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TMember>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new OnlyHasUniqueItemsWithPredicateConstraint<TItem, TMember, TMember>(it, memberAccessor,
					doNotPopulateThisValue,
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
		return new StringEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new OnlyHasUniqueItemsWithPredicateConstraint<TItem, string, string>(it, memberAccessor,
					doNotPopulateThisValue,
					options)),
			source, options
		);
	}

	private readonly struct OnlyHasUniqueItemsConstraint<TItem, TMatch>(string it, IOptionsEquality<TMatch> options)
		: IContextConstraint<IEnumerable<TItem>?>
		where TItem : TMatch
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			IEnumerable<TItem> materialized = context
				.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			List<TItem> checkedItems = new();
			List<TItem> duplicates = new();

			IOptionsEquality<TMatch> o = options;
			foreach (TItem item in materialized)
			{
				if (checkedItems.Any(compareWith =>
					    o.AreConsideredEqual(item, compareWith) &&
					    duplicates.All(x => !o.AreConsideredEqual(item, x))))
				{
					duplicates.Add(item);
				}

				checkedItems.Add(item);
			}

			if (duplicates.Any())
			{
				string failure = CollectionHelpers.CreateDuplicateFailureMessage(it, duplicates);
				return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(), failure);
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(actual,
				ToString());
		}

		public override string ToString() => $"only has unique items{options}";
	}

	private readonly struct OnlyHasUniqueItemsWithPredicateConstraint<TItem, TMember, TMatch>(
		string it,
		Func<TItem, TMember> memberAccessor,
		string memberAccessorExpression,
		IOptionsEquality<TMatch> options)
		: IContextConstraint<IEnumerable<TItem>?>
		where TMember : TMatch
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			IEnumerable<TItem> materialized = context
				.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			List<TMember> checkedItems = new();
			List<TMember> duplicates = new();

			IOptionsEquality<TMatch> o = options;
			foreach (TItem item in materialized)
			{
				TMember itemMember = memberAccessor(item);
				if (checkedItems.Any(compareWith =>
					    o.AreConsideredEqual(itemMember, compareWith) &&
					    duplicates.All(x => !o.AreConsideredEqual(itemMember, x))))
				{
					duplicates.Add(itemMember);
				}

				checkedItems.Add(itemMember);
			}

			if (duplicates.Any())
			{
				string failure = CollectionHelpers.CreateDuplicateFailureMessage(it, duplicates);
				return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(), failure);
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(actual,
				ToString());
		}

		public override string ToString() => $"only has unique items for {memberAccessorExpression}{options}";
	}
}
