﻿namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class IsNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsEmpty_ShouldFail()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).IsNull();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is null,
					             but it was ""
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenActualIsNotNull_ShouldFail(string? subject)
			{
				async Task Act()
					=> await That(subject).IsNull();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is null,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenActualIsNull_ShouldSucceed()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).IsNull();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
