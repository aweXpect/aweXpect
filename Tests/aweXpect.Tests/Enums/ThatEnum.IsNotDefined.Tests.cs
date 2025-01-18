namespace aweXpect.Tests;

public sealed partial class ThatEnum
{
	public sealed class IsNotDefined
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectIsDefined_ShouldFail(MyColors subject)
			{
				async Task Act()
					=> await That(subject).IsNotDefined();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsNotDefined();

				await That(Act).Does().NotThrow();
			}
		}
	}
}
