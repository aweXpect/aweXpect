namespace aweXpect.Tests.Enums;

public sealed partial class EnumShould
{
	public sealed class NotBeDefined
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectIsDefined_ShouldFail(MyColors subject)
			{
				async Task Act()
					=> await That(subject).Should().NotBeDefined();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be defined,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNotDefined_ShouldSucceed()
			{
				MyColors subject = (MyColors)42;

				async Task Act()
					=> await That(subject).Should().NotBeDefined();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
