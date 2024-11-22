namespace Testably.Expectations.Tests.ThatTests.DateTimeOffsets;

public sealed partial class NullableDateTimeOffsetShould
{
	public sealed class HaveOffsetTests
	{
		[Fact]
		public async Task WhenOffsetOfSubjectIsDifferent_ShouldFail()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
			TimeSpan expected = TimeSpan.FromHours(1);

			async Task Act()
				=> await That(subject).Should().HaveOffset(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have offset of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenOffsetOfSubjectIsTheSame_ShouldSucceed()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
			TimeSpan expected = TimeSpan.FromHours(2);

			async Task Act()
				=> await That(subject).Should().HaveOffset(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			DateTimeOffset? subject = null;
			TimeSpan expected = TimeSpan.FromHours(2);

			async Task Act()
				=> await That(subject).Should().HaveOffset(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have offset of 2:00:00,
				             but it was <null>
				             """);
		}
	}

	public sealed class NotHaveOffsetTests
	{
		[Fact]
		public async Task WhenOffsetOfSubjectIsDifferent_ShouldSucceed()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
			TimeSpan unexpected = TimeSpan.FromHours(1);

			async Task Act()
				=> await That(subject).Should().NotHaveOffset(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenOffsetOfSubjectIsTheSame_ShouldFail()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
			TimeSpan unexpected = TimeSpan.FromHours(2);

			async Task Act()
				=> await That(subject).Should().NotHaveOffset(unexpected);

			await That(Act).Should().Throw<XunitException>()
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
			TimeSpan expected = TimeSpan.FromHours(2);

			async Task Act()
				=> await That(subject).Should().NotHaveOffset(expected);

			await That(Act).Should().NotThrow();
		}
	}
}
