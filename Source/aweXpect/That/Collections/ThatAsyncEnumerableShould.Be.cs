#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that the actual collection matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static CollectionBeResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		Be<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new CollectionBeResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
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
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			ICollectionMatcher<TItem, object?> matcher = matchOptions.GetCollectionMatcher<TItem, object?>(expected);
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				if (matcher.Verify(it, item, options, out string? failure))
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
						failure ?? await TooManyDeviationsError(materializedEnumerable));
				}
			}

			if (matcher.VerifyComplete(it, options, out string? lastFailure))
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
					lastFailure ?? await TooManyDeviationsError(materializedEnumerable));
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}


		private async Task<string> TooManyDeviationsError(IAsyncEnumerable<TItem> materializedEnumerable)
		{
			StringBuilder sb = new();
			sb.Append(it);
			sb.Append(" was completely different: [");
			int count = 0;
			await foreach (TItem item in materializedEnumerable)
			{
				if (count++ >= CollectionFormatCount)
				{
					break;
				}

				sb.AppendLine();
				sb.Append("  ");
				Formatter.Format(sb, item);
				sb.Append(",");
			}

			if (count > CollectionFormatCount)
			{
				sb.AppendLine();
				sb.Append("  …,");
			}

			sb.Length--;
			sb.AppendLine();
			sb.Append("] had more than ");
			sb.Append(2 * CollectionFormatCount);
			sb.Append(" deviations compared to ");
			Formatter.Format(sb, expected.Take(CollectionFormatCount + 1), FormattingOptions.MultipleLines);
			return sb.ToString();
		}

		public override string ToString()
			=> $"match collection {expectedExpression}{matchOptions}";
	}
}
#endif
