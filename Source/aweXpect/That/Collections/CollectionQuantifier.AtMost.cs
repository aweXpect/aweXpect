using System.Diagnostics.CodeAnalysis;

namespace Testably.Expectations;

public abstract partial class CollectionQuantifier
{
	/// <summary>
	///     Matches at most <paramref name="maximum" /> items.
	/// </summary>
	public static CollectionQuantifier AtMost(int maximum) => new AtMostQuantifier(maximum);

	private sealed class AtMostQuantifier(int maximum) : CollectionQuantifier
	{
		/// <inheritdoc />
		public override string ToString(bool includeItems)
		{
			string result = $"at most {maximum}";
			if (includeItems)
			{
				result += maximum == 1 ? " item" : " items";
			}

			return result;
		}

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

			if (matchingCount <= maximum && matchingCount + notMatchingCount == totalCount)
			{
				result = new CollectionEvaluatorResult(true, "");
				return false;
			}

			result = null;
			return true;
		}
	}
}
