namespace aweXpect.Tests.Booleans;

public sealed partial class NullableBoolShould
{
	public sealed class NotBe
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(true, false)]
			[InlineData(true, null)]
			[InlineData(false, true)]
			[InlineData(false, null)]
			[InlineData(null, true)]
			[InlineData(null, false)]
			public async Task WhenSubjectIsDifferent_ShouldSucceed(bool? subject, bool? unexpected)
			{
				async Task Act()
					=> await That(subject).Should().NotBe(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			[InlineData(null)]
			public async Task WhenSubjectIsTheSame_ShouldFail(bool? subject)
			{
				bool? unexpected = subject;

				async Task Act()
					=> await That(subject).Should().NotBe(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
