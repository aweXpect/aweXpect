using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDictionaryShould
{
	/// <summary>
	///     Verifies that the dictionary contains the <paramref name="expected" /> key.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>>> HaveKey<TKey, TValue>(
		this IThat<IDictionary<TKey, TValue>> source,
		TKey expected)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new HaveKeyConstraint<TKey, TValue>(it, expected)),
			source
		);

	private readonly struct HaveKeyConstraint<TKey, TValue>(string it, TKey expected)
		: IValueConstraint<IDictionary<TKey, TValue>>
	{
		public ConstraintResult IsMetBy(IDictionary<TKey, TValue> actual)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was <null>");
			}

			if (actual.ContainsKey(expected))
			{
				return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual, ToString());
			}

			return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(),
				$"{it} contained only {Formatter.Format(actual.Keys, FormattingOptions.MultipleLines)}");
		}

		public override string ToString() => $"have key {Formatter.Format(expected)}";
	}
}
