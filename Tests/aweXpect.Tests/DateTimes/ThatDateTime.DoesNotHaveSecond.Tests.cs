namespace aweXpect.Tests;

public sealed partial class ThatDateTime
{
	public sealed class DoesNotHaveSecond
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSecondOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = 14;

				async Task Act()
					=> await That(subject).DoesNotHaveSecond(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSecondOfSubjectIsTheSame_ShouldFail()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int unexpected = 15;

				async Task Act()
					=> await That(subject).DoesNotHaveSecond(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have second of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveSecond(unexpected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
