using System;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches between <paramref name="minimum" /> and <paramref name="maximum" /> items.
	/// </summary>
	public static EnumerableQuantifier Between(int minimum, int maximum,
		ExpectationGrammars expectationGrammars = ExpectationGrammars.None)
		=> new BetweenQuantifier(minimum, maximum, expectationGrammars);

	private sealed class BetweenQuantifier(int minimum, int maximum, ExpectationGrammars expectationGrammars)
		: EnumerableQuantifier
	{
		public override string ToString() => $"between {minimum} and {maximum}";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount > maximum;

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
			if (matchingCount > maximum)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
					(totalCount.HasValue, expectationExpression is null) switch
					{
						(true, true) => $"found {matchingCount}",
						(true, false) => $"{matchingCount} of {totalCount} {verb}",
						(false, true) => $"found at least {matchingCount}",
						(false, false) => $"at least {matchingCount} {verb}",
					});
			}

			if (matchingCount >= minimum)
			{
				return new ConstraintResult.Success<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars));
			}

			if (totalCount.HasValue)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
					expectationExpression is null
						? $"found only {matchingCount}"
						: $"only {matchingCount} of {totalCount} {verb}"
				);
			}

			return new UndecidedResult<TEnumerable>(actual,
				GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
				"could not verify, because it was not enumerated completely");
		}

		/// <inheritdoc />
		public override Outcome GetOutcome(int matchingCount, int notMatchingCount, int? totalCount)
		{
			if (matchingCount > maximum)
			{
				return Outcome.Failure;
			}

			if (matchingCount >= minimum)
			{
				return Outcome.Success;
			}

			if (totalCount.HasValue)
			{
				return Outcome.Failure;
			}

			return Outcome.Undecided;
		}

		/// <inheritdoc />
		public override void AppendResult(StringBuilder stringBuilder,
			ExpectationGrammars grammars,
			int matchingCount,
			int notMatchingCount, int? totalCount)
		{
			if (totalCount.HasValue)
			{
				if (matchingCount > maximum)
				{
					stringBuilder.Append("found ").Append(matchingCount);
				}
				else
				{
					stringBuilder.Append("found only ").Append(matchingCount);
				}
			}
			else if (matchingCount > maximum)
			{
				stringBuilder.Append("found at least ").Append(matchingCount);
			}
		}
	}
}
