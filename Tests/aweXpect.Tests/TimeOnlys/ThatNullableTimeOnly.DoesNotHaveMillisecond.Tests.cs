﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatNullableTimeOnly
{
	public sealed class DoesNotHaveMillisecond
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMillisecondOfSubjectIsDifferent_ShouldSucceed()
			{
				TimeOnly? subject = new(10, 11, 12, 345);
				int? unexpected = 12;

				async Task Act()
					=> await That(subject).DoesNotHaveMillisecond(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMillisecondOfSubjectIsTheSame_ShouldFail()
			{
				TimeOnly? subject = new(10, 11, 12, 345);
				int unexpected = 345;

				async Task Act()
					=> await That(subject).DoesNotHaveMillisecond(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have millisecond of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
			{
				TimeOnly? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveMillisecond(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				TimeOnly? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).DoesNotHaveMillisecond(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				TimeOnly? subject = new(10, 11, 12);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveMillisecond(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
