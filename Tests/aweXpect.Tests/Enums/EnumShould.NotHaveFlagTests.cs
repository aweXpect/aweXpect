namespace aweXpect.Tests.Enums;

public sealed partial class EnumShould
{
	public sealed class NotHaveFlag
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectDoesNotHaveFlag_ShouldSucceed(MyColors unexpected)
			{
				MyColors subject = MyColors.Yellow | (MyColors.Red & ~unexpected);

				async Task Act()
					=> await That(subject).Should().NotHaveFlag(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineData(MyColors.Blue | MyColors.Green, MyColors.Green)]
			[InlineData(MyColors.Blue | MyColors.Yellow, MyColors.Blue)]
			public async Task WhenSubjectHasFlag_ShouldFail(MyColors subject, MyColors unexpected)
			{
				async Task Act()
					=> await That(subject).Should().NotHaveFlag(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have flag {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectIsTheSame_ShouldFail(MyColors subject)
			{
				MyColors unexpected = subject;

				async Task Act()
					=> await That(subject).Should().NotHaveFlag(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have flag {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				MyColors subject = MyColors.Yellow;

				async Task Act()
					=> await That(subject).Should().NotHaveFlag(null);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
