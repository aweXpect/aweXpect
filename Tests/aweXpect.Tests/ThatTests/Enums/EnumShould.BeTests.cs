namespace Testably.Expectations.Tests.ThatTests.Enums;

public sealed partial class EnumShould
{
	public sealed class BeTests
	{
		[Theory]
		[InlineData(EnumLong.Int64Max, EnumLong.Int64LessOne)]
		public async Task ForLong_WhenSubjectIsDifferent_ShouldFail(EnumLong subject,
			EnumLong expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(EnumLong.Int64Max)]
		[InlineData(EnumLong.Int64LessOne)]
		public async Task ForLong_WhenSubjectTheSame_ShouldSucceed(EnumLong subject)
		{
			EnumLong expected = subject;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(EnumULong.UInt64Max, EnumULong.UInt64LessOne)]
		[InlineData(EnumULong.UInt64Max, EnumULong.Int64Max)]
		public async Task ForULong_WhenSubjectIsDifferent_ShouldFail(EnumULong subject,
			EnumULong expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(EnumULong.Int64Max)]
		[InlineData(EnumULong.UInt64LessOne)]
		[InlineData(EnumULong.UInt64Max)]
		public async Task ForULong_WhenSubjectTheSame_ShouldSucceed(EnumULong subject)
		{
			EnumULong expected = subject;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			MyColors subject = MyColors.Yellow;

			async Task Act()
				=> await That(subject).Should().Be(null);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(MyColors.Blue, MyColors.Green)]
		[InlineData(MyColors.Green, MyColors.Blue)]
		public async Task WhenSubjectIsDifferent_ShouldFail(MyColors subject, MyColors expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(MyColors.Blue)]
		[InlineData(MyColors.Green)]
		public async Task WhenSubjectIsTheSame_ShouldSucceed(MyColors subject)
		{
			MyColors expected = subject;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeTests
	{
		[Theory]
		[InlineData(EnumLong.Int64Max, EnumLong.Int64LessOne)]
		public async Task ForLong_WhenSubjectIsDifferent_ShouldSucceed(EnumLong subject,
			EnumLong unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(EnumLong.Int64Max)]
		[InlineData(EnumLong.Int64LessOne)]
		public async Task ForLong_WhenSubjectTheSame_ShouldFail(EnumLong subject)
		{
			EnumLong unexpected = subject;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(EnumULong.UInt64Max, EnumULong.UInt64LessOne)]
		[InlineData(EnumULong.UInt64Max, EnumULong.Int64Max)]
		public async Task ForULong_WhenSubjectIsDifferent_ShouldSucceed(EnumULong subject,
			EnumULong unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(EnumULong.Int64Max)]
		[InlineData(EnumULong.UInt64LessOne)]
		[InlineData(EnumULong.UInt64Max)]
		public async Task ForULong_WhenSubjectTheSame_ShouldFail(EnumULong subject)
		{
			EnumULong unexpected = subject;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(MyColors.Blue, MyColors.Green)]
		[InlineData(MyColors.Green, MyColors.Blue)]
		public async Task WhenSubjectIsDifferent_ShouldSucceed(MyColors subject,
			MyColors unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(MyColors.Blue)]
		[InlineData(MyColors.Green)]
		public async Task WhenSubjectIsTheSame_ShouldFail(MyColors subject)
		{
			MyColors unexpected = subject;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			MyColors subject = MyColors.Yellow;

			async Task Act()
				=> await That(subject).Should().NotBe(null);

			await That(Act).Should().NotThrow();
		}
	}
}
