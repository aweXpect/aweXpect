namespace aweXpect.Tests.Enums;

public sealed partial class NullableEnumShould
{
	public sealed class HaveFlag
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				MyColors? subject = MyColors.Yellow;

				async Task Act()
					=> await That(subject).Should().HaveFlag(null);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have flag <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldSucceed()
			{
				MyColors? subject = null;

				async Task Act()
					=> await That(subject).Should().HaveFlag(null);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineData(MyColors.Blue | MyColors.Red, MyColors.Green)]
			[InlineData(MyColors.Green | MyColors.Yellow, MyColors.Blue)]
			public async Task WhenSubjectDoesNotHaveFlag_ShouldFail(MyColors? subject,
				MyColors expected)
			{
				async Task Act()
					=> await That(subject).Should().HaveFlag(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have flag {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectHasFlag_ShouldSucceed(MyColors expected)
			{
				MyColors? subject = MyColors.Yellow | MyColors.Red | expected;

				async Task Act()
					=> await That(subject).Should().HaveFlag(expected);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectIsTheSame_ShouldSucceed(MyColors expected)
			{
				MyColors? subject = expected;

				async Task Act()
					=> await That(subject).Should().HaveFlag(expected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
