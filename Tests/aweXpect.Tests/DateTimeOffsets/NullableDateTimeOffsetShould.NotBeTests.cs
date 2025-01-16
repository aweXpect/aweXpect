﻿namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class NullableDateTimeOffsetShould
{
	public sealed class NotBe
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset? subject = CurrentTime();
				DateTimeOffset? unexpected = LaterTime();

				async Task Act()
					=> await That(subject).Should().NotBe(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset? subject = CurrentTime();
				DateTimeOffset? unexpected = subject;

				async Task Act()
					=> await That(subject).Should().NotBe(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
