using System.Collections.Generic;
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainsKeyConstraint<TKey, TValue>(it, expected)),
			source,
			expected,
			f => f.TryGetValue(expected, out TValue? value) ? value : default
		);

	/// <summary>
	///     Verifies that the dictionary does not contain the <paramref name="unexpected" /> key.
	/// </summary>
	public static AndOrResult<IDictionary<TKey, TValue>, IThat<IDictionary<TKey, TValue>?>>
		DoesNotContainKey<TKey, TValue>(
			this IThat<IDictionary<TKey, TValue>?> source,
			TKey unexpected)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new DoesNotContainKeyConstraint<TKey, TValue>(it, unexpected)),
			source
		);

	private readonly struct ContainsKeyConstraint<TKey, TValue>(string it, TKey expected)
		: IValueConstraint<IDictionary<TKey, TValue>?>
	{
		public ConstraintResult IsMetBy(IDictionary<TKey, TValue>? actual)
		{
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

		public override string ToString() => $"contains key {Formatter.Format(expected)}";
	}

	private readonly struct DoesNotContainKeyConstraint<TKey, TValue>(string it, TKey unexpected)
		: IValueConstraint<IDictionary<TKey, TValue>?>
	{
		public ConstraintResult IsMetBy(IDictionary<TKey, TValue>? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was <null>");
			}

			if (actual.ContainsKey(unexpected))
			{
				return new ConstraintResult.Failure<IDictionary<TKey, TValue>>(actual, ToString(),
					$"{it} did");
			}

			return new ConstraintResult.Success<IDictionary<TKey, TValue>>(actual, ToString());
		}

		public override string ToString() => $"does not contain key {Formatter.Format(unexpected)}";
	}
}
