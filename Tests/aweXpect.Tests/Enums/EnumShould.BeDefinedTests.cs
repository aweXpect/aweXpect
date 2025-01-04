namespace aweXpect.Tests.Enums;

public sealed partial class EnumShould
{
	public sealed class BeDefined
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectIsDefined_ShouldSucceed(MyColors subject)
			{
				async Task Act()
					=> await That(subject).Should().BeDefined();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNotDefined_ShouldFail()
			{
				MyColors subject = (MyColors)42;

				async Task Act()
					=> await That(subject).Should().BeDefined();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be defined,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
