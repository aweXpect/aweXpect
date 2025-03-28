﻿namespace aweXpect.Core.Tests.Core.Exceptions;

public sealed class FailExceptionTests
{
	[Theory]
	[AutoData]
	public async Task Message_ShouldBeSet(string message)
	{
		FailException subject = new(message);

		await That(subject.Message).IsEqualTo(message);
	}
}
