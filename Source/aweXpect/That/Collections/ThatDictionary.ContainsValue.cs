﻿using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDictionary
{
	/// <summary>
	///     Verifies that the dictionary contains the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>> ContainsValue<TKey,
		TValue>(
		this IThat<IDictionary<TKey, TValue>?> source,
		TValue expected)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new ContainValueConstraint<TKey, TValue>(it, expected)),
			source
		);

	/// <summary>
	///     Verifies that the dictionary does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>>
		DoesNotContainValue<TKey, TValue>(
			this IThat<IDictionary<TKey, TValue>?> source,
			TValue unexpected)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NotContainValueConstraint<TKey, TValue>(it, unexpected)),
			source
		);

	private readonly struct ContainValueConstraint<TKey, TValue>(string it, TValue expected)
		: IValueConstraint<IDictionary<TKey, TValue>?>
	{
		public ConstraintResult IsMetBy(IDictionary<TKey, TValue>? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was <null>");
			}

			if (actual.ContainsValue(expected))
			{
				return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual, ToString());
			}

			return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(),
				$"{it} contained only {Formatter.Format(actual.Keys, FormattingOptions.MultipleLines)}");
		}

		public override string ToString() => $"contains value {Formatter.Format(expected)}";
	}

	private readonly struct NotContainValueConstraint<TKey, TValue>(string it, TValue unexpected)
		: IValueConstraint<IDictionary<TKey, TValue>?>
	{
		public ConstraintResult IsMetBy(IDictionary<TKey, TValue>? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was <null>");
			}

			if (actual.ContainsValue(unexpected))
			{
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(),
					$"{it} did");
			}

			return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual, ToString());
		}

		public override string ToString() => $"does not contain value {Formatter.Format(unexpected)}";
	}
}
