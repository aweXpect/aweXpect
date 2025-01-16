using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDictionaryShould
{
	/// <summary>
	///     Verifies that the dictionary contains the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThatShould<IDictionary<TKey, TValue>>> ContainValue<TKey, TValue>(
		this IThatShould<IDictionary<TKey, TValue>> source,
		TValue expected)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new ContainValueConstraint<TKey, TValue>(it, expected)),
			source
		);

	/// <summary>
	///     Verifies that the dictionary does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThatShould<IDictionary<TKey, TValue>>>
		NotContainValue<TKey, TValue>(
			this IThatShould<IDictionary<TKey, TValue>> source,
			TValue unexpected)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotContainValueConstraint<TKey, TValue>(it, unexpected)),
			source
		);

	private readonly struct ContainValueConstraint<TKey, TValue>(string it, TValue expected)
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

			if (actual.ContainsValue(expected))
			{
				return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual, ToString());
			}

			return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(),
				$"{it} contained only {Formatter.Format(actual.Keys, FormattingOptions.MultipleLines)}");
		}

		public override string ToString() => $"have value {Formatter.Format(expected)}";
	}

	private readonly struct NotContainValueConstraint<TKey, TValue>(string it, TValue unexpected)
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

			if (actual.ContainsValue(unexpected))
			{
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(),
					$"{it} did");
			}

			return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual, ToString());
		}

		public override string ToString() => $"not have value {Formatter.Format(unexpected)}";
	}
}
