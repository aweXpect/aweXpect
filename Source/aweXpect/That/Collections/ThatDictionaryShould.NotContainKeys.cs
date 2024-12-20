using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDictionaryShould
{
	/// <summary>
	///     Verifies that the dictionary contains none of the <paramref name="unexpected" /> keys.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>>> NotContainKeys<TKey, TValue>(
		this IThat<IDictionary<TKey, TValue>> source,
		params TKey[] unexpected)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotContainKeysConstraint<TKey, TValue>(it, unexpected)),
			source
		);

	private readonly struct NotContainKeysConstraint<TKey, TValue>(string it, TKey[] unexpected)
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

			List<TKey> existingKeys = unexpected.Where(actual.ContainsKey).ToList();
			if (existingKeys.Any())
			{
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(),
					$"{it} did have {Formatter.Format(existingKeys, FormattingOptions.MultipleLines)}");
			}

			return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual, ToString());
		}

		public override string ToString() => $"not have keys {Formatter.Format(unexpected)}";
	}
}
