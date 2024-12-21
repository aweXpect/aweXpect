namespace aweXpect.Tests.Enums;

public sealed partial class NullableEnumShould
{
	public sealed class HaveValue
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				MyColors? subject = MyColors.Yellow;

				async Task Act()
					=> await That(subject).Should().HaveValue(null);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have value <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(MyNumbers.One, 2L)]
			[InlineData(MyNumbers.Two, -7L)]
			[InlineData(MyNumbers.Three, 0L)]
			public async Task WhenSubjectDoesNotHaveExpectedValue_ShouldFail(MyNumbers? subject,
				long? expected)
			{
				async Task Act()
					=> await That(subject).Should().HaveValue(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have value {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(MyNumbers.One, 1L)]
			[InlineData(MyNumbers.Two, 2L)]
			[InlineData(MyNumbers.Three, 3L)]
			public async Task WhenSubjectHasExpectedValue_ShouldSucceed(MyNumbers? subject,
				long? expected)
			{
				async Task Act()
					=> await That(subject).Should().HaveValue(expected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
