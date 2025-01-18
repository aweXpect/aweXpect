#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatDateOnly
{
	public sealed class HasDay
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenDayOfSubjectIsDifferent_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 11;

				async Task Act()
					=> await That(subject).HasDay(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have day of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenDayOfSubjectIsTheSame_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 12;

				async Task Act()
					=> await That(subject).HasDay(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).HasDay(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have day of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
#endif
