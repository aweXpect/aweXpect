using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDictionaryShould
{
	/// <summary>
	///     Verifies that the dictionary contains all <paramref name="expected" /> keys.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>>> ContainKeys<TKey, TValue>(
		this IThat<IDictionary<TKey, TValue>> source,
		params TKey[] expected)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new ContainKeysConstraint<TKey, TValue>(it, expected)),
			source
		);

	private readonly struct ContainKeysConstraint<TKey, TValue>(string it, TKey[] expected)
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

			List<TKey> missingKeys = expected.Where(key => !actual.ContainsKey(key)).ToList();
			if (missingKeys.Any())
			{
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(),
					$"{it} did not have {Formatter.Format(missingKeys, FormattingOptions.MultipleLines)} in {Formatter.Format(actual.Keys, FormattingOptions.MultipleLines)}");
			}

			return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual, ToString());
		}

		public override string ToString() => $"have keys {Formatter.Format(expected)}";
	}
}
