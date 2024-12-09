using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches exactly <paramref name="expected"/> items.
	/// </summary>
	public static EnumerableQuantifier Exactly(int expected) => new ExactlyQuantifier(expected);

	private sealed class ExactlyQuantifier(int expected) : EnumerableQuantifier
	{
		public override string ToString() => $"exactly {expected}";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount > expected;

		/// <inheritdoc />
		public override string GetExpectation(string it, ExpectationBuilder expectationBuilder)
			=> $"have exactly {expected} items {expectationBuilder}";

		/// <inheritdoc />
		public override ConstraintResult GetResult<TEnumerable>(TEnumerable actual,
			string it,
			ExpectationBuilder expectationBuilder,
			int matchingCount,
			int notMatchingCount,
			int? totalCount)
		{
			if (matchingCount > expected)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GetExpectation(it, expectationBuilder),
					$"at least {matchingCount} were");
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
					$"only {matchingCount} of {totalCount} were");
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GetExpectation(it, expectationBuilder),
				"could not verify, because it was not enumerated completely");
		}
	}
}
