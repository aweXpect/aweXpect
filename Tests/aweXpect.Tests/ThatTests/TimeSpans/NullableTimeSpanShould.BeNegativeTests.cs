namespace Testably.Expectations.Tests.ThatTests.TimeSpans;

public sealed partial class NullableTimeSpanShould
{
	public sealed class BeNegativeTests
	{
		[Fact]
		public async Task WhenSubjectIsMaxValue_ShouldFail()
		{
			TimeSpan? subject = TimeSpan.MaxValue;

			async Task Act()
				=> await That(subject).Should().BeNegative();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be negative,
				             but it was the maximum time span
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsMinValue_ShouldSucceed()
		{
			TimeSpan? subject = TimeSpan.MinValue;

			async Task Act()
				=> await That(subject).Should().BeNegative();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNegative_ShouldSucceed()
		{
			TimeSpan? subject = TimeSpan.FromSeconds(-1);

			async Task Act()
				=> await That(subject).Should().BeNegative();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsPositive_ShouldFail()
		{
			TimeSpan? subject = TimeSpan.FromSeconds(1);

			async Task Act()
				=> await That(subject).Should().BeNegative();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be negative,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsZero_ShouldFail()
		{
			TimeSpan? subject = TimeSpan.Zero;

			async Task Act()
				=> await That(subject).Should().BeNegative();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be negative,
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}

	public sealed class NotBeNegativeTests
	{
		[Fact]
		public async Task WhenSubjectIsMaxValue_ShouldSucceed()
		{
			TimeSpan? subject = TimeSpan.MaxValue;

			async Task Act()
				=> await That(subject).Should().NotBeNegative();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsMinValue_ShouldFail()
		{
			TimeSpan? subject = TimeSpan.MinValue;

			async Task Act()
				=> await That(subject).Should().NotBeNegative();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be negative,
				             but it was the minimum time span
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNegative_ShouldFail()
		{
			TimeSpan? subject = TimeSpan.FromSeconds(-1);

			async Task Act()
				=> await That(subject).Should().NotBeNegative();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be negative,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsPositive_ShouldSucceed()
		{
			TimeSpan? subject = TimeSpan.FromSeconds(1);

			async Task Act()
				=> await That(subject).Should().NotBeNegative();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsZero_ShouldSucceed()
		{
			TimeSpan? subject = TimeSpan.Zero;

			async Task Act()
				=> await That(subject).Should().NotBeNegative();

			await That(Act).Should().NotThrow();
		}
	}
}
