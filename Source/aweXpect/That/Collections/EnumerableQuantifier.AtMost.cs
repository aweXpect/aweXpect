using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches at most <paramref name="maximum" /> items.
	/// </summary>
	public static EnumerableQuantifier AtMost(int maximum) => new AtMostQuantifier(maximum);

	private sealed class AtMostQuantifier(int maximum) : EnumerableQuantifier
	{
		public override string ToString() => $"at most {maximum}";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount > maximum;

		/// <inheritdoc />
		public override string GetExpectation(string it, string? expectationExpression)
			=> (maximum, expectationExpression is null) switch
			{
				(1, true) => "have at most one item",
				(1, false) => $"have at most one item {expectationExpression}",
				(_, true) => $"have at most {maximum} items",
				(_, false) => $"have at most {maximum} items {expectationExpression}"
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
			if (matchingCount > maximum)
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
				return new ConstraintResult.Success<TEnumerable>(actual,
					GetExpectation(it, expectationExpression));
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GetExpectation(it, expectationExpression),
				"could not verify, because it was not enumerated completely");
		}
	}
}
