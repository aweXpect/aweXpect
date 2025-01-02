using aweXpect.Core;
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
		public override string GetExpectation(string it, ExpectationBuilder? expectationBuilder)
			=> (expected, expectationBuilder is null) switch
			{
				(1, true) => "have exactly one item",
				(1, false) => $"have exactly one item {expectationBuilder}",
				(_, true) => $"have exactly {expected} items",
				(_, false) => $"have exactly {expected} items {expectationBuilder}"
			};

		/// <inheritdoc />
		public override ConstraintResult GetResult<TEnumerable>(TEnumerable actual,
			string it,
			ExpectationBuilder? expectationBuilder,
			int matchingCount,
			int notMatchingCount,
			int? totalCount)
		{
			if (matchingCount > expected)
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

			if (totalCount.HasValue)
			{
				if (matchingCount == expected)
				{
					return new ConstraintResult.Success<TEnumerable>(actual,
						GetExpectation(it, expectationBuilder));
				}

				return new ConstraintResult.Failure<TEnumerable>(actual,
					GetExpectation(it, expectationBuilder),
					expectationBuilder is null
						? $"found only {matchingCount}"
						: $"only {matchingCount} of {totalCount} were");
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GetExpectation(it, expectationBuilder),
				"could not verify, because it was not enumerated completely");
		}
	}
}
