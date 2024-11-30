using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches all items in the enumerable.
	/// </summary>
	public static EnumerableQuantifier None => new NoneQuantifier();

	private sealed class NoneQuantifier : EnumerableQuantifier
	{
		public override string ToString() => "none";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount > 0;

		/// <inheritdoc />
		public override string GetExpectation(string it, ExpectationBuilder expectationBuilder)
			=> $"have no items {expectationBuilder}";

		/// <inheritdoc />
		public override ConstraintResult GetResult<TEnumerable>(TEnumerable actual,
			string it,
			ExpectationBuilder expectationBuilder,
			int matchingCount,
			int notMatchingCount,
			int? totalCount)
		{
			if (matchingCount > 0)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GetExpectation(it, expectationBuilder),
					totalCount.HasValue
						? $"{matchingCount} of {totalCount} were"
						: "at least one was");
			}

			if (totalCount.HasValue)
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
