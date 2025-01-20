namespace aweXpect.Tests;

public sealed partial class ThatTimeSpan
{
	public sealed class IsNotNegative
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsMaxValue_ShouldSucceed()
			{
				TimeSpan subject = TimeSpan.MaxValue;

				async Task Act()
					=> await That(subject).IsNotNegative();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsMinValue_ShouldFail()
			{
				TimeSpan subject = TimeSpan.MinValue;

				async Task Act()
					=> await That(subject).IsNotNegative();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be negative,
					             but it was the minimum time span
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNegative_ShouldFail()
			{
				TimeSpan subject = -1.Seconds();

				async Task Act()
					=> await That(subject).IsNotNegative();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be negative,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsPositive_ShouldSucceed()
			{
				TimeSpan subject = 1.Seconds();

				async Task Act()
					=> await That(subject).IsNotNegative();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsZero_ShouldSucceed()
			{
				TimeSpan subject = TimeSpan.Zero;

				async Task Act()
					=> await That(subject).IsNotNegative();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
