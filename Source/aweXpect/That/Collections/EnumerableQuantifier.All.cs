using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches all items.
	/// </summary>
	public static EnumerableQuantifier All(ExpectationForm expectationForm = ExpectationForm.Default) => new AllQuantifier(expectationForm);

	private sealed class AllQuantifier(ExpectationForm expectationForm) : EnumerableQuantifier
	{
		public override string ToString() => "all";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> notMatchingCount > 0;

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
			if (notMatchingCount > 0)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationForm),
					totalCount.HasValue
						? $"only {matchingCount} of {totalCount} {verb}"
						: $"not all {verb}");
			}

			if (matchingCount == totalCount)
			{
				return new ConstraintResult.Success<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationForm));
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationForm),
				"could not verify, because it was not enumerated completely");
		}
	}
}
