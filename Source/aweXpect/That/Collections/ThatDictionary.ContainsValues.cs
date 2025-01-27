using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDictionary
{
	/// <summary>
	///     Verifies that the dictionary contains all <paramref name="expected" /> values.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>> ContainsValues<TKey,
		TValue>(
		this IThat<IDictionary<TKey, TValue>?> source,
		params TValue[] expected)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ContainValuesConstraint<TKey, TValue>(it, expected)),
			source
		);

	/// <summary>
	///     Verifies that the dictionary contains none of the <paramref name="unexpected" /> values.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>>
		DoesNotContainValues<TKey,
			TValue>(
			this IThat<IDictionary<TKey, TValue>?> source,
			params TValue[] unexpected)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NotContainValuesConstraint<TKey, TValue>(it, unexpected)),
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

	private readonly struct NotContainValuesConstraint<TKey, TValue>(string it, TValue[] unexpected)
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
