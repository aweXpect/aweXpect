using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches at most <paramref name="maximum" /> items.
	/// </summary>
	public static EnumerableQuantifier AtMost(int maximum,
		ExpectationGrammars expectationGrammars = ExpectationGrammars.None)
		=> new AtMostQuantifier(maximum, expectationGrammars);

	private sealed class AtMostQuantifier(int maximum, ExpectationGrammars expectationGrammars) : EnumerableQuantifier
	{
		public override string ToString()
			=> maximum switch
			{
				1 => "at most one",
				_ => $"at most {maximum}"
			};

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount > maximum;

		/// <inheritdoc />
		public override bool IsSingle() => maximum == 1;

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
			if (matchingCount > maximum)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
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
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars));
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
				"could not verify, because it was not enumerated completely");
		}
	}
}
