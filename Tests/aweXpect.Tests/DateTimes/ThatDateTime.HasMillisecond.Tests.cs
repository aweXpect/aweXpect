﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTime
{
	public sealed class HasMillisecond
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasMillisecond(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have millisecond of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMillisecondOfSubjectIsDifferent_ShouldFail()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? expected = 15;

				async Task Act()
					=> await That(subject).HasMillisecond(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have millisecond of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMillisecondOfSubjectIsTheSame_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int expected = 167;

				async Task Act()
					=> await That(subject).HasMillisecond(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
