namespace aweXpect.Tests;

public sealed partial class ThatDateTime
{
	public sealed class DoesNotHaveMinute
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMinuteOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = 13;

				async Task Act()
					=> await That(subject).DoesNotHaveMinute(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSame_ShouldFail()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int unexpected = 14;

				async Task Act()
					=> await That(subject).DoesNotHaveMinute(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have minute of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveMinute(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
