﻿using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject is equal to <paramref name="expected" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> IsEqualTo(
		this IThat<string?> source,
		string? expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.Get().ExpectationBuilder.AddConstraint((expectationBuilder, it, grammars) =>
				new IsEqualToConstraint(expectationBuilder, it, grammars, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to <paramref name="unexpected" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> IsNotEqualTo(
		this IThat<string?> source,
		string? unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.Get().ExpectationBuilder.AddConstraint((expectationBuilder, it, grammars) =>
				new IsEqualToConstraint(expectationBuilder, it, grammars, unexpected, options).Invert()),
			source,
			options);
	}

	private sealed class IsEqualToConstraint(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		string? expected,
		StringEqualityOptions options)
		: ConstraintResult.WithEqualToValue<string?>(it, grammars, expected is null),
			IAsyncConstraint<string?>
	{
		public async Task<ConstraintResult> IsMetBy(string? actual, CancellationToken cancellationToken)
		{
			Actual = actual;
			Outcome = await options.AreConsideredEqual(actual, expected) ? Outcome.Success : Outcome.Failure;
			expectationBuilder.UpdateContexts(contexts => contexts
				.Add(new ResultContext("Actual", actual)));
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExpectation(expected, Grammars | ExpectationGrammars.Active));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(It, Grammars, Actual, expected)
				.Indent(indentation, false));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExpectation(expected, Grammars | ExpectationGrammars.Active));

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(It, Grammars, Actual, expected)
				.Indent(indentation, false));
	}
}
