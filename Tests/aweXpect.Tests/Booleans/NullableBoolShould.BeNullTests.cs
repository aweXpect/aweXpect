namespace aweXpect.Tests.Booleans;

public sealed partial class NullableBoolShould
{
	public sealed class BeNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenNull_ShouldSucceed()
			{
				bool? subject = null;

				async Task Act()
					=> await That(subject).Should().BeNull();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task WhenTrueOrFalse_ShouldFail(bool? subject)
			{
				async Task Act()
					=> await That(subject).Should().BeNull().Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
