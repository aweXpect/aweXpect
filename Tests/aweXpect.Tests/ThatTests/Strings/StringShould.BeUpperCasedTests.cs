namespace Testably.Expectations.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class BeUpperCasedTests
	{
		[Fact]
		public async Task WhenActualIsEmpty_ShouldSucceed()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().BeUpperCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsLowerCased_ShouldFail()
		{
			string subject = "abc";

			async Task Act()
				=> await That(subject).Should().BeUpperCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be upper-cased,
				             but it was "abc"
				             """);
		}

		[Fact]
		public async Task WhenActualIsMixedCased_ShouldFail()
		{
			string subject = "AbC";

			async Task Act()
				=> await That(subject).Should().BeUpperCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be upper-cased,
				             but it was "AbC"
				             """);
		}

		[Fact]
		public async Task WhenActualIsNotUpperCased_ShouldLimitDisplayedStringTo100Characters()
		{
			string subject = StringWithMoreThan100Characters;

			async Task Act()
				=> await That(subject).Should().BeUpperCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be upper-cased,
				              but it was "{StringWith100Characters}…"
				              """);
		}

		[Fact]
		public async Task WhenActualIsNull_ShouldFail()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().BeUpperCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be upper-cased,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenActualIsUpperCased_ShouldSucceed()
		{
			string subject = "ABC";

			async Task Act()
				=> await That(subject).Should().BeUpperCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsUpperCasedOrCaseless_ShouldSucceed()
		{
			string subject = "A漢字B";

			async Task Act()
				=> await That(subject).Should().BeUpperCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsUpperCasedOrSpecialCharacters_ShouldSucceed()
		{
			string subject = "A-B-C!";

			async Task Act()
				=> await That(subject).Should().BeUpperCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsWhitespace_ShouldSucceed()
		{
			string subject = " \t\r\n";

			async Task Act()
				=> await That(subject).Should().BeUpperCased();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeUpperCasedTests
	{
		[Fact]
		public async Task WhenActualIsEmpty_ShouldFail()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().NotBeUpperCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be upper-cased,
				             but it was ""
				             """);
		}

		[Fact]
		public async Task WhenActualIsLowerCased_ShouldSucceed()
		{
			string subject = "abc";

			async Task Act()
				=> await That(subject).Should().NotBeUpperCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsMixedCased_ShouldSucceed()
		{
			string subject = "AbC";

			async Task Act()
				=> await That(subject).Should().NotBeUpperCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsNotUpperCased_ShouldLimitDisplayedStringTo100Characters()
		{
			string subject = StringWithMoreThan100Characters.ToUpperInvariant();

			async Task Act()
				=> await That(subject).Should().NotBeUpperCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be upper-cased,
				              but it was "{StringWith100Characters.ToUpperInvariant()}…"
				              """);
		}

		[Fact]
		public async Task WhenActualIsNull_ShouldSucceed()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeUpperCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsUpperCased_ShouldFail()
		{
			string subject = "ABC";

			async Task Act()
				=> await That(subject).Should().NotBeUpperCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be upper-cased,
				             but it was "ABC"
				             """);
		}

		[Fact]
		public async Task WhenActualIsUpperCasedOrCaseless_ShouldFail()
		{
			string subject = "A漢字B";

			async Task Act()
				=> await That(subject).Should().NotBeUpperCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be upper-cased,
				             but it was "A漢字B"
				             """);
		}

		[Fact]
		public async Task WhenActualIsUpperCasedOrSpecialCharacters_ShouldFail()
		{
			string subject = "A-B-C!";

			async Task Act()
				=> await That(subject).Should().NotBeUpperCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be upper-cased,
				             but it was "A-B-C!"
				             """);
		}

		[Fact]
		public async Task WhenActualIsWhitespace_ShouldSucceed()
		{
			string subject = " \t ";

			async Task Act()
				=> await That(subject).Should().NotBeUpperCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be upper-cased,
				             but it was " \t "
				             """);
		}
	}
}
