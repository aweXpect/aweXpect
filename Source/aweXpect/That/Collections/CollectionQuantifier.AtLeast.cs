using System.Diagnostics.CodeAnalysis;

namespace Testably.Expectations;

public abstract partial class CollectionQuantifier
{
	/// <summary>
	///     Matches at least <paramref name="minimum" /> items.
	/// </summary>
	public static CollectionQuantifier AtLeast(int minimum) => new AtLeastQuantifier(minimum);

	private sealed class AtLeastQuantifier(int minimum) : CollectionQuantifier
	{
		/// <inheritdoc />
		public override string ToString(bool includeItems)
		{
			string result = $"at least {minimum}";
			if (includeItems)
			{
				result += minimum == 1 ? " item" : " items";
			}

			return result;
		}

		/// <inheritdoc />
		protected override bool ContinueEvaluation(
			int matchingCount, int notMatchingCount,
			int? totalCount,
			[NotNullWhen(false)] out CollectionEvaluatorResult? result)
		{
			if (matchingCount < minimum && matchingCount + notMatchingCount == totalCount)
			{
				result = new CollectionEvaluatorResult(false,
					$"only {matchingCount} of {totalCount}");
				return false;
			}

			if (matchingCount >= minimum)
			{
				result = new CollectionEvaluatorResult(true, "");
				return false;
			}

			result = null;
			return true;
		}
	}
}
