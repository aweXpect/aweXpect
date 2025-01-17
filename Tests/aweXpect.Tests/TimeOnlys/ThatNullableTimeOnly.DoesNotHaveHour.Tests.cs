﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatNullableTimeOnly
{
	public sealed class DoesNotHaveHour
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenHourOfSubjectIsDifferent_ShouldSucceed()
			{
				TimeOnly? subject = new(10, 11, 12);
				int? unexpected = 11;

				async Task Act()
					=> await That(subject).DoesNotHaveHour(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenHourOfSubjectIsTheSame_ShouldFail()
			{
				TimeOnly? subject = new(10, 11, 12);
				int unexpected = 10;

				async Task Act()
					=> await That(subject).DoesNotHaveHour(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have hour of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
			{
				TimeOnly? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveHour(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				TimeOnly? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).DoesNotHaveHour(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				TimeOnly? subject = new(10, 11, 12);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveHour(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
