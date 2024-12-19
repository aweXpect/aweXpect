namespace aweXpect.Core.Tests.Options;

public sealed class StringEqualityOptionsTests
{
	[Fact]
	public async Task RegexMatch_ShouldDisplayActualAndPatternUnderneathEachOther()
	{
		string sut = "foo";

		async Task Act()
			=> await That(sut).Should().Be("bar").AsRegex();

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             match "bar",
			             but it did not match
			               ↓ (actual)
			               "foo"
			               "bar"
			               ↑ (regex)
			             """);
	}

	[Fact]
	public async Task RegexMatch_ShouldReplaceNewlines()
	{
		string sut = "foo\nbar";

		async Task Act()
			=> await That(sut).Should().Be("\tsomething\r\nelse").AsRegex();

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected sut to
			             match "\tsomething\r\nelse",
			             but it did not match
			               ↓ (actual)
			               "foo\nbar"
			               "\tsomething\r\nelse"
			               ↑ (regex)
			             """);
	}

	[Fact]
	public async Task WildcardMatch_ShouldDisplayActualAndPatternUnderneathEachOther()
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
	public async Task WildcardMatch_ShouldReplaceNewlines()
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
}
