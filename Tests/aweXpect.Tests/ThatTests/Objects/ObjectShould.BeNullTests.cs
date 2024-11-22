namespace Testably.Expectations.Tests.ThatTests.Objects;

public sealed partial class ObjectShould
{
	public sealed class BeNullTests
	{
		[Fact]
		public async Task WhenSubjectIsNull_ShouldSucceed()
		{
			object? subject = null;

			async Task Act()
				=> await That(subject).Should().BeNull();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsObject_ShouldFail()
		{
			object subject = new MyClass();

			async Task Act()
				=> await That(subject).Should().BeNull()
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be null, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}

	public sealed class NotBeNullTests
	{
		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			object? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeNull()
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be null, because we want to test the failure,
				             but it was
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsObject_ShouldSucceed()
		{
			object subject = new MyClass();

			async Task Act()
				=> await That(subject).Should().NotBeNull();

			await That(Act).Should().NotThrow();
		}
	}
}
