namespace aweXpect.Tests;

public sealed partial class ThatNullableEnum
{
	public sealed class IsNotDefined
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectIsDefined_ShouldFail(MyColors? subject)
			{
				async Task Act()
					=> await That(subject).IsNotDefined();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              not be defined,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNotDefined_ShouldSucceed()
			{
				MyColors? subject = (MyColors)42;

				async Task Act()
					=> await That(subject).IsNotDefined();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				MyColors? subject = null;

				async Task Act()
					=> await That(subject).IsNotDefined();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             not be defined,
					             but it was <null>
					             """);
			}
		}
	}
}
