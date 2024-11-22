namespace Testably.Expectations.Tests.ThatTests.Enums;

public sealed partial class NullableEnumShould
{
	public sealed class HaveValueTests
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

	public sealed class NotHaveValueTests
	{
		[Theory]
		[InlineData(MyNumbers.One, 2L)]
		[InlineData(MyNumbers.Two, -7L)]
		[InlineData(MyNumbers.Three, 0L)]
		public async Task WhenSubjectDoesNotHaveUnexpectedValue_ShouldSucceed(MyNumbers? subject,
			long? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotHaveValue(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(MyNumbers.One, 1L)]
		[InlineData(MyNumbers.Two, 2L)]
		[InlineData(MyNumbers.Three, 3L)]
		public async Task WhenSubjectHasUnexpectedValue_ShouldFail(MyNumbers? subject,
			long? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotHaveValue(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not have value {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			MyColors? subject = MyColors.Yellow;

			async Task Act()
				=> await That(subject).Should().NotHaveValue(null);

			await That(Act).Should().NotThrow();
		}
	}
}
