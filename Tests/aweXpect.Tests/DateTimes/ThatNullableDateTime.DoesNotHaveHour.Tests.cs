namespace aweXpect.Tests;

public sealed partial class ThatNullableDateTime
{
	public sealed class DoesNotHaveHour
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenHourOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = 12;

				async Task Act()
					=> await That(subject).DoesNotHaveHour(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenHourOfSubjectIsTheSame_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int unexpected = 13;

				async Task Act()
					=> await That(subject).DoesNotHaveHour(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have hour of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
			{
				DateTime? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveHour(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				DateTime? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).DoesNotHaveHour(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveHour(unexpected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
