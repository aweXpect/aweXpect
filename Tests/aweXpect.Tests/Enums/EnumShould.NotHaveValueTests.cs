namespace aweXpect.Tests.Enums;

public sealed partial class EnumShould
{
	public sealed class NotHaveValue
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyNumbers.One, 2L)]
			[InlineData(MyNumbers.Two, -7)]
			[InlineData(MyNumbers.Three, 0)]
			public async Task WhenSubjectDoesNotHaveUnexpectedValue_ShouldSucceed(MyNumbers subject,
				long unexpected)
			{
				async Task Act()
					=> await That(subject).Should().NotHaveValue(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(MyNumbers.One, 1)]
			[InlineData(MyNumbers.Two, 2)]
			[InlineData(MyNumbers.Three, 3)]
			public async Task WhenSubjectHasUnexpectedValue_ShouldFail(MyNumbers subject,
				long unexpected)
			{
				async Task Act()
					=> await That(subject).Should().NotHaveValue(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have value {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				MyColors subject = MyColors.Yellow;

				async Task Act()
					=> await That(subject).Should().NotHaveValue(null);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
