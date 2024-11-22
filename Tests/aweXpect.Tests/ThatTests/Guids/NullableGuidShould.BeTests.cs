namespace Testably.Expectations.Tests.ThatTests.Guids;

public sealed partial class NullableGuidShould
{
	public sealed class BeTests
	{
		[Fact]
		public async Task WhenSubjectAndExpectedAreNull_ShouldSucceed()
		{
			Guid? subject = null;

			async Task Act()
				=> await That(subject).Should().Be(null);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsDifferent_ShouldFail()
		{
			Guid? subject = FixedGuid();
			Guid? expected = OtherGuid();

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Guid? subject = null;

			async Task Act()
				=> await That(subject).Should().Be(FixedGuid());

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {FixedGuid()},
				              but it was <null>
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsTheSame_ShouldSucceed()
		{
			Guid? subject = FixedGuid();
			Guid? expected = subject;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeTests
	{
		[Fact]
		public async Task WhenSubjectAndExpectedAreNull_ShouldFail()
		{
			Guid? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBe(null);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be <null>,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsDifferent_ShouldSucceed()
		{
			Guid? subject = FixedGuid();
			Guid? unexpected = OtherGuid();

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsTheSame_ShouldFail()
		{
			Guid? subject = FixedGuid();
			Guid? unexpected = subject;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}
}
