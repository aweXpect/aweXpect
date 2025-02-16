using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches none items.
	/// </summary>
	public static EnumerableQuantifier None(ExpectationGrammars expectationGrammars = ExpectationGrammars.None)
		=> new NoneQuantifier(expectationGrammars);

	private sealed class NoneQuantifier(ExpectationGrammars expectationGrammars) : EnumerableQuantifier
	{
		public override string ToString()
			=> (expectationGrammars.HasFlag(ExpectationGrammars.Nested),
					expectationGrammars.HasFlag(ExpectationGrammars.Plural)) switch
				{
					(true, _) => "none",
					(_, true) => "no",
					_ => "none",
				};

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
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
					totalCount.HasValue
						? $"{matchingCount} of {totalCount} {verb}"
						: $"at least one {(verb == "were" ? "was" : verb)}");
			}

			if (totalCount.HasValue)
			{
				return new ConstraintResult.Success<TEnumerable>(actual,
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars));
			}

			return new UndecidedResult<TEnumerable>(actual,
				GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationGrammars),
				"could not verify, because it was not enumerated completely");
		}
	}
}
