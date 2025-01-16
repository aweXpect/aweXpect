using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches exactly <paramref name="expected" /> items.
	/// </summary>
	public static EnumerableQuantifier Exactly(int expected) => new ExactlyQuantifier(expected);

	private sealed class ExactlyQuantifier(int expected) : EnumerableQuantifier
	{
		public override string ToString() => $"exactly {expected}";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount > expected;

		/// <inheritdoc />
		public override string GetExpectation(string it, string? expectationExpression)
			=> (expected, expectationExpression is null) switch
			{
				(1, true) => "have exactly one item",
				(1, false) => $"have exactly one item {expectationExpression}",
				(_, true) => $"have exactly {expected} items",
				(_, false) => $"have exactly {expected} items {expectationExpression}"
			};

		/// <inheritdoc />
		public override ConstraintResult GetResult<TEnumerable>(TEnumerable actual,
			string it,
			string? expectationExpression,
			int matchingCount,
			int notMatchingCount,
			int? totalCount,
			string? verb)
		{
			verb ??= "were";
			if (matchingCount > expected)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GetExpectation(it, expectationExpression),
					(totalCount.HasValue, expectationExpression is null) switch
					{
						(true, true) => $"found {matchingCount}",
						(true, false) => $"{matchingCount} of {totalCount} {verb}",
						(false, true) => $"found at least {matchingCount}",
						(false, false) => $"at least {matchingCount} {verb}"
					});
			}

			if (totalCount.HasValue)
			{
				if (matchingCount == expected)
				{
					return new ConstraintResult.Success<TEnumerable>(actual,
						GetExpectation(it, expectationExpression));
				}

				return new ConstraintResult.Failure<TEnumerable>(actual,
					GetExpectation(it, expectationExpression),
					expectationExpression is null
						? $"found only {matchingCount}"
						: $"only {matchingCount} of {totalCount} {verb}");
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GetExpectation(it, expectationExpression),
				"could not verify, because it was not enumerated completely");
		}
	}
}
