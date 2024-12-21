#if NET8_0_OR_GREATER
namespace aweXpect.Tests.DateOnlys;

public sealed partial class NullableDateOnlyShould
{
	public sealed class NotHaveDay
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsDifferent_ShouldSucceed()
			{
				DateOnly? subject = new(2010, 11, 12);
				int? unexpected = 11;

				async Task Act()
					=> await That(subject).Should().NotHaveDay(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSame_ShouldFail()
			{
				DateOnly? subject = new(2010, 11, 12);
				int unexpected = 12;

				async Task Act()
					=> await That(subject).Should().NotHaveDay(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have day of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
			{
				DateOnly? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveDay(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				DateOnly? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).Should().NotHaveDay(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateOnly? subject = new(2010, 11, 12);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveDay(unexpected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
#endif
