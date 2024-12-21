namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class NullableDateTimeOffsetShould
{
	public sealed class NotHaveMillisecond
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMillisecondOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? unexpected = 15;

				async Task Act()
					=> await That(subject).Should().NotHaveMillisecond(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenMillisecondOfSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
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
				DateTimeOffset? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveMillisecond(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				DateTimeOffset? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).Should().NotHaveMillisecond(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveMillisecond(unexpected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
