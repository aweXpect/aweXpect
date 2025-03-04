using System;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches at least <paramref name="minimum" /> items.
	/// </summary>
	public static EnumerableQuantifier AtLeast(int minimum,
		ExpectationGrammars expectationGrammars = ExpectationGrammars.None)
		=> new AtLeastQuantifier(minimum, expectationGrammars);

	private sealed class AtLeastQuantifier(int minimum, ExpectationGrammars expectationGrammars) : EnumerableQuantifier
	{
		public override string ToString()
			=> minimum switch
			{
				1 => "at least one",
				_ => $"at least {minimum}",
			};

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> matchingCount >= minimum;

		/// <inheritdoc />
		public override bool IsSingle() => minimum == 1;

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
			if (matchingCount >= minimum)
			{
				return new ConstraintResult.Success<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars));
			}

			if (totalCount.HasValue)
			{
				return new ConstraintResult.Failure<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
					expectationExpression == null
						? $"found only {matchingCount}"
						: $"only {matchingCount} of {totalCount} {verb}");
			}

			return new UndecidedResult<TEnumerable>(actual,
				GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
				"could not verify, because it was not enumerated completely");
		}

		/// <inheritdoc />
		public override Outcome GetOutcome(int matchingCount, int notMatchingCount, int? totalCount)
		{
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
			stringBuilder.Append("found only ").Append(matchingCount);
		}
	}
}
