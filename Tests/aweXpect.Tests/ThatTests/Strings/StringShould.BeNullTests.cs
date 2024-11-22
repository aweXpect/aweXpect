namespace Testably.Expectations.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class BeNullTests
	{
		[Fact]
		public async Task WhenActualIsEmpty_ShouldFail()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().BeNull();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be null,
				             but it was ""
				             """);
		}

		[Theory]
		[AutoData]
		public async Task WhenActualIsNotNull_ShouldFail(string? subject)
		{
			async Task Act()
				=> await That(subject).Should().BeNull();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be null,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenActualIsNull_ShouldSucceed()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().BeNull();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeNullTests
	{
		[Fact]
		public async Task WhenActualIsEmpty_ShouldSucceed()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().NotBeNull();

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task WhenActualIsNotNull_ShouldSucceed(string? subject)
		{
			async Task Act()
				=> await That(subject).Should().NotBeNull();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsNull_ShouldFail()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeNull();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be null,
				             but it was
				             """);
		}
	}
}
