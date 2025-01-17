#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class TimeOnlyShould
{
	public sealed class DoesNotHaveMinute
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMinuteOfSubjectIsDifferent_ShouldSucceed()
			{
				TimeOnly subject = new(10, 11, 12);
				int? unexpected = 12;

				async Task Act()
					=> await That(subject).DoesNotHaveMinute(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSame_ShouldFail()
			{
				TimeOnly subject = new(10, 11, 12);
				int unexpected = 11;

				async Task Act()
					=> await That(subject).DoesNotHaveMinute(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have minute of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				TimeOnly subject = new(10, 11, 12);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveMinute(unexpected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
#endif
