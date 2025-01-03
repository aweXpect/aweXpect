namespace aweXpect.Core.Tests.Options;

public sealed partial class StringEqualityOptionsTests
{
	public sealed class WildcardMatchTypeTests
	{
		[Theory]
		[InlineData(false)]
		[InlineData(true)]
		public async Task ShouldCompareCaseSensitive(bool ignoreCase)
		{
			string sut = "foo\nbar";

			async Task Act()
				=> await That(sut).Should().Be("FOO\nBAR").AsWildcard().IgnoringCase(ignoreCase);

			await That(Act).Should().Throw<XunitException>().OnlyIf(!ignoreCase)
				.WithMessage("""
				             Expected sut to
				             match "FOO\nBAR",
				             but it did not match
				               ↓ (actual)
				               "foo\nbar"
				               "FOO\nBAR"
				               ↑ (wildcard pattern)
				             """);
		}

		[Fact]
		public async Task ShouldDisplayActualAndPatternUnderneathEachOther()
		{
			string sut = "foo";

			async Task Act()
				=> await That(sut).Should().Be("bar").AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match "bar",
				             but it did not match
				               ↓ (actual)
				               "foo"
				               "bar"
				               ↑ (wildcard pattern)
				             """);
		}

		[Fact]
		public async Task ShouldReplaceNewlines()
		{
			string sut = "foo\nbar";

			async Task Act()
				=> await That(sut).Should().Be("\tsomething\r\nelse").AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match "\tsomething\r\nelse",
				             but it did not match
				               ↓ (actual)
				               "foo\nbar"
				               "\tsomething\r\nelse"
				               ↑ (wildcard pattern)
				             """);
		}

		[Fact]
		public async Task ShouldSupportPassiveGrammaticVoice()
		{
			Exception exception = new("foo");

			async Task Act()
				=> await That(() => Task.FromException(exception)).Should().ThrowException().WithMessage("bar")
					.AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected () => Task.FromException(exception) to
				             throw an exception with Message matching "bar",
				             but it did not match
				               ↓ (actual)
				               "foo"
				               "bar"
				               ↑ (wildcard pattern)
				             """);
		}

		[Fact]
		public async Task WhenPatternIsNull_ShouldFail()
		{
			string? sut = "foo";

			async Task Act()
				=> await That(sut).Should().Be(null).AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match <null>,
				             but could not compare the <null> wildcard pattern with "foo"
				             """);
		}

		[Fact]
		public async Task WhenSubjectAndPatternAreNull_ShouldFail()
		{
			string? sut = null;

			async Task Act()
				=> await That(sut).Should().Be(null).AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match <null>,
				             but could not compare the <null> wildcard pattern with <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			string? sut = null;

			async Task Act()
				=> await That(sut).Should().Be("*").AsWildcard();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             match "*",
				             but it did not match
				               ↓ (actual)
				               <null>
				               "*"
				               ↑ (wildcard pattern)
				             """);
		}
	}
}
