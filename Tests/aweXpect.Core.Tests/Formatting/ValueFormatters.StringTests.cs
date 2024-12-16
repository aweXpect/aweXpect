using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class StringTests
	{
		[Fact]
		public async Task Strings_ShouldDefaultToSingleLine()
		{
			string value = "a\nb";
			string expectedResult = """
			                        "a\nb"
			                        """;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task Strings_ShouldUseDoubleQuotationMarks()
		{
			string value = "foo";
			string expectedResult = """
			                        "foo"
			                        """;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task Strings_WhenUsingMultipleLines_ShouldUseNotEscapeNewlines()
		{
			string value = $"a{Environment.NewLine}b";
			string expectedResult = """
			                        "a
			                        b"
			                        """;
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.MultipleLines);
			Formatter.Format(sb, value, FormattingOptions.MultipleLines);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task WhenNull_ShouldUseDefaultNullString()
		{
			string? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(ValueFormatter.NullString);
			await That(sb.ToString()).Should().Be(ValueFormatter.NullString);
		}
	}
}
