namespace aweXpect.Tests;

public sealed partial class ThatDateTime
{
	public sealed class DoesNotHaveYear
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotHaveYear(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenYearOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = 2011;

				async Task Act()
					=> await That(subject).DoesNotHaveYear(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenYearOfSubjectIsTheSame_ShouldFail()
			{
				DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
				int unexpected = 2010;

				async Task Act()
					=> await That(subject).DoesNotHaveYear(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have year of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
