﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTimeOffset
{
	public sealed partial class Nullable
	{
		public sealed class IsEqualTo
		{
			public sealed class Tests
			{
				[Fact]
				public async Task WhenSubjectAndExpectedAreNull_ShouldSucceed()
				{
					DateTimeOffset? subject = null;
					DateTimeOffset? expected = null;

					async Task Act()
						=> await That(subject).IsEqualTo(expected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsDifferent_ShouldFail()
				{
					DateTimeOffset? subject = CurrentTime();
					DateTimeOffset? expected = LaterTime();

					async Task Act()
						=> await That(subject).IsEqualTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is equal to {Formatter.Format(expected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsTheSame_ShouldSucceed()
				{
					DateTimeOffset? subject = CurrentTime();
					DateTimeOffset? expected = subject;

					async Task Act()
						=> await That(subject).IsEqualTo(expected);

					await That(Act).DoesNotThrow();
				}
			}
		}
	}
}
