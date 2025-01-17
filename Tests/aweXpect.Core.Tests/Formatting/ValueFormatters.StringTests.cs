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

			await That(result).Is(expectedResult);
			await That(objectResult).Is(expectedResult);
			await That(sb.ToString()).Is(expectedResult);
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

			await That(result).Is(expectedResult);
			await That(objectResult).Is(expectedResult);
			await That(sb.ToString()).Is(expectedResult);
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

			await That(result).Is(expectedResult);
			await That(sb.ToString()).Is(expectedResult);
		}

		[Fact]
		public async Task WhenNull_ShouldUseDefaultNullString()
		{
			string? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Is(ValueFormatter.NullString);
			await That(objectResult).Is(ValueFormatter.NullString);
			await That(sb.ToString()).Is(ValueFormatter.NullString);
		}
	}
}
