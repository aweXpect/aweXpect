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

namespace aweXpect;

public static partial class ThatDictionary
{
	/// <summary>
	///     Verifies that the dictionary only contains unique values.
	/// </summary>
	/// <remarks>
	///     This expectation completely ignores the dictionary keys, as they are unique by design.
	/// </remarks>
	public static ObjectEqualityResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>, TValue>
		AreAllUnique<TKey, TValue>(
			this IThat<IDictionary<TKey, TValue>?> source)
	{
		ObjectEqualityOptions<TValue> options = new();
		return new ObjectEqualityResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>, TValue>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new AllIsUniqueConstraint<TKey, TValue, TValue>(it, options)),
			source, options
		);
	}

	/// <summary>
	///     Verifies that the dictionary only contains unique values.
	/// </summary>
	/// <remarks>
	///     This expectation completely ignores the dictionary keys, as they are unique by design.
	/// </remarks>
	public static StringEqualityResult<IDictionary<TKey, string?>, IThat<IDictionary<TKey, string?>?>>
		AreAllUnique<TKey>(this IThat<IDictionary<TKey, string?>?> source)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IDictionary<TKey, string?>, IThat<IDictionary<TKey, string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new AllIsUniqueConstraint<TKey, string?, string?>(it, options)),
			source, options
		);
	}

	/// <summary>
	///     Verifies that the dictionary only contains values with unique members specified by the
	///     <paramref name="memberAccessor" />.
	/// </summary>
	/// <remarks>
	///     This expectation completely ignores the dictionary keys, as they are unique by design.
	/// </remarks>
	public static ObjectEqualityResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>, TMember>
		AreAllUnique<TKey,
			TValue, TMember>(
			this IThat<IDictionary<TKey, TValue>?> source,
			Func<TValue, TMember> memberAccessor,
			[CallerArgumentExpression("memberAccessor")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TMember> options = new();
		return new ObjectEqualityResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>, TMember>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new AllIsUniqueWithPredicateConstraint<TKey, TValue, TMember, TMember>(it, memberAccessor,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					options)),
			source, options
		);
	}

	/// <summary>
	///     Verifies that the dictionary only contains values with unique members specified by the
	///     <paramref name="memberAccessor" />.
	/// </summary>
	/// <remarks>
	///     This expectation completely ignores the dictionary keys, as they are unique by design.
	/// </remarks>
	public static StringEqualityResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>>
		AreAllUnique<TKey,
			TValue>(
			this IThat<IDictionary<TKey, TValue>?> source,
			Func<TValue, string> memberAccessor,
			[CallerArgumentExpression("memberAccessor")]
			string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new AllIsUniqueWithPredicateConstraint<TKey, TValue, string, string>(it, memberAccessor,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					options)),
			source, options
		);
	}

	private readonly struct AllIsUniqueConstraint<TKey, TValue, TMatch>(string it, IOptionsEquality<TMatch> options)
		: IContextConstraint<IDictionary<TKey, TValue>?>
		where TValue : TMatch
	{
		public ConstraintResult IsMetBy(IDictionary<TKey, TValue>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>?>(actual, ToString(), $"{it} was <null>");
			}

			List<TValue> checkedItems = new();
			List<TValue> duplicates = new();

			IOptionsEquality<TMatch> o = options;
			foreach (TValue item in actual.Values)
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
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(), failure);
			}

			return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual,
				ToString());
		}

		public override string ToString() => $"only has unique values{options}";
	}

	private readonly struct AllIsUniqueWithPredicateConstraint<TKey, TValue, TMember, TMatch>(
		string it,
		Func<TValue, TMember> memberAccessor,
		string memberAccessorExpression,
		IOptionsEquality<TMatch> options)
		: IContextConstraint<IDictionary<TKey, TValue>?>
		where TMember : TMatch
	{
		public ConstraintResult IsMetBy(IDictionary<TKey, TValue>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>?>(actual, ToString(), $"{it} was <null>");
			}

			List<TMember> checkedItems = new();
			List<TMember> duplicates = new();

			IOptionsEquality<TMatch> o = options;
			foreach (TValue item in actual.Values)
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
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(), failure);
			}

			return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual,
				ToString());
		}

		public override string ToString() => $"only has unique values for {memberAccessorExpression}{options}";
	}
}
