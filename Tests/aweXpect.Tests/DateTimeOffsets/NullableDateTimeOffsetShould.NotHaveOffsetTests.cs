﻿namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class NullableDateTimeOffsetShould
{
	public sealed class NotHaveOffset
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenOffsetOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				TimeSpan unexpected = 1.Hours();

				async Task Act()
					=> await That(subject).Should().NotHaveOffset(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenOffsetOfSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset? subject = 12.November(2010).At(13, 14, 15, 167).WithOffset(2.Hours());
				TimeSpan unexpected = 2.Hours();

				async Task Act()
					=> await That(subject).Should().NotHaveOffset(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have offset of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				DateTimeOffset? subject = null;
				TimeSpan expected = 2.Hours();

				async Task Act()
					=> await That(subject).Should().NotHaveOffset(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
