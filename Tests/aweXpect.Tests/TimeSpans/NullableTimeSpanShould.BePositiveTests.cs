﻿namespace aweXpect.Tests.TimeSpans;

public sealed partial class NullableTimeSpanShould
{
	public sealed class BePositive
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsMaxValue_ShouldSucceed()
			{
				TimeSpan? subject = TimeSpan.MaxValue;

				async Task Act()
					=> await That(subject).Should().BePositive();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsMinValue_ShouldFail()
			{
				TimeSpan? subject = TimeSpan.MinValue;

				async Task Act()
					=> await That(subject).Should().BePositive();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be positive,
					             but it was the minimum time span
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNegative_ShouldFail()
			{
				TimeSpan? subject = -1.Seconds();

				async Task Act()
					=> await That(subject).Should().BePositive();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be positive,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsPositive_ShouldSucceed()
			{
				TimeSpan? subject = 1.Seconds();

				async Task Act()
					=> await That(subject).Should().BePositive();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsZero_ShouldFail()
			{
				TimeSpan? subject = TimeSpan.Zero;

				async Task Act()
					=> await That(subject).Should().BePositive();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be positive,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
