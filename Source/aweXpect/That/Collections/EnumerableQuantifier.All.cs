using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Matches all items.
	/// </summary>
	public static EnumerableQuantifier All(ExpectationGrammars expectationGrammars = ExpectationGrammars.None)
		=> new AllQuantifier();

	private sealed class AllQuantifier : EnumerableQuantifier
	{
		public override string ToString() => "all";

		/// <inheritdoc />
		public override bool IsDeterminable(int matchingCount, int notMatchingCount)
			=> notMatchingCount > 0;

		/// <inheritdoc />
		public override bool IsSingle() => false;

		/// <inheritdoc />
		public override Outcome GetOutcome(int matchingCount, int notMatchingCount, int? totalCount)
		{
			if (notMatchingCount > 0)
			{
				return Outcome.Failure;
			}

			if (matchingCount == totalCount)
			{
				return Outcome.Success;
			}

			return Outcome.Undecided;
		}

		/// <inheritdoc />
		public override void AppendResult(StringBuilder stringBuilder,
			ExpectationGrammars grammars,
			int matchingCount,
			int notMatchingCount,
			int? totalCount,
			string? verb = null)
		{
			verb ??= "were";
			if (totalCount.HasValue)
			{
				stringBuilder.Append("only ").Append(matchingCount).Append(" of ").Append(totalCount.Value).Append(' ')
					.Append(verb);
			}
			else
			{
				stringBuilder.Append("not all ").Append(verb);
			}
		}
	}
}
