namespace Testably.Expectations.Tests.ThatTests.Enums;

public sealed partial class EnumShould
{
	public sealed class HaveFlagTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			MyColors subject = MyColors.Yellow;

			async Task Act()
				=> await That(subject).Should().HaveFlag(null);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have flag <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(MyColors.Blue | MyColors.Red, MyColors.Green)]
		[InlineData(MyColors.Green | MyColors.Yellow, MyColors.Blue)]
		public async Task WhenSubjectDoesNotHaveFlag_ShouldFail(MyColors subject, MyColors expected)
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
			MyColors subject = MyColors.Yellow | MyColors.Red | expected;

			async Task Act()
				=> await That(subject).Should().HaveFlag(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(MyColors.Blue)]
		[InlineData(MyColors.Green)]
		public async Task WhenSubjectIsTheSame_ShouldSucceed(MyColors subject)
		{
			MyColors expected = subject;

			async Task Act()
				=> await That(subject).Should().HaveFlag(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotHaveFlagTests
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
