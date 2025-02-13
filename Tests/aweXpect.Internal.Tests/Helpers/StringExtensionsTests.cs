using aweXpect.Helpers;

namespace aweXpect.Internal.Tests.Helpers;

public class StringExtensionsTests
{
	[Fact]
	public async Task Indent_WhenIndentationIsEmpty_ShouldReturnInput()
	{
		string input = "foo\nbar";

		string result = input.Indent("");

		await That(result).IsEqualTo(input);
	}

	[Fact]
	public async Task Indent_WhenIndentationIsNotEmpty_ShouldReturnIndentedInput()
	{
		string input = "foo\nbar";
		string expected = "   foo\n   bar";

		string result = input.Indent("   ");

		await That(result).IsEqualTo(expected);
	}

	[Fact]
	public async Task Indent_WhenIndentFirstLineIsFalse_ShouldOnlyIndentSubsequentLines()
	{
		string input = "foo\nbar";
		string expected = "foo\n   bar";

		string result = input.Indent("   ", false);

		await That(result).IsEqualTo(expected);
	}

	[Fact]
	public async Task Indent_WhenNull_ShouldReturnNull()
	{
		string? input = null;

		string? result = input.Indent();

		await That(result).IsNull();
	}

	[Theory]
	[InlineData("", "a ")]
	[InlineData("apple", "an apple")]
	[InlineData("bee", "a bee")]
	[InlineData("Exception", "an Exception")]
	[InlineData("NotSupportedException", "a NotSupportedException")]
	public async Task PrependAOrAn(string input, string expected)
	{
		string result = input.PrependAOrAn();

		await That(result).IsEqualTo(expected);
	}
}
