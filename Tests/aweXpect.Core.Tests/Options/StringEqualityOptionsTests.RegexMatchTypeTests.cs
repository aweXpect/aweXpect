namespace aweXpect.Core.Tests.Options;

public sealed partial class StringEqualityOptionsTests
{
	public sealed class RegexMatchTypeTests
	{
		[Fact]
		public async Task ShouldCompareCaseSensitive()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Should().Be("FOO").AsRegex();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match regex "FOO",
				             but it did not match
				               ↓ (actual)
				               "foo"
				               "FOO"
				               ↑ (regex)
				             """);
		}

		[Fact]
		public async Task ShouldDisplayActualAndPatternUnderneathEachOther()
		{
			string sut = "foo";

			async Task Act()
				=> await That(sut).Should().Be("bar").AsRegex();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(sut).Should().Be("\tsomething\r\nelse").AsRegex();

			await That(Act).Should().Throw<XunitException>()
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
		public async Task WhenIgnoringCase_ShouldCompareCaseInsensitive()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Should().Be("FOO").AsRegex().IgnoringCase();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenPatternIsNull_ShouldFail()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Should().Be(null).AsRegex();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match regex <null>,
				             but it did not match
				               ↓ (actual)
				               "foo"
				               <null>
				               ↑ (regex)
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			string? sut = null;

			async Task Act()
				=> await That(sut).Should().Be(".*").AsRegex();

			await That(Act).Should().Throw<XunitException>()
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
