namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public sealed partial class IsEqualTo
	{
		public sealed class AsSuffixTests
		{
			[Fact]
			public async Task WhenActualAndExpectedAreNull_ShouldSucceed()
			{
				string? subject = null;
				string? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenActualIsNull_ShouldFail()
			{
				string? subject = null;
				string expected = "some text";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             ends with "some text",
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				string subject = "some text";
				string? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             ends with <null>,
					             but it was "some text"

					             Actual:
					             some text
					             """);
			}

			[Fact]
			public async Task WhenStringEndsWithExpected_ShouldSucceed()
			{
				string subject = "some text without out";
				string expected = "text without out";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenStringHasAdditionalLeadingWhitespace_ShouldSucceed()
			{
				string subject = " \t some text";
				string expected = "some text";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenStringHasMissingLeadingWhitespace_ShouldFail()
			{
				string subject = "some text";
				string expected = " \t some text";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             ends with " \t some text",
					             but it was "some text" which misses some whitespace (" \t " at the beginning)

					             Actual:
					             some text
					             """);
			}

			[Fact]
			public async Task WhenStringHasMissingTrailingWhitespace_ShouldFail()
			{
				string subject = "some text";
				string expected = "some text \t ";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             ends with "some text \t ",
					             but it was "some text" which misses some whitespace (" \t " at the end)

					             Actual:
					             some text
					             """);
			}

			[Fact]
			public async Task WhenStringHasUnexpectedTrailingWhitespace_ShouldFail()
			{
				string subject = "some text \t ";
				string expected = "some text";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             ends with "some text",
					             but it was "some text \t " which has unexpected whitespace (" \t " at the end)

					             Actual:
					             some text 	 
					             """);
			}

			[Fact]
			public async Task WhenStringIsShorter_ShouldFail()
			{
				string subject = "some text with";
				string expected = "some text without out";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             ends with "some text without out",
					             but it was "some text with" with a length of 14 which is shorter than the expected length of 21 and misses:
					               "out out"

					             Actual:
					             some text with
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenStringsAreTheSame_ShouldSucceed(string subject)
			{
				string expected = subject;

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenStringsDiffer_ShouldFail()
			{
				string subject = "actual text";
				string expected = "expected other text";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).AsSuffix();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             ends with "expected other text",
					             but it was "actual text" which differs at index 0:
					                ↓ (actual)
					               "actual text"
					               "expected other text"
					                ↑ (expected)

					             Actual:
					             actual text
					             """);
			}
		}

		public sealed class AsSuffixNegatedTests
		{
			[Fact]
			public async Task WhenStringEndsWithExpected_ShouldFail()
			{
				string subject = "some text without out";
				string expected = "text without out";

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsEqualTo(expected).AsSuffix().IgnoringCase());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with "text without out" ignoring case,
					             but it was "some text without out"

					             Actual:
					             some text without out
					             """);
			}

			[Fact]
			public async Task WhenStringEndsWithExpected_UsingCustomComparer_ShouldFail()
			{
				string subject = "some text without out";
				string expected = "text wIthOUt OUt";

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it
						=> it.IsEqualTo(expected).AsSuffix().Using(new IgnoreCaseForVocalsComparer()));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with "text wIthOUt OUt" using IgnoreCaseForVocalsComparer,
					             but it was "some text without out"

					             Actual:
					             some text without out
					             """);
			}
		}
	}
}
