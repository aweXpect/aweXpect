namespace aweXpect.Tests;

public sealed partial class ThatNullableEnum
{
	public sealed class DoesNotHaveValue
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyNumbers.One, 2L)]
			[InlineData(MyNumbers.Two, -7L)]
			[InlineData(MyNumbers.Three, 0L)]
			public async Task WhenSubjectDoesNotHaveUnexpectedValue_ShouldSucceed(MyNumbers? subject,
				long? unexpected)
			{
				async Task Act()
					=> await That(subject).DoesNotHaveValue(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(MyNumbers.One, 1L)]
			[InlineData(MyNumbers.Two, 2L)]
			[InlineData(MyNumbers.Three, 3L)]
			public async Task WhenSubjectHasUnexpectedValue_ShouldFail(MyNumbers? subject,
				long? unexpected)
			{
				async Task Act()
					=> await That(subject).DoesNotHaveValue(unexpected);

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).DoesNotHaveValue(null);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
