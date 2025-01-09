﻿namespace aweXpect.Tests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class NotHaveMillisecond
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMillisecondOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = 15;

				async Task Act()
					=> await That(subject).Should().NotHaveMillisecond(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenMillisecondOfSubjectIsTheSame_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int unexpected = 167;

				async Task Act()
					=> await That(subject).Should().NotHaveMillisecond(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have millisecond of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
			{
				DateTime? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveMillisecond(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				DateTime? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).Should().NotHaveMillisecond(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveMillisecond(unexpected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}