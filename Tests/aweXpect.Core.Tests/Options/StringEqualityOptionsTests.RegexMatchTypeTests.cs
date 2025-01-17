namespace aweXpect.Core.Tests.Options;

public sealed partial class StringEqualityOptionsTests
{
	public sealed class RegexMatchTypeTests
	{
		[Theory]
		[InlineData(false)]
		[InlineData(true)]
		public async Task ShouldCompareCaseSensitive(bool ignoreCase)
		{
			string sut = "foo\nbar";

			async Task Act()
				=> await That(sut).Is("FOO\nBAR").AsRegex().IgnoringCase(ignoreCase);

			await That(Act).Does().Throw<XunitException>().OnlyIf(!ignoreCase)
				.WithMessage("""
				             Expected sut to
				             match regex "FOO\nBAR",
				             but it did not match
				               ↓ (actual)
				               "foo\nbar"
				               "FOO\nBAR"
				               ↑ (regex)
				             """);
		}

		[Fact]
		public async Task ShouldDisplayActualAndPatternUnderneathEachOther()
		{
			string sut = "foo";

			async Task Act()
				=> await That(sut).Is("bar").AsRegex();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match regex "bar",
				             but it did not match
				               ↓ (actual)
				               "foo"
				               "bar"
				               ↑ (regex)
				             """);
		}

		[Fact]
		public async Task ShouldReplaceNewlines()
		{
			string sut = "foo\nbar";

			async Task Act()
				=> await That(sut).Is("\tsomething\r\nelse").AsRegex();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match regex "\tsomething\r\nelse",
				             but it did not match
				               ↓ (actual)
				               "foo\nbar"
				               "\tsomething\r\nelse"
				               ↑ (regex)
				             """);
		}

		[Fact]
		public async Task ShouldSupportPassiveGrammaticVoice()
		{
			Exception exception = new("foo");

			async Task Act()
				=> await That(() => Task.FromException(exception)).Does().ThrowException().WithMessage("bar")
					.AsRegex();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected () => Task.FromException(exception) to
				             throw an exception with Message matching regex "bar",
				             but it did not match
				               ↓ (actual)
				               "foo"
				               "bar"
				               ↑ (regex)
				             """);
		}

		[Fact]
		public async Task WhenIgnoringCase_ShouldCompareCaseInsensitive()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Is("FOO").AsRegex().IgnoringCase();

			await That(Act).Does().NotThrow();
		}

		[Fact]
		public async Task WhenPatternIsNull_ShouldFail()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Is(null).AsRegex();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match regex <null>,
				             but could not compare the <null> regex with "foo"
				             """);
		}

		[Fact]
		public async Task WhenSubjectAndPatternAreNull_ShouldFail()
		{
			string? sut = null;

			async Task Act()
				=> await That(sut).Is(null).AsRegex();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match regex <null>,
				             but could not compare the <null> regex with <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			string? sut = null;

			async Task Act()
				=> await That(sut).Is(".*").AsRegex();

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match regex ".*",
				             but it did not match
				               ↓ (actual)
				               <null>
				               ".*"
				               ↑ (regex)
				             """);
		}
	}
}
