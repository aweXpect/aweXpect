namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public sealed class IsNotUpperCased
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsEmpty_ShouldFail()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).IsNotUpperCased();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsNotUpperCased();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsMixedCased_ShouldSucceed()
			{
				string subject = "AbC";

				async Task Act()
					=> await That(subject).IsNotUpperCased();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsNotUpperCased_ShouldLimitDisplayedStringTo100Characters()
			{
				string subject = StringWithMoreThan100Characters.ToUpperInvariant();

				async Task Act()
					=> await That(subject).IsNotUpperCased();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsNotUpperCased();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsUpperCased_ShouldFail()
			{
				string subject = "ABC";

				async Task Act()
					=> await That(subject).IsNotUpperCased();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsNotUpperCased();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsNotUpperCased();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsNotUpperCased();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be upper-cased,
					             but it was " \t "
					             """);
			}
		}
	}
}
