﻿namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class HaveYear
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? expected = null;

				async Task Act()
					=> await That(subject).Should().HaveYear(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have year of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenYearOfSubjectIsDifferent_ShouldFail()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? expected = 2011;

				async Task Act()
					=> await That(subject).Should().HaveYear(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have year of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenYearOfSubjectIsTheSame_ShouldSucceed()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int expected = 2010;

				async Task Act()
					=> await That(subject).Should().HaveYear(expected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}