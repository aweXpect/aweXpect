﻿namespace aweXpect.Tests;

public sealed partial class ThatEnum
{
	public sealed partial class Nullable
	{
		public sealed class IsNotEqualTo
		{
			public sealed class Tests
			{
				[Fact]
				public async Task WhenSubjectAndUnexpectedAreNull_ShouldFail()
				{
					MyColors? subject = null;
					MyColors? unexpected = null;

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is not <null>,
						             but it was <null>
						             """);
				}

				[Theory]
				[InlineData(MyColors.Blue, MyColors.Green)]
				[InlineData(MyColors.Blue, null)]
				[InlineData(MyColors.Green, MyColors.Blue)]
				[InlineData(MyColors.Green, null)]
				[InlineData(null, MyColors.Blue)]
				[InlineData(null, MyColors.Green)]
				public async Task WhenSubjectIsDifferent_ShouldSucceed(MyColors? subject,
					MyColors? unexpected)
				{
					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Theory]
				[InlineData(MyColors.Blue)]
				[InlineData(MyColors.Green)]
				[InlineData(null)]
				public async Task WhenSubjectIsTheSame_ShouldFail(MyColors? subject)
				{
					MyColors? unexpected = subject;

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not {Formatter.Format(unexpected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenUnexpectedIsNull_ShouldSucceed()
				{
					MyColors? subject = MyColors.Yellow;

					async Task Act()
						=> await That(subject).IsNotEqualTo(null);

					await That(Act).DoesNotThrow();
				}
			}

			public sealed class LongTests
			{
				[Theory]
				[InlineData(EnumLong.Int64Max, EnumLong.Int64LessOne)]
				public async Task WhenSubjectIsDifferent_ShouldSucceed(EnumLong? subject,
					EnumLong? unexpected)
				{
					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Theory]
				[InlineData(EnumLong.Int64Max)]
				[InlineData(EnumLong.Int64LessOne)]
				public async Task WhenSubjectTheSame_ShouldFail(EnumLong? subject)
				{
					EnumLong? unexpected = subject;

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not {Formatter.Format(unexpected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}
			}

			public sealed class UlongTests
			{
				[Theory]
				[InlineData(EnumULong.UInt64Max, EnumULong.UInt64LessOne)]
				[InlineData(EnumULong.UInt64Max, EnumULong.Int64Max)]
				public async Task WhenSubjectIsDifferent_ShouldSucceed(EnumULong? subject,
					EnumULong? unexpected)
				{
					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Theory]
				[InlineData(EnumULong.Int64Max)]
				[InlineData(EnumULong.UInt64LessOne)]
				[InlineData(EnumULong.UInt64Max)]
				public async Task WhenSubjectTheSame_ShouldFail(EnumULong? subject)
				{
					EnumULong? unexpected = subject;

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not {Formatter.Format(unexpected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}
			}
		}
	}
}
