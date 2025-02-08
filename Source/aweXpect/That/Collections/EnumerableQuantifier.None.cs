﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches none items.
	/// </summary>
	public static EnumerableQuantifier None(ExpectationForm expectationForm = ExpectationForm.Default) => new NoneQuantifier(expectationForm);

	private sealed class NoneQuantifier(ExpectationForm expectationForm) : EnumerableQuantifier
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
					GenerateExpectation(ToString(), expectationExpression, expectationGenerator, expectationForm),
					totalCount.HasValue
						? $"{matchingCount} of {totalCount} {verb}"
						: $"at least one {(verb == "were" ? "was" : verb)}");
			}

			if (totalCount.HasValue)
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
