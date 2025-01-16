namespace aweXpect.Tests;

public sealed partial class NullableBoolShould
{
	public sealed class BeFalse
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenFalse_ShouldSucceed()
			{
				bool? subject = false;

				async Task Act()
					=> await That(subject).Should().BeFalse();

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(true)]
			[InlineData(null)]
			public async Task WhenTrueOrNull_ShouldFail(bool? subject)
			{
				async Task Act()
					=> await That(subject).Should().BeFalse().Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be False, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
