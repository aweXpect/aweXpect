using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class DateTimeFormatterTests
	{
		[Fact]
		public async Task Nullable_ShouldUseRoundtripFormat()
		{
			DateTime? value = new(2024, 11, 2, 15, 42, 08, 123);
			string expectedResult = "2024-11-02T15:42:08.1230000";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task ShouldUseRoundtripFormat()
		{
			DateTime value = new(2024, 11, 2, 15, 42, 08, 123);
			string expectedResult = "2024-11-02T15:42:08.1230000";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task WhenNull_ShouldUseDefaultNullString()
		{
			DateTime? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(ValueFormatter.NullString);
			await That(sb.ToString()).Should().Be(ValueFormatter.NullString);
		}
	}
}
