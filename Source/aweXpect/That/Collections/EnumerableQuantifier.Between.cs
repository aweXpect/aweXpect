﻿using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches between <paramref name="minimum" /> and <paramref name="maximum" /> items.
	/// </summary>
	public static EnumerableQuantifier Between(int minimum, int maximum) => new BetweenQuantifier(minimum, maximum);

	private sealed class BetweenQuantifier(int minimum, int maximum) : EnumerableQuantifier
	{
		public override string ToString() => $"between {minimum} and {maximum}";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount > maximum;

		/// <inheritdoc />
		public override string GetExpectation(string it, ExpectationBuilder? expectationBuilder)
			=> expectationBuilder is null
				? $"have between {minimum} and {maximum} items"
				: $"have between {minimum} and {maximum} items {expectationBuilder}";

		/// <inheritdoc />
		public override ConstraintResult GetResult<TEnumerable>(TEnumerable actual,
			string it,
			ExpectationBuilder? expectationBuilder,
			int matchingCount,
			int notMatchingCount,
			int? totalCount)
		{
			if (matchingCount > maximum)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GetExpectation(it, expectationBuilder),
					(totalCount.HasValue, expectationBuilder is null) switch
					{
						(true, true) => $"found {matchingCount}",
						(true, false) => $"{matchingCount} of {totalCount} were",
						(false, true) => $"found at least {matchingCount}",
						(false, false) => $"at least {matchingCount} were"
					});
			}

			if (matchingCount >= minimum)
			{
				return new ConstraintResult.Success<TEnumerable>(actual,
					GetExpectation(it, expectationBuilder));
			}

			if (totalCount.HasValue)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GetExpectation(it, expectationBuilder),
					expectationBuilder is null
						? $"found only {matchingCount}"
						: $"only {matchingCount} of {totalCount} were"
				);
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GetExpectation(it, expectationBuilder),
				"could not verify, because it was not enumerated completely");
		}
	}
}
