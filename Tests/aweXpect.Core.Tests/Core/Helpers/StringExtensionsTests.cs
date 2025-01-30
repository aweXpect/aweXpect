using aweXpect.Core.Helpers;

namespace aweXpect.Core.Tests.Core.Helpers;

public class StringExtensionsTests
{
	[Fact]
	public async Task DisplayWhitespace_ShouldEscapeNewlines()
	{
		string input = "\r,\n;\t ";
		string expected = @"\r,\n;\t ";

		string result = input.DisplayWhitespace();

		await That(result).IsEqualTo(expected);
	}

	[Fact]
	public async Task DisplayWhitespace_WhenNull_ShouldReturnNull()
	{
		string? input = null;

		string? result = input.DisplayWhitespace();

		await That(result).IsNull();
	}


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

	[Fact]
	public async Task RemoveNewlineStyle_ShouldReplaceNewlinesWithSlashN()
	{
		string input = "\ra\r\nb\nc";
		string expected = "\na\nb\nc";

		string result = input.RemoveNewlineStyle();

		await That(result).IsEqualTo(expected);
	}


	[Fact]
	public async Task RemoveNewlineStyle_WhenNull_ShouldReturnNull()
	{
		string? input = null;

		string? result = input.Indent();

		await That(result).IsNull();
	}

	[Fact]
	public async Task SubstringUntilFirst_WhenFirstCharacter_ShouldReturnEmptyString()
	{
		string input = "a,b,c";

		string result = input.SubstringUntilFirst('a');

		await That(result).IsEqualTo("");
	}


	[Fact]
	public async Task SubstringUntilFirst_WhenNotPresent_ShouldReturnString()
	{
		string input = "foo";

		string result = input.SubstringUntilFirst('X');

		await That(result).IsEqualTo(input);
	}

	[Fact]
	public async Task SubstringUntilFirst_WhenPresent_ShouldReturnSubstringUntilFirstOccurrence()
	{
		string input = "a,b,c";

		string result = input.SubstringUntilFirst(',');

		await That(result).IsEqualTo("a");
	}


	[Fact]
	public async Task ToSingleLine_ShouldEscapeNewlines()
	{
		string input = "\r,\n;\t ";
		string expected = "\\r,\\n;\t ";

		string result = input.ToSingleLine();

		await That(result).IsEqualTo(expected);
	}

	[Fact]
	public async Task ToSingleLine_WhenNull_ShouldReturnNull()
	{
		string? input = null;

		string? result = input.ToSingleLine();

		await That(result).IsNull();
	}

	[Fact]
	public async Task TruncateWithEllipsis_WhenLonger_ShouldTruncateWithEllipsis()
	{
		string input = "12345678910";
		string expected = "1234567891…";

		string result = input.TruncateWithEllipsis(10);

		await That(result).IsEqualTo(expected);
	}


	[Fact]
	public async Task TruncateWithEllipsis_WhenNull_ShouldReturnNull()
	{
		string? input = null;

		string? result = input.TruncateWithEllipsis(10);

		await That(result).IsNull();
	}

	[Fact]
	public async Task TruncateWithEllipsis_WhenShorter_ShouldReturnInput()
	{
		string input = "1234567890";

		string result = input.TruncateWithEllipsis(10);

		await That(result).IsEqualTo(input);
	}

	[Fact]
	public async Task TruncateWithEllipsisOnWord_WhenLongerWithoutWordBoundary_ShouldTruncateOnWordWithEllipsis()
	{
		string input = "some word boundary";
		string expected = "some word…";

		string result = input.TruncateWithEllipsisOnWord(11);

		await That(result).IsEqualTo(expected);
	}

	[Fact]
	public async Task TruncateWithEllipsisOnWord_WhenLongerWithoutWordBoundary_ShouldTruncateWithEllipsis()
	{
		string input = "12345678910";
		string expected = "1234567891…";

		string result = input.TruncateWithEllipsisOnWord(10);

		await That(result).IsEqualTo(expected);
	}

	[Fact]
	public async Task TruncateWithEllipsisOnWord_WhenNull_ShouldReturnNull()
	{
		string? input = null;

		string? result = input.TruncateWithEllipsisOnWord(10);

		await That(result).IsNull();
	}

	[Fact]
	public async Task TruncateWithEllipsisOnWord_WhenShorter_ShouldReturnInput()
	{
		string input = "1234567890";

		string result = input.TruncateWithEllipsisOnWord(10);

		await That(result).IsEqualTo(input);
	}

	[Theory]
	[InlineData("another word-boundary", "another wo…")]
	[InlineData("_another word-boundary", "_another…")]
	public async Task TruncateWithEllipsisOnWord_WhenWordBoundaryIsBelow80Percent_ShouldTruncateWithEllipsis(
		string input, string expected)
	{
		string result = input.TruncateWithEllipsisOnWord(10);

		await That(result).IsEqualTo(expected);
	}
}
