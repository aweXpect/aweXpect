#if NET8_0_OR_GREATER
namespace aweXpect.Tests.TimeOnlys;

public sealed partial class TimeOnlyShould
{
	public sealed class HaveMinute
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeOnly subject = new(10, 11, 12);
				int? expected = null;

				async Task Act()
					=> await That(subject).Should().HaveMinute(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsDifferent_ShouldFail()
			{
				TimeOnly subject = new(10, 11, 12);
				int? expected = 12;

				async Task Act()
					=> await That(subject).Should().HaveMinute(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have minute of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSame_ShouldSucceed()
			{
				TimeOnly subject = new(10, 11, 12);
				int expected = 11;

				async Task Act()
					=> await That(subject).Should().HaveMinute(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
#endif
