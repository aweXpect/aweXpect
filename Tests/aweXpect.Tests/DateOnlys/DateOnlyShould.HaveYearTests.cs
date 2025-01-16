#if NET8_0_OR_GREATER
namespace aweXpect.Tests.DateOnlys;

public sealed partial class DateOnlyShould
{
	public sealed class HaveYear
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).Should().HaveYear(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have year of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenYearOfSubjectIsDifferent_ShouldFail()
			{
				DateOnly subject = new(2010, 11, 12);
				int? expected = 2011;

				async Task Act()
					=> await That(subject).Should().HaveYear(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have year of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenYearOfSubjectIsTheSame_ShouldSucceed()
			{
				DateOnly subject = new(2010, 11, 12);
				int expected = 2010;

				async Task Act()
					=> await That(subject).Should().HaveYear(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
#endif
