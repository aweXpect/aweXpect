using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDictionaryShould
{
	/// <summary>
	///     Verifies that the dictionary contains all <paramref name="expected" /> values.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>>> ContainValues<TKey, TValue>(
		this IThat<IDictionary<TKey, TValue>> source,
		params TValue[] expected)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new ContainValuesConstraint<TKey, TValue>(it, expected)),
			source
		);

	private readonly struct ContainValuesConstraint<TKey, TValue>(string it, TValue[] expected)
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

			List<TValue> missingValues = expected.Where(value => !actual.ContainsValue(value)).ToList();
			if (missingValues.Any())
			{
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(),
					$"{it} did not have {Formatter.Format(missingValues, FormattingOptions.MultipleLines)} in {Formatter.Format(actual.Values, FormattingOptions.MultipleLines)}");
			}

			return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual, ToString());
		}

		public override string ToString() => $"have values {Formatter.Format(expected)}";
	}
}
