﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatTimeOnly
{
	public sealed class HasMinute
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeOnly subject = new(10, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMinute(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsDifferent_ShouldFail()
			{
				TimeOnly subject = new(10, 11, 12);
				int? expected = 12;

				async Task Act()
					=> await That(subject).HasMinute(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSame_ShouldSucceed()
			{
				TimeOnly subject = new(10, 11, 12);
				int expected = 11;

				async Task Act()
					=> await That(subject).HasMinute(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
