using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches exactly <paramref name="expected" /> items.
	/// </summary>
	public static EnumerableQuantifier Exactly(int expected, ExpectationGrammars expectationGrammars = ExpectationGrammars.None)
		=> new ExactlyQuantifier(expected, expectationGrammars);

	private sealed class ExactlyQuantifier(int expected, ExpectationGrammars expectationGrammars) : EnumerableQuantifier
	{
		public override string ToString() 
			=> expected switch
			{
				1 => "exactly one",
				_ => $"exactly {expected}",
			};

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount > expected;

		/// <inheritdoc />
		public override bool IsSingle() => expected == 1;

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
			if (matchingCount > expected)
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
				if (matchingCount == expected)
				{
					return new ConstraintResult.Success<TEnumerable>(actual,
						GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars));
				}

				return new ConstraintResult.Failure<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
					expectationExpression is null
						? $"found only {matchingCount}"
						: $"only {matchingCount} of {totalCount} {verb}");
			}

			return new ConstraintResult.Failure<TEnumerable>(actual,
				GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
				"could not verify, because it was not enumerated completely");
		}
	}
}
