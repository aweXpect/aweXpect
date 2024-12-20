using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDictionaryShould
{
	/// <summary>
	///     Verifies that the dictionary contains none of the <paramref name="unexpected" /> values.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>>> NotHaveValues<TKey, TValue>(
		this IThat<IDictionary<TKey, TValue>> source,
		params TValue[] unexpected)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotHaveValuesConstraint<TKey, TValue>(it, unexpected)),
			source
		);

	private readonly struct NotHaveValuesConstraint<TKey, TValue>(string it, TValue[] unexpected)
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

			List<TValue> existingValues = unexpected.Where(actual.ContainsValue).ToList();
			if (existingValues.Any())
			{
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(),
					$"{it} did have {Formatter.Format(existingValues, FormattingOptions.MultipleLines)}");
			}

			return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual, ToString());
		}

		public override string ToString() => $"not have values {Formatter.Format(unexpected)}";
	}
}
