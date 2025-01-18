using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches at least <paramref name="minimum" /> items.
	/// </summary>
	public static EnumerableQuantifier AtLeast(int minimum) => new AtLeastQuantifier(minimum);

	private sealed class AtLeastQuantifier(int minimum) : EnumerableQuantifier
	{
		public override string ToString() => $"at least {minimum}";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount >= minimum;

		/// <inheritdoc />
		public override string GetExpectation(string it, string? expectationExpression)
			=> (minimum, expectationExpression is null) switch
			{
				(1, true) => "have at least one item",
				(1, false) => $"have at least one item {expectationExpression}",
				(_, true) => $"have at least {minimum} items",
				(_, false) => $"have at least {minimum} items {expectationExpression}"
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
			if (matchingCount >= minimum)
			{
				return new ConstraintResult.Success<TEnumerable>(actual,
					GetExpectation(it, expectationExpression));
			}

			if (totalCount.HasValue)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GetExpectation(it, expectationExpression),
					expectationExpression == null
						? $"found only {matchingCount}"
						: $"only {matchingCount} of {totalCount} {verb}");
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GetExpectation(it, expectationExpression),
				"could not verify, because it was not enumerated completely");
		}
	}
}
