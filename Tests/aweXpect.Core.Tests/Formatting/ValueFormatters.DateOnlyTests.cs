#if NET8_0_OR_GREATER
using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class DateOnlyTests
	{
		[Fact]
		public async Task Nullable_ShouldUseRoundtripFormat()
		{
			DateOnly? value = new(2024, 11, 2);
			string expectedResult = "2024-11-02";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Is(expectedResult);
			await That(objectResult).Is(expectedResult);
			await That(sb.ToString()).Is(expectedResult);
		}

		[Fact]
		public async Task ShouldUseRoundtripFormat()
		{
			DateOnly value = new(2024, 11, 2);
			string expectedResult = "2024-11-02";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Is(expectedResult);
			await That(objectResult).Is(expectedResult);
			await That(sb.ToString()).Is(expectedResult);
		}

		[Fact]
		public async Task WhenNull_ShouldUseDefaultNullString()
		{
			DateOnly? value = null;
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
#endif
