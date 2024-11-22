using System.Collections.Generic;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStringEnumerableShould
{
	/// <summary>
	///     Verifies that the actual collection contains the <paramref name="expected" /> value.
	/// </summary>
	public static StringCountResult<IEnumerable<string>, IThat<IEnumerable<string>>> Contain(
		this IThat<IEnumerable<string>> source,
		string expected)
	{
		Quantifier quantifier = new();
		StringEqualityOptions options = new();
		return new StringCountResult<IEnumerable<string>, IThat<IEnumerable<string>>>(source
				.ExpectationBuilder
				.AddConstraint(it => new ContainValueConstraint(it, expected, quantifier, options)),
			source,
			quantifier,
			options);
	}

	private readonly struct ContainValueConstraint(
		string it,
		string expected,
		Quantifier quantifier,
		StringEqualityOptions options)
		: IValueConstraint<IEnumerable<string>>
	{
		public ConstraintResult IsMetBy(IEnumerable<string> actual)
		{
			IEqualityComparer<string> comparer = options.Comparer;
			int count = 0;
			foreach (string item in actual)
			{
				if (comparer.Equals(item, expected))
				{
					count++;

					if (quantifier.Check(count) == false)
					{
						return new ConstraintResult.Failure<IEnumerable<string>>(actual, ToString(),
							$"{it} contained it at least {count} times in {Formatter.Format(actual)}");
					}
				}
			}

			if (quantifier.Check(count) == true)
			{
				return new ConstraintResult.Success<IEnumerable<string>>(actual,
					ToString());
			}

			return new ConstraintResult.Failure<IEnumerable<string>>(actual, ToString(),
				$"{it} contained it {count} times in {Formatter.Format(actual)}");
		}

		public override string ToString()
		{
			string quantifierString = quantifier.ToString();
			if (quantifierString == "never")
			{
				return $"not contain {Formatter.Format(expected)}{options}";
			}

			return $"contain {Formatter.Format(expected)} {quantifier}{options}";
		}
	}
}
