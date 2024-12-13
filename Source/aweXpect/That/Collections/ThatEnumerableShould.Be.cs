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

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that the collection matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static CollectionBeResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		Be<TItem>(
			this IThat<IEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new CollectionBeResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(source.ExpectationBuilder
				.AddConstraint(it
					=> new BeConstraint<TItem>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	private readonly struct BeConstraint<TItem>(
		string it,
		string expectedExpression,
		IEnumerable<TItem> expected,
		ObjectEqualityOptions options,
		CollectionMatchOptions matchOptions)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			ICollectionMatcher<TItem, object?> matcher = matchOptions.GetCollectionMatcher<TItem, object?>(expected);
			foreach (TItem item in materializedEnumerable)
			{
				if (matcher.Verify(it, item, options, out string? failure))
				{
					return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
						failure ?? TooManyDeviationsError(materializedEnumerable));
				}
			}

			if (matcher.VerifyComplete(it, options, out string? lastFailure))
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
					lastFailure ?? TooManyDeviationsError(materializedEnumerable));
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		private string TooManyDeviationsError(IEnumerable<TItem> materializedEnumerable)
			=> $"{it} was completely different: {Formatter.Format(materializedEnumerable, FormattingOptions.MultipleLines)} had more than {2 * Customization.Customize.Formatting.MaximumNumberOfCollectionItems} deviations compared to {Formatter.Format(expected, FormattingOptions.MultipleLines)}";

		public override string ToString()
			=> $"match collection {expectedExpression}{matchOptions}";
	}
}
