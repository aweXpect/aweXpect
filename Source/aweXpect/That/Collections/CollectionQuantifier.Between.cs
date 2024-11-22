using System.Diagnostics.CodeAnalysis;

namespace Testably.Expectations;

public abstract partial class CollectionQuantifier
{
	/// <summary>
	///     Matches between <paramref name="minimum" /> and <paramref name="maximum" /> items.
	/// </summary>
	public static CollectionQuantifier Between(int minimum, int maximum)
		=> new BetweenQuantifier(minimum, maximum);

	private sealed class BetweenQuantifier(int minimum, int maximum) : CollectionQuantifier
	{
		/// <inheritdoc />
		public override string ToString(bool includeItems)
			=> $"between {minimum} and {maximum}{(includeItems ? " items" : "")}";

		/// <inheritdoc />
		protected override bool ContinueEvaluation(
			int matchingCount, int notMatchingCount,
			int? totalCount,
			[NotNullWhen(false)] out CollectionEvaluatorResult? result)
		{
			if (matchingCount > maximum)
			{
				result = new CollectionEvaluatorResult(false, totalCount.HasValue
					? $"at least {matchingCount} of {totalCount}"
					: $"at least {matchingCount}");
				return false;
			}

			if (totalCount != null && matchingCount + notMatchingCount == totalCount)
			{
				if (matchingCount >= minimum)
				{
					result = new CollectionEvaluatorResult(true, "");
					return false;
				}

				result = new CollectionEvaluatorResult(false, $"only {matchingCount}");
				return false;
			}

			result = null;
			return true;
		}
	}
}
