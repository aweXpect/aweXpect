namespace aweXpect.Tests;

public sealed partial class ThatBool
{
	public sealed class IsNot
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task WhenSubjectIsDifferent_ShouldSucceed(bool subject)
			{
				bool unexpected = !subject;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task WhenSubjectIsTheSame_ShouldFail(bool subject)
			{
				bool unexpected = subject;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineAutoData(true)]
			[InlineAutoData(false)]
			public async Task WhenSubjectIsTheSame_ShouldFailWithDescriptiveMessage(
				bool subject, string reason)
			{
				bool unexpected = subject;

				async Task Act()
					=> await That(subject).IsNot(unexpected).Because(reason);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be {Formatter.Format(unexpected)}, because {reason},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
