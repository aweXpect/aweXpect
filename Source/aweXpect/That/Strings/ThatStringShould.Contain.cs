using System;
using System.Collections.Generic;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject contains the <paramref name="expected" /> <see langword="string" />.
	/// </summary>
	public static StringCountResult<string?, IThat<string?>> Contain(
		this IThat<string?> source,
		string expected)
	{
		Quantifier? quantifier = new();
		StringEqualityOptions? options = new();
		return new StringCountResult<string?, IThat<string?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ContainConstraint(it, expected, quantifier, options)),
			source,
			quantifier,
			options);
	}

	/// <summary>
	///     Verifies that the subject contains the <paramref name="unexpected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityResult<string?, IThat<string?>> NotContain(
		this IThat<string?> source,
		string unexpected)
	{
		Quantifier? quantifier = new();
		quantifier.Exactly(0);
		StringEqualityOptions? options = new();
		return new StringEqualityResult<string?, IThat<string?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ContainConstraint(it, unexpected, quantifier, options)),
			source,
			options);
	}

	private readonly struct ContainConstraint(
		string it,
		string expected,
		Quantifier quantifier,
		StringEqualityOptions options)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<string?>(null, ToString(),
					$"{it} was <null>");
			}

			int actualCount = CountOccurrences(actual, expected, options.Comparer);
			if (quantifier.Check(actualCount) == true)
			{
				return new ConstraintResult.Success<string?>(actual, ToString());
			}

			return new ConstraintResult.Failure<string?>(actual, ToString(),
				$"{it} contained it {actualCount} times in {Formatter.Format(actual)}");
		}

		private static int CountOccurrences(string actual, string expected,
			IEqualityComparer<string> comparer)
		{
			if (expected.Length > actual.Length)
			{
				return 0;
			}

			int count = 0;
			int index = 0;
			while (index < actual.Length)
			{
				if (comparer.Equals(
					actual.Substring(index, Math.Min(expected.Length, actual.Length - index)),
					expected))
				{
					count++;
					index += expected.Length;
				}
				else
				{
					index++;
				}
			}

			return count;
		}

		/// <inheritdoc />
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
