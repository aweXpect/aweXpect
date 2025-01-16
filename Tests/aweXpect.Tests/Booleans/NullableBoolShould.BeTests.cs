namespace aweXpect.Tests;

public sealed partial class NullableBoolShould
{
	public sealed class Be
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
			public async Task WhenSubjectIsDifferent_ShouldFail(bool? subject, bool? expected)
			{
				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			[InlineData(null)]
			public async Task WhenSubjectIsTheSame_ShouldSucceed(bool? subject)
			{
				bool? expected = subject;

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
