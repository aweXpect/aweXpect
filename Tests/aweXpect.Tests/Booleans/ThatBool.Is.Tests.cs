namespace aweXpect.Tests;

public sealed partial class ThatBool
{
	public sealed class Is
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task WhenSubjectIsDifferent_ShouldFail(bool subject)
			{
				bool expected = !subject;

				async Task Act()
					=> await That(subject).Is(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineAutoData(true)]
			[InlineAutoData(false)]
			public async Task WhenSubjectIsDifferent_ShouldFailWithDescriptiveMessage(
				bool subject, string reason)
			{
				bool expected = !subject;

				async Task Act()
					=> await That(subject).Is(expected).Because(reason);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be {Formatter.Format(expected)}, because {reason},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task WhenSubjectIsTheSame_ShouldSucceed(bool subject)
			{
				bool expected = subject;

				async Task Act()
					=> await That(subject).Is(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
