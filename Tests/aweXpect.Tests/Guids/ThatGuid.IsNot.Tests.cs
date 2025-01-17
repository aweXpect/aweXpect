namespace aweXpect.Tests;

public sealed partial class ThatGuid
{
	public sealed class IsNot
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldSucceed()
			{
				Guid subject = FixedGuid();
				Guid unexpected = OtherGuid();

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsTheSame_ShouldFail()
			{
				Guid subject = FixedGuid();
				Guid unexpected = subject;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				Guid subject = FixedGuid();
				Guid? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
