namespace Testably.Expectations.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class BeNullOrWhiteSpaceTests
	{
		[Fact]
		public async Task WhenActualIsEmpty_ShouldSucceed()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().BeNullOrWhiteSpace();

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task WhenActualIsNotEmpty_ShouldFail(string? subject)
		{
			async Task Act()
				=> await That(subject).Should().BeNullOrWhiteSpace();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be null or white-space,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenActualIsNotEmpty_ShouldLimitDisplayedStringTo100Characters()
		{
			string subject = StringWithMoreThan100Characters;

			async Task Act()
				=> await That(subject).Should().BeNullOrWhiteSpace();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be null or white-space,
				              but it was "{StringWith100Characters}…"
				              """);
		}

		[Fact]
		public async Task WhenActualIsNull_ShouldSucceed()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().BeNullOrWhiteSpace();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsWhitespace_ShouldFail()
		{
			string subject = " \t ";

			async Task Act()
				=> await That(subject).Should().BeNullOrWhiteSpace();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeNullOrWhiteSpaceTests
	{
		[Fact]
		public async Task WhenActualIsEmpty_ShouldFail()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().NotBeNullOrWhiteSpace();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be null or white-space,
				             but it was ""
				             """);
		}

		[Theory]
		[AutoData]
		public async Task WhenActualIsNotEmpty_ShouldSucceed(string? subject)
		{
			async Task Act()
				=> await That(subject).Should().NotBeNullOrWhiteSpace();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsNull_ShouldSucceed()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeNullOrWhiteSpace();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be null or white-space,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenActualIsWhiteSpace_ShouldLimitDisplayedStringTo100Characters()
		{
			string subject = new(' ', 101);

			async Task Act()
				=> await That(subject).Should().NotBeNullOrWhiteSpace();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be null or white-space,
				              but it was "{new string(' ', 100)}…"
				              """);
		}

		[Fact]
		public async Task WhenActualIsWhitespace_ShouldSucceed()
		{
			string subject = " \t ";

			async Task Act()
				=> await That(subject).Should().NotBeNullOrWhiteSpace();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be null or white-space,
				             but it was " \t "
				             """);
		}
	}
}
