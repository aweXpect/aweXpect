﻿namespace aweXpect.Tests;

public sealed partial class ThatNullableEnum
{
	public sealed class IsEqualTo
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(EnumLong.Int64Max, EnumLong.Int64LessOne)]
			public async Task ForLong_WhenSubjectIsDifferent_ShouldFail(EnumLong? subject,
				EnumLong? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              be {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(EnumLong.Int64Max)]
			[InlineData(EnumLong.Int64LessOne)]
			public async Task ForLong_WhenSubjectTheSame_ShouldSucceed(EnumLong? subject)
			{
				EnumLong? expected = subject;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(EnumULong.UInt64Max, EnumULong.UInt64LessOne)]
			[InlineData(EnumULong.UInt64Max, EnumULong.Int64Max)]
			public async Task ForULong_WhenSubjectIsDifferent_ShouldFail(EnumULong? subject,
				EnumULong? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              be {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(EnumULong.Int64Max)]
			[InlineData(EnumULong.UInt64LessOne)]
			[InlineData(EnumULong.UInt64Max)]
			public async Task ForULong_WhenSubjectTheSame_ShouldSucceed(EnumULong? subject)
			{
				EnumULong? expected = subject;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				MyColors? subject = MyColors.Yellow;

				async Task Act()
					=> await That(subject).IsEqualTo(null);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              be <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreNull_ShouldSucceed()
			{
				MyColors? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(null);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(MyColors.Blue, MyColors.Green)]
			[InlineData(MyColors.Blue, null)]
			[InlineData(MyColors.Green, MyColors.Blue)]
			[InlineData(MyColors.Green, null)]
			[InlineData(null, MyColors.Blue)]
			[InlineData(null, MyColors.Green)]
			public async Task WhenSubjectIsDifferent_ShouldFail(MyColors? subject, MyColors? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              be {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				MyColors? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(MyColors.Red);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             be Red,
					             but it was <null>
					             """);
			}

			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData(MyColors.Green)]
			public async Task WhenSubjectIsTheSame_ShouldSucceed(MyColors? subject)
			{
				MyColors? expected = subject;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
