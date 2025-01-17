namespace aweXpect.Tests;

public sealed partial class ThatEnum
{
	public sealed class HasValue
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				MyColors subject = MyColors.Yellow;

				async Task Act()
					=> await That(subject).HasValue(null);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have value <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(MyNumbers.One, 2L)]
			[InlineData(MyNumbers.Two, -7)]
			[InlineData(MyNumbers.Three, 0)]
			public async Task WhenSubjectDoesNotHaveExpectedValue_ShouldFail(MyNumbers subject,
				long expected)
			{
				async Task Act()
					=> await That(subject).HasValue(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have value {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(MyNumbers.One, 1)]
			[InlineData(MyNumbers.Two, 2)]
			[InlineData(MyNumbers.Three, 3)]
			public async Task WhenSubjectHasExpectedValue_ShouldSucceed(MyNumbers subject,
				long expected)
			{
				async Task Act()
					=> await That(subject).HasValue(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
