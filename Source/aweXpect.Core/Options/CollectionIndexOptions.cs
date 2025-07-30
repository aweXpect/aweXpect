using System;

namespace aweXpect.Options;

/// <summary>
///     Options for limitations on a collection index.
/// </summary>
public class CollectionIndexOptions
{
	private string _description = "";
	private Func<int, int?, bool?> _isIndexMatch = (_, _) => true;
	private bool _matchesOnlySingleIndex;

	/// <summary>
	///     Flag indicating if the expected index is checked from the end of the collection.
	/// </summary>
	public bool FromEnd { get; private set; }

	/// <summary>
	///     Checks if the <paramref name="index" /> is in range.
	/// </summary>
	/// <returns>
	///     <see langword="true" />, if the <paramref name="index" /> is in range, <see langword="null" />,
	///     if the <paramref name="index" /> is not in range, but could be in range for a larger index,
	///     otherwise <see langword="false" /> when the <paramref name="index" /> is not in range
	///     and will also not be in range for larger values.
	/// </returns>
	public bool? DoesIndexMatch(int index, int? count)
		=> _isIndexMatch.Invoke(index, count);

	/// <summary>
	///     Flag indicating, if only a single index is considered in range.
	/// </summary>
	public bool MatchesOnlySingleIndex()
		=> _matchesOnlySingleIndex;

	/// <summary>
	///     Specifies, that the expected index is checked from the end of the collection.
	/// </summary>
	public void SetFromEnd() => FromEnd = true;

	/// <summary>
	///     Set the expected index to be <paramref name="index" />.
	/// </summary>
	public void SetIndex(int index)
	{
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(index), "The index must be greater than or equal to 0.");
		}

		_isIndexMatch = (idx, count) =>
		{
			switch (FromEnd)
			{
				case true when
					count is not null &&
					count - idx - 1 == index:
				case false when
					idx == index:
					return true;
				default:
					return null;
			}
		};
		_matchesOnlySingleIndex = true;
		_description = $" at index {index}";
	}

	/// <summary>
	///     Set the checked index to be a match depending on the <paramref name="isIndexMatch" /> function.
	///     <para />
	///     The <paramref name="matchesOnlySingleIndex" /> parameter specifies, if only a single index could match the
	///     function, or if it could match multiple indices.
	/// </summary>
	/// <remarks>
	///     The <paramref name="isIndexMatch" /> receives two parameters,
	///     the first being the index to check, the second the total number of items
	///     and is expected to return <see langword="true" /> when the index matches,
	///     <see langword="null" /> when the index does not match, but could match for larger values and otherwise
	///     <see langword="false" />, when it does not match and will not match for larger values.
	/// </remarks>
	public void SetIndexMatch(Func<int, int?, bool?> isIndexMatch, bool matchesOnlySingleIndex, string description)
	{
		_isIndexMatch = isIndexMatch;
		_matchesOnlySingleIndex = matchesOnlySingleIndex;
		_description = description;
	}

	/// <summary>
	///     Returns the description of the <see cref="CollectionIndexOptions" />.
	/// </summary>
	public string GetDescription()
		=> _description + (FromEnd ? " from end" : "");
}
