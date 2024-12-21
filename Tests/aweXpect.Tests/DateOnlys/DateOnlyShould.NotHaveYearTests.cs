#if NET8_0_OR_GREATER
namespace aweXpect.Tests.DateOnlys;

public sealed partial class DateOnlyShould
{
	public sealed class NotHaveYear
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveYear(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenYearOfSubjectIsDifferent_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int? unexpected = 2011;

				async Task Act()
					=> await That(subject).Should().NotHaveYear(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenYearOfSubjectIsTheSame_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int unexpected = 2010;

				async Task Act()
					=> await That(subject).Should().NotHaveYear(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have year of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
#endif
