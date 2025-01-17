namespace aweXpect.Core.Tests.Options;

public sealed partial class StringEqualityOptionsTests
{
	public sealed class ExactMatchTypeTests
	{
		[Theory]
		[InlineData(false)]
		[InlineData(true)]
		public async Task ShouldCompareCaseSensitive(bool ignoreCase)
		{
			string sut = "foo\nbar";

			async Task Act()
				=> await That(sut).Is("FOO\nBAR").Exactly().IgnoringCase(ignoreCase);

			await That(Act).Does().Throw<XunitException>().OnlyIf(!ignoreCase)
				.WithMessage("""
				             Expected sut to
				             be equal to "FOO\nBAR",
				             but it was "foo\nbar" which differs at index 0:
				                ↓ (actual)
				               "foo\nbar"
				               "FOO\nBAR"
				                ↑ (expected)
				             """);
		}

		[Fact]
		public async Task ShouldDisplayActualAndPatternUnderneathEachOther()
		{
			string sut = "foo";

			async Task Act()
				=> await That(sut).Is("bar").Exactly();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             be equal to "bar",
				             but it was "foo" which differs at index 0:
				                ↓ (actual)
				               "foo"
				               "bar"
				                ↑ (expected)
				             """);
		}

		[Fact]
		public async Task ShouldReplaceNewlines()
		{
			string sut = "foo\nbar";

			async Task Act()
				=> await That(sut).Is("\tsomething\r\nelse").Exactly();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             be equal to "\tsomething\r\nelse",
				             but it was "foo\nbar" which differs at index 0:
				                ↓ (actual)
				               "foo\nbar"
				               "\tsomething\r\nelse"
				                ↑ (expected)
				             """);
		}

		[Fact]
		public async Task ShouldSupportPassiveGrammaticVoice()
		{
			Exception exception = new("foo");

			async Task Act()
				=> await That(() => Task.FromException(exception)).Does().ThrowException().WithMessage("bar")
					.Exactly();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected () => Task.FromException(exception) to
				             throw an exception with Message equal to "bar",
				             but it was "foo" which differs at index 0:
				                ↓ (actual)
				               "foo"
				               "bar"
				                ↑ (expected)
				             """);
		}

		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Is(null).Exactly();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             be equal to <null>,
				             but it was "foo"
				             """);
		}

		[Fact]
		public async Task WhenIgnoringCase_ShouldCompareCaseInsensitive()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Is("FOO").Exactly().IgnoringCase();

			await That(Act).Does().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreNull_ShouldSucceed()
		{
			string? sut = null;

			async Task Act()
				=> await That(sut).Is(null).Exactly();

			await That(Act).Does().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			string? sut = null;

			async Task Act()
				=> await That(sut).Is("").Exactly();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             be equal to "",
				             but it was <null>
				             """);
		}
	}
}
