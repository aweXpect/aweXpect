namespace aweXpect.Core;

/// <summary>
///     Extension methods on <see cref="StringDifferenceSettings" />.
/// </summary>
public static class StringDifferenceSettingsExtensions
{
	/// <summary>
	///     Sets the <see cref="StringDifferenceSettings.MatchType" /> used to display the <see cref="StringDifference" />.
	/// </summary>
	public static StringDifferenceSettings WithMatchType(this StringDifferenceSettings? settings,
		StringDifference.MatchType matchType)
	{
		if (settings == null)
		{
			return new StringDifferenceSettings(0, 0)
			{
				MatchType = matchType
			};
		}

		settings.MatchType = matchType;
		return settings;
	}
}
