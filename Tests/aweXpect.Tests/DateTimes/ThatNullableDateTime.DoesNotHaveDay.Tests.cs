﻿namespace aweXpect.Tests;

public sealed partial class ThatNullableDateTime
{
	public sealed class DoesNotHaveDay
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = 11;

				async Task Act()
					=> await That(subject).DoesNotHaveDay(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSame_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int unexpected = 12;

				async Task Act()
					=> await That(subject).DoesNotHaveDay(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have day of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
			{
				DateTime? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveDay(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				DateTime? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).DoesNotHaveDay(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveDay(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
