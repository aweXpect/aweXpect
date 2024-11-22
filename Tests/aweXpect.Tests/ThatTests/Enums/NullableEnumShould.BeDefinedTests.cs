namespace Testably.Expectations.Tests.ThatTests.Enums;

public sealed partial class NullableEnumShould
{
	public sealed class BeDefinedTests
	{
		[Theory]
		[InlineData(MyColors.Blue)]
		[InlineData(MyColors.Green)]
		public async Task WhenSubjectIsDefined_ShouldSucceed(MyColors? subject)
		{
			async Task Act()
				=> await That(subject).Should().BeDefined();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNotDefined_ShouldFail()
		{
			MyColors? subject = (MyColors)42;

			async Task Act()
				=> await That(subject).Should().BeDefined();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be defined,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			MyColors? subject = null;

			async Task Act()
				=> await That(subject).Should().BeDefined();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be defined,
				             but it was <null>
				             """);
		}
	}

	public sealed class NotBeDefinedTests
	{
		[Theory]
		[InlineData(MyColors.Blue)]
		[InlineData(MyColors.Green)]
		public async Task WhenSubjectIsDefined_ShouldFail(MyColors? subject)
		{
			async Task Act()
				=> await That(subject).Should().NotBeDefined();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be defined,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsNotDefined_ShouldSucceed()
		{
			MyColors? subject = (MyColors)42;

			async Task Act()
				=> await That(subject).Should().NotBeDefined();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			MyColors? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeDefined();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be defined,
				             but it was <null>
				             """);
		}
	}
}
