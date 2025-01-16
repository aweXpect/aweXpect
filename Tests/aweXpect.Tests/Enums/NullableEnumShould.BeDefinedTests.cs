namespace aweXpect.Tests.Enums;

public sealed partial class NullableEnumShould
{
	public sealed class BeDefined
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectIsDefined_ShouldSucceed(MyColors? subject)
			{
				async Task Act()
					=> await That(subject).Should().BeDefined();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNotDefined_ShouldFail()
			{
				MyColors? subject = (MyColors)42;

				async Task Act()
					=> await That(subject).Should().BeDefined();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be defined,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				MyColors? subject = null;

				async Task Act()
					=> await That(subject).Should().BeDefined();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be defined,
					             but it was <null>
					             """);
			}
		}
	}
}
