﻿using aweXpect.Core.Helpers;

namespace aweXpect.Core.Tests.Core.Helpers;

public class BecauseReasonTests
{
	[Fact]
	public async Task WhenReasonDoesNotStartWithBecause_ShouldPrefixCommaAndBecause()
	{
		string reason = "something";
		string expected = ", because something";
		BecauseReason sut = new(reason);

		string result = sut.ToString();

		await That(result).IsEqualTo(expected);
	}

	[Fact]
	public async Task WhenReasonStartsWithBecause_ShouldPrefixComma()
	{
		string reason = "because something";
		string expected = ", because something";
		BecauseReason sut = new(reason);

		string result = sut.ToString();

		await That(result).IsEqualTo(expected);
	}
}
