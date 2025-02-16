﻿namespace aweXpect.Core.Tests.Core;

public class StringDifferenceSettingsExtensionTests
{
	[Fact]
	public async Task ShouldInitializeToEqualityMatchType()
	{
		StringDifferenceSettings settings = new(2, 3);

		await That(settings.MatchType).IsEqualTo(StringDifference.MatchType.Equality);
		await That(settings.IgnoredTrailingLines).IsEqualTo(2);
		await That(settings.IgnoredTrailingColumns).IsEqualTo(3);
	}

	[Theory]
	[InlineData(StringDifference.MatchType.Wildcard)]
	[InlineData(StringDifference.MatchType.Regex)]
	public async Task WithMatchType_ShouldSetMatchTypeAndKeepOtherValues(StringDifference.MatchType matchType)
	{
		StringDifferenceSettings settings = new(2, 3);

		settings.WithMatchType(matchType);

		await That(settings.MatchType).IsEqualTo(matchType);
		await That(settings.IgnoredTrailingLines).IsEqualTo(2);
		await That(settings.IgnoredTrailingColumns).IsEqualTo(3);
	}
}
