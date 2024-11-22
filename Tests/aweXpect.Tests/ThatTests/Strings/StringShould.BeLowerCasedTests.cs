namespace Testably.Expectations.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class BeLowerCasedTests
	{
		[Fact]
		public async Task WhenActualIsEmpty_ShouldSucceed()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().BeLowerCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsLowerCased_ShouldSucceed()
		{
			string subject = "abc";

			async Task Act()
				=> await That(subject).Should().BeLowerCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsLowerCasedOrCaseless_ShouldSucceed()
		{
			string subject = "a漢字b";

			async Task Act()
				=> await That(subject).Should().BeLowerCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsLowerCasedOrSpecialCharacters_ShouldSucceed()
		{
			string subject = "a-b-c!";

			async Task Act()
				=> await That(subject).Should().BeLowerCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsMixedCased_ShouldFail()
		{
			string subject = "aBc";

			async Task Act()
				=> await That(subject).Should().BeLowerCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be lower-cased,
				             but it was "aBc"
				             """);
		}

		[Fact]
		public async Task WhenActualIsNotLowerCased_ShouldLimitDisplayedStringTo100Characters()
		{
			string subject = StringWithMoreThan100Characters;

			async Task Act()
				=> await That(subject).Should().BeLowerCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be lower-cased,
				              but it was "{StringWith100Characters}…"
				              """);
		}

		[Fact]
		public async Task WhenActualIsNull_ShouldFail()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().BeLowerCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be lower-cased,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenActualIsUpperCased_ShouldFail()
		{
			string subject = "ABC";

			async Task Act()
				=> await That(subject).Should().BeLowerCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be lower-cased,
				             but it was "ABC"
				             """);
		}

		[Fact]
		public async Task WhenActualIsWhitespace_ShouldSucceed()
		{
			string subject = " \t\r\n";

			async Task Act()
				=> await That(subject).Should().BeLowerCased();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeLowerCasedTests
	{
		[Fact]
		public async Task WhenActualIsEmpty_ShouldFail()
		{
			string subject = "";

			async Task Act()
				=> await That(subject).Should().NotBeLowerCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be lower-cased,
				             but it was ""
				             """);
		}

		[Fact]
		public async Task WhenActualIsLowerCased_ShouldFail()
		{
			string subject = "abc";

			async Task Act()
				=> await That(subject).Should().NotBeLowerCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be lower-cased,
				             but it was "abc"
				             """);
		}

		[Fact]
		public async Task WhenActualIsLowerCasedOrCaseless_ShouldFail()
		{
			string subject = "a漢字b";

			async Task Act()
				=> await That(subject).Should().NotBeLowerCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be lower-cased,
				             but it was "a漢字b"
				             """);
		}

		[Fact]
		public async Task WhenActualIsLowerCasedOrSpecialCharacters_ShouldFail()
		{
			string subject = "a-b-c!";

			async Task Act()
				=> await That(subject).Should().NotBeLowerCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be lower-cased,
				             but it was "a-b-c!"
				             """);
		}

		[Fact]
		public async Task WhenActualIsMixedCased_ShouldSucceed()
		{
			string subject = "aBc";

			async Task Act()
				=> await That(subject).Should().NotBeLowerCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsNotLowerCased_ShouldLimitDisplayedStringTo100Characters()
		{
			string subject = StringWithMoreThan100Characters.ToLowerInvariant();

			async Task Act()
				=> await That(subject).Should().NotBeLowerCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be lower-cased,
				              but it was "{StringWith100Characters.ToLowerInvariant()}…"
				              """);
		}

		[Fact]
		public async Task WhenActualIsNull_ShouldSucceed()
		{
			string? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeLowerCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsUpperCased_ShouldSucceed()
		{
			string subject = "ABC";

			async Task Act()
				=> await That(subject).Should().NotBeLowerCased();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenActualIsWhitespace_ShouldSucceed()
		{
			string subject = " \t ";

			async Task Act()
				=> await That(subject).Should().NotBeLowerCased();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be lower-cased,
				             but it was " \t "
				             """);
		}
	}
}
