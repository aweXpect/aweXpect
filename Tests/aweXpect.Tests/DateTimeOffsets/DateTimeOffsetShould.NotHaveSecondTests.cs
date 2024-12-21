﻿namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class NotHaveSecond
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSecondOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? unexpected = 14;

				async Task Act()
					=> await That(subject).Should().NotHaveSecond(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSecondOfSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int unexpected = 15;

				async Task Act()
					=> await That(subject).Should().NotHaveSecond(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have second of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveSecond(unexpected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
