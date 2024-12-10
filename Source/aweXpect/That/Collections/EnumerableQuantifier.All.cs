using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches all items.
	/// </summary>
	public static EnumerableQuantifier All => new AllQuantifier();

	private sealed class AllQuantifier : EnumerableQuantifier
	{
		public override string ToString() => "all";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> notMatchingCount > 0;

		/// <inheritdoc />
		public override string GetExpectation(string it, ExpectationBuilder? expectationBuilder)
			=> expectationBuilder == null
				? "have all items"
				: $"have all items {expectationBuilder}";

		/// <inheritdoc />
		public override ConstraintResult GetResult<TEnumerable>(TEnumerable actual,
			string it,
			ExpectationBuilder? expectationBuilder,
			int matchingCount,
			int notMatchingCount,
			int? totalCount)
		{
			if (notMatchingCount > 0)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GetExpectation(it, expectationBuilder),
					totalCount.HasValue
						? $"only {matchingCount} of {totalCount} were"
						: "not all were");
			}

			if (matchingCount == totalCount)
			{
				return new ConstraintResult.Success<TEnumerable>(actual,
					GetExpectation(it, expectationBuilder));
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GetExpectation(it, expectationBuilder),
				"could not verify, because it was not enumerated completely");
		}
	}
}
