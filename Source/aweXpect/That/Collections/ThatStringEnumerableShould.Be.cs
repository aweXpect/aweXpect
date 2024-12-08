using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatStringEnumerableShould
{
	/// <summary>
	///     Verifies that the actual enumerable matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionBeTypeResult<IEnumerable<string>, IThat<IEnumerable<string>>>
		Be(this IThat<IEnumerable<string>> source,
			IEnumerable<string> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new StringCollectionBeTypeResult<IEnumerable<string>, IThat<IEnumerable<string>>>(source
				.ExpectationBuilder.AddConstraint(it
					=> new BeConstraint(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	private readonly struct BeConstraint(
		string it,
		string expectedExpression,
		IEnumerable<string> expected,
		StringEqualityOptions options,
		CollectionMatchOptions matchOptions)
		: IContextConstraint<IEnumerable<string>>
	{
		public ConstraintResult IsMetBy(IEnumerable<string> actual, IEvaluationContext context)
		{
			IEnumerable<string> materializedEnumerable =
				context.UseMaterializedEnumerable<string, IEnumerable<string>>(actual);
			ICollectionMatcher<string, string?> matcher = matchOptions.GetCollectionMatcher<string, string?>(expected);
			foreach (string item in materializedEnumerable)
			{
				if (matcher.Verify(it, item, options, out string? failure))
				{
					return new ConstraintResult.Failure<IEnumerable<string>>(actual, ToString(),
						failure ?? TooManyDeviationsError(materializedEnumerable));
				}
			}

			if (matcher.VerifyComplete(it, options, out string? lastFailure))
			{
				return new ConstraintResult.Failure<IEnumerable<string>>(actual, ToString(),
					lastFailure ?? TooManyDeviationsError(materializedEnumerable));
			}

			return new ConstraintResult.Success<IEnumerable<string>>(materializedEnumerable,
				ToString());
		}

		private string TooManyDeviationsError(IEnumerable<string> materializedEnumerable)
			=> $"{it} was completely different: {Formatter.Format(materializedEnumerable, FormattingOptions.MultipleLines)} had more than {2 * CollectionFormatCount} deviations compared to {Formatter.Format(expected, FormattingOptions.MultipleLines)}";

		public override string ToString()
			=> $"match collection {expectedExpression}{matchOptions}";
	}
}
