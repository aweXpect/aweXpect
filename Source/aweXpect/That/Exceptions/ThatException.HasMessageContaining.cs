using System;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatException
{
	/// <summary>
	///     Verifies that the actual exception has a message containing the <paramref name="expected" /> pattern.
	/// </summary>
	public static StringEqualityTypeResult<Exception?, IThat<Exception?>> HasMessageContaining(
		this IThat<Exception?> source,
		string? expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<Exception?, IThat<Exception?>>(
			source.Get().ExpectationBuilder.AddConstraint((expectationBuilder, it, grammars)
				=> new HasMessageContainingConstraint(
					expectationBuilder, it, grammars, expected, options)),
			source,
			options);
	}

	internal class HasMessageContainingConstraint(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		string? expected,
		StringEqualityOptions options)
		: ConstraintResult.WithValue<Exception?>(grammars),
			IAsyncConstraint<Exception?>
	{
		public async Task<ConstraintResult> IsMetBy(Exception? actual, CancellationToken cancellationToken)
		{
			Actual = actual;
			options.AsWildcard();
			Outcome = expected is null || await options.AreConsideredEqual(actual?.Message, $"*{expected}*")
				? Outcome.Success
				: Outcome.Failure;
			if (Outcome == Outcome.Failure)
			{
				expectationBuilder.UpdateContexts(contexts => contexts
					.Add(new ResultContext("Message", actual?.Message)));
			}

			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			ExpectationGrammars equalityGrammars = Grammars;
			if (Grammars.HasFlag(ExpectationGrammars.Active))
			{
				stringBuilder.Append("with Message containing ");
				equalityGrammars &= ~ExpectationGrammars.Active;
			}
			else if (Grammars.HasFlag(ExpectationGrammars.Nested))
			{
				stringBuilder.Append("Message contains ");
			}
			else
			{
				stringBuilder.Append("contains Message ");
			}

			stringBuilder.Append(options.GetExpectation(expected, equalityGrammars));
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(options.GetExtendedFailure(it, Grammars, Actual?.Message, expected));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			ExpectationGrammars equalityGrammars = Grammars;
			if (Grammars.HasFlag(ExpectationGrammars.Active))
			{
				stringBuilder.Append("with Message not containing ");
				equalityGrammars &= ~ExpectationGrammars.Active;
			}
			else if (Grammars.HasFlag(ExpectationGrammars.Nested))
			{
				stringBuilder.Append("Message does not contain ");
			}
			else
			{
				stringBuilder.Append("does not contain Message ");
			}

			stringBuilder.Append(options.GetExpectation(expected, equalityGrammars.Negate()));
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
}
