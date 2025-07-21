using System;

namespace aweXpect.Options;

/// <summary>
///     Options for limitations on a collection index.
/// </summary>
public class CollectionIndexOptions
{
	private string _description = "";
	private Func<int, bool?> _isIndexMatch = _ => true;
	private bool _matchesOnlySingleIndex;

	/// <summary>
	///     Checks if the <paramref name="index" /> is in range.
	/// </summary>
	/// <returns>
	///     <see langword="true" />, if the <paramref name="index" /> is in range, <see langword="null" />,
	///     if the <paramref name="index" /> is not in range, but could be in range for a larger index,
	///     otherwise <see langword="false" /> when the <paramref name="index" /> is not in range
	///     and will also not be in range for larger values.
	/// </returns>
	public bool? DoesIndexMatch(int index)
		=> _isIndexMatch.Invoke(index);

	/// <summary>
	///     Flag indicating, if only a single index is considered in range.
	/// </summary>
	public bool MatchesOnlySingleIndex()
		=> _matchesOnlySingleIndex;

	/// <summary>
	///     Set the checked index to be in range between <paramref name="minimum" /> and <paramref name="maximum" />.
	/// </summary>
	/// <remarks>When either parameter is set to <see langword="null" />, the corresponding range direction is unlimited.</remarks>
	public void SetIndexRange(int? minimum, int? maximum)
	{
		_isIndexMatch = index =>
		{
			if (maximum.HasValue && index > maximum)
			{
				return false;
			}

			if ((minimum is null || index >= minimum) &&
			    (maximum is null || index <= maximum))
			{
				return true;
			}

			return null;
		};
		_matchesOnlySingleIndex = maximum == minimum && minimum is not null;
		if (minimum is null && maximum is null)
		{
			_description = "";
		}
		else
		{
			_description = minimum == maximum
				? $" at index {minimum}"
				: $" with index between {minimum} and {maximum}";
		}
	}

	/// <summary>
	///     Set the checked index to be a match depending on the <paramref name="isIndexMatch" /> function.
	///     <para />
	///     The <paramref name="matchesOnlySingleIndex" /> parameter specifies, if only a single index could match the
	///     function, or if it could match multiple indices.
	/// </summary>
	/// <remarks>
	///     The <paramref name="isIndexMatch" /> is expected to return <see langword="true" /> when the index matches,
	///     <see langword="null" /> when the index does not match, but could match for larger values and otherwise
	///     <see langword="false" />, when it does not match and will not match for larger values.
	/// </remarks>
	public void SetIndexMatch(Func<int, bool?> isIndexMatch, bool matchesOnlySingleIndex, string description)
	{
		_isIndexMatch = isIndexMatch;
		_matchesOnlySingleIndex = matchesOnlySingleIndex;
		_description = description;
	}

	/// <summary>
	///     Returns the description of the <see cref="CollectionIndexOptions" />.
	/// </summary>
	public string GetDescription()
		=> _description;
}
