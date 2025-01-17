namespace aweXpect.Tests;

public sealed partial class ThatEnum
{
	public sealed class HasFlag
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				MyColors subject = MyColors.Yellow;

				async Task Act()
					=> await That(subject).HasFlag(null);

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).HasFlag(expected);

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).HasFlag(expected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectIsTheSame_ShouldSucceed(MyColors subject)
			{
				MyColors expected = subject;

				async Task Act()
					=> await That(subject).HasFlag(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
