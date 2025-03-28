﻿namespace aweXpect.Tests;

public class SkipTests
{
	[Theory]
	[AutoData]
	public async Task ThrownException_ShouldContainReason(string message)
	{
		void Act()
			=> Skip.Test(message);

		await That(Act).ThrowsException()
			.WithMessage($"*{message}*").AsWildcard();
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public async Task Unless_ShouldThrowExceptionWhenTrue(bool condition, string message)
	{
		void Act()
			=> Skip.Unless(condition, message);

		await That(Act).ThrowsException().OnlyIf(!condition)
			.WithMessage($"*{message}*").AsWildcard();
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public async Task When_ShouldThrowExceptionWhenTrue(bool condition, string message)
	{
		void Act()
			=> Skip.When(condition, message);

		await That(Act).ThrowsException().OnlyIf(condition)
			.WithMessage($"*{message}*").AsWildcard();
	}
}
