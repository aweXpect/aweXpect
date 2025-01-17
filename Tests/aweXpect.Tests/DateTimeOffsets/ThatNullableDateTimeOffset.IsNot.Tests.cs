﻿namespace aweXpect.Tests;

public sealed partial class ThatNullableDateTimeOffset
{
	public sealed class IsNot
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset? subject = CurrentTime();
				DateTimeOffset? unexpected = LaterTime();

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset? subject = CurrentTime();
				DateTimeOffset? unexpected = subject;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
