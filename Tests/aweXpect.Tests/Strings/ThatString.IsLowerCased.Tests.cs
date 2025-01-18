namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class IsLowerCased
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsEmpty_ShouldSucceed()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).IsLowerCased();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsLowerCased_ShouldSucceed()
			{
				string subject = "abc";

				async Task Act()
					=> await That(subject).IsLowerCased();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsLowerCasedOrCaseless_ShouldSucceed()
			{
				string subject = "a漢字b";

				async Task Act()
					=> await That(subject).IsLowerCased();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsLowerCasedOrSpecialCharacters_ShouldSucceed()
			{
				string subject = "a-b-c!";

				async Task Act()
					=> await That(subject).IsLowerCased();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsMixedCased_ShouldFail()
			{
				string subject = "aBc";

				async Task Act()
					=> await That(subject).IsLowerCased();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsLowerCased();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsLowerCased();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsLowerCased();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsLowerCased();

				await That(Act).Does().NotThrow();
			}
		}
	}
}
