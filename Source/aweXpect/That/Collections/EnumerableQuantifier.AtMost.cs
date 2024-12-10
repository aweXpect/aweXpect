using aweXpect.Core;
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
		public override string GetExpectation(string it, ExpectationBuilder? expectationBuilder)
			=> (maximum, expectationBuilder is null) switch
			{
				(1, true) => "have at most one item",
				(1, false) => $"have at most one item {expectationBuilder}",
				(_, true) => $"have at most {maximum} items",
				(_, false) => $"have at most {maximum} items {expectationBuilder}"
			};

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
