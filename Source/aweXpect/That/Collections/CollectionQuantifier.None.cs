using System.Diagnostics.CodeAnalysis;

namespace Testably.Expectations;

public abstract partial class CollectionQuantifier
{
	/// <summary>
	///     Matches no items in the collection.
	/// </summary>
	public static CollectionQuantifier None => new NoneQuantifier();

	private sealed class NoneQuantifier : CollectionQuantifier
	{
		/// <inheritdoc />
		public override string ToString(bool includeItems) =>
			includeItems ? "no items" : "none";

		/// <inheritdoc />
		protected override bool ContinueEvaluation(
			int matchingCount, int notMatchingCount,
			int? totalCount,
			[NotNullWhen(false)] out CollectionEvaluatorResult? result)
		{
			if (matchingCount > 0)
			{
				result = new CollectionEvaluatorResult(false, "at least one");
				return false;
			}

			if (notMatchingCount == totalCount)
			{
				result = new CollectionEvaluatorResult(true, "");
				return false;
			}

			result = null;
			return true;
		}
	}
}
