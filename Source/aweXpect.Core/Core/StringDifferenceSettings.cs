namespace aweXpect.Core;

/// <summary>
///     The comparison settings used to display the <see cref="StringDifference" />.
/// </summary>
public class StringDifferenceSettings(int ignoredTrailingLines, int ignoredTrailingColumns)
{
	/// <summary>
	///     The number of ignored trailing lines.
	/// </summary>
	public int IgnoredTrailingLines
		=> ignoredTrailingLines;

	/// <summary>
	///     The number of ignored trailing columns in the first line.
	/// </summary>
	public int IgnoredTrailingColumns
		=> ignoredTrailingColumns;

	/// <summary>
	///     The match type used to display the <see cref="StringDifference" />.
	/// </summary>
	public StringDifference.MatchType MatchType { get; internal set; } = StringDifference.MatchType.Equality;
}
