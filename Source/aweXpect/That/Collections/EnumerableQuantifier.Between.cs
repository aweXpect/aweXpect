using System;
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
		public override bool IsSingle() => false;

		/// <inheritdoc />
		public override ConstraintResult GetResult<TEnumerable>(TEnumerable actual,
			string it,
			string? expectationExpression,
			int matchingCount,
			int notMatchingCount,
			int? totalCount,
			string? verb,
			Func<string, string?, string>? expectationGenerator = null)
		{
			verb ??= "were";
			if (matchingCount > maximum)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator),
					(totalCount.HasValue, expectationExpression is null) switch
					{
						(true, true) => $"found {matchingCount}",
						(true, false) => $"{matchingCount} of {totalCount} {verb}",
						(false, true) => $"found at least {matchingCount}",
						(false, false) => $"at least {matchingCount} {verb}"
					});
			}

			if (matchingCount >= minimum)
			{
				return new ConstraintResult.Success<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator));
			}

			if (totalCount.HasValue)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator),
					expectationExpression is null
						? $"found only {matchingCount}"
						: $"only {matchingCount} of {totalCount} {verb}"
				);
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GenerateExpectation(ToString(), expectationExpression, expectationGenerator),
				"could not verify, because it was not enumerated completely");
		}
	}
}
