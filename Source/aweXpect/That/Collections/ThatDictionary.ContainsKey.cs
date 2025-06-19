﻿using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDictionary
{
	/// <summary>
	///     Verifies that the dictionary contains the <paramref name="expected" /> key.
	/// </summary>
	public static ContainsValueResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>, TKey, TValue?>
		ContainsKey<TKey, TValue>(
			this IThat<IDictionary<TKey, TValue>?> source,
			TKey expected)
	{
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ContainsValueResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>, TKey, TValue?>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new ContainsKeyConstraint<TKey, TValue>(expectationBuilder, it, grammars, expected)),
			source,
			expected,
			f => f.TryGetValue(expected, out TValue? value) ? value : default
		);
	}

	/// <summary>
	///     Verifies that the dictionary does not contain the <paramref name="unexpected" /> key.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>>
		DoesNotContainKey<TKey, TValue>(
			this IThat<IDictionary<TKey, TValue>?> source,
			TKey unexpected)
	{
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new ContainsKeyConstraint<TKey, TValue>(expectationBuilder, it, grammars, unexpected).Invert()),
			source
		);
	}

	private sealed class ContainsKeyConstraint<TKey, TValue>(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		TKey expected)
		: ConstraintResult.WithNotNullValue<IDictionary<TKey, TValue>?>(it, grammars),
			IValueConstraint<IDictionary<TKey, TValue>?>
	{
		public ConstraintResult IsMetBy(IDictionary<TKey, TValue>? actual)
		{
			Actual = actual;
			Outcome = actual?.ContainsKey(expected) == true ? Outcome.Success : Outcome.Failure;
			expectationBuilder.AddCollectionContext(actual);
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("contains key ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" contained only ");
			Formatter.Format(stringBuilder, Actual?.Keys, FormattingOptions.MultipleLines);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("does not contain key ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(It).Append(" did");
	}
}
