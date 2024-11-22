namespace Testably.Expectations.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class BeEmptyTests
	{
		[Fact]
		public async Task WhenActualIsEmpty_ShouldSucceed()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task WhenActualIsNotEmpty_ShouldFail(string? subject)
		{
			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be empty,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenActualIsNotEmpty_ShouldLimitDisplayedStringTo100Characters()
		{
			string subject = StringWithMoreThan100Characters;

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be empty,
				              but it was "{StringWith100Characters}…"
				              """);
		}

		[Fact]
		public async Task WhenActualIsNull_ShouldFail()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be empty,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenActualIsWhitespace_ShouldFail()
		{
			string subject = " \t ";

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be empty,
				             but it was " \t "
				             """);
		}
	}

	public sealed class NotBeEmptyTests
	{
		[Fact]
		public async Task WhenActualIsEmpty_ShouldFail()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().NotBeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be empty,
				             but it was
				             """);
		}

		[Theory]
		[AutoData]
		public async Task WhenActualIsNotEmpty_ShouldSucceed(string? subject)
		{
			async Task Act()
				=> await That(subject).Should().NotBeEmpty();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsNull_ShouldSucceed()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeEmpty();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsWhitespace_ShouldSucceed()
		{
			string subject = " \t ";

			async Task Act()
				=> await That(subject).Should().NotBeEmpty();

			await That(Act).Should().NotThrow();
		}
	}
}
