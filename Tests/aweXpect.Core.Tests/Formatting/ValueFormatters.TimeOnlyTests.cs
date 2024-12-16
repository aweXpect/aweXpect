#if NET6_0_OR_GREATER
using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class TimeOnlyTests
	{
		[Fact]
		public async Task Nullable_ShouldUseRoundtripFormat()
		{
			TimeOnly? value = new(15, 42, 15, 234);
			string expectedResult = "15:42:15.2340000";
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
			TimeOnly value = new(15, 42, 15, 234);
			string expectedResult = "15:42:15.2340000";
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
			TimeOnly? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(ValueFormatter.NullString);
			await That(sb.ToString()).Should().Be(ValueFormatter.NullString);
		}
	}
}
#endif
