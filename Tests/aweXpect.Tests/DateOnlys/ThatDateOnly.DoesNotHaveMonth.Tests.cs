﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatDateOnly
{
	public sealed class DoesNotHaveMonth
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMonthOfSubjectIsDifferent_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? unexpected = 12;

				async Task Act()
					=> await That(subject).DoesNotHaveMonth(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsTheSame_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int unexpected = 11;

				async Task Act()
					=> await That(subject).DoesNotHaveMonth(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have month of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveMonth(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
