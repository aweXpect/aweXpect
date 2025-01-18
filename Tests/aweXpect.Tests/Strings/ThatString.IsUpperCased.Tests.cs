namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class IsUpperCased
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsEmpty_ShouldSucceed()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).IsUpperCased();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenActualIsLowerCased_ShouldFail()
			{
				string subject = "abc";

				async Task Act()
					=> await That(subject).IsUpperCased();

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).IsUpperCased();

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).IsUpperCased();

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).IsUpperCased();

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).IsUpperCased();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenActualIsUpperCasedOrCaseless_ShouldSucceed()
			{
				string subject = "A漢字B";

				async Task Act()
					=> await That(subject).IsUpperCased();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenActualIsUpperCasedOrSpecialCharacters_ShouldSucceed()
			{
				string subject = "A-B-C!";

				async Task Act()
					=> await That(subject).IsUpperCased();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenActualIsWhitespace_ShouldSucceed()
			{
				string subject = " \t\r\n";

				async Task Act()
					=> await That(subject).IsUpperCased();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
