using System;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches none items.
	/// </summary>
	public static EnumerableQuantifier None => new NoneQuantifier();

	private sealed class NoneQuantifier : EnumerableQuantifier
	{
		public override string ToString() => "none";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount > 0;

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
			if (matchingCount > 0)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator),
					totalCount.HasValue
						? $"{matchingCount} of {totalCount} {verb}"
						: $"at least one {(verb == "were" ? "was" : verb)}");
			}

			if (totalCount.HasValue)
			{
				return new ConstraintResult.Success<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator));
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GenerateExpectation(ToString(), expectationExpression, expectationGenerator),
				"could not verify, because it was not enumerated completely");
		}
	}
}
