using System.Diagnostics.CodeAnalysis;

namespace Testably.Expectations;

public abstract partial class CollectionQuantifier
{
	/// <summary>
	///     Matches all items in the collection.
	/// </summary>
	public static CollectionQuantifier All => new AllQuantifier();

	private sealed class AllQuantifier : CollectionQuantifier
	{
		/// <inheritdoc />
		public override string ToString(bool includeItems)
			=> $"all{(includeItems ? " items" : "")}";

		/// <inheritdoc />
		protected override bool ContinueEvaluation(
			int matchingCount, int notMatchingCount,
			int? totalCount,
			[NotNullWhen(false)] out CollectionEvaluatorResult? result)
		{
			if (notMatchingCount > 0)
			{
				result = new CollectionEvaluatorResult(false, totalCount.HasValue
					? $"not all of {totalCount}"
					: "not all");
				return false;
			}

			if (matchingCount == totalCount)
			{
				result = new CollectionEvaluatorResult(true, "");
				return false;
			}

			result = null;
			return true;
		}
	}
}
