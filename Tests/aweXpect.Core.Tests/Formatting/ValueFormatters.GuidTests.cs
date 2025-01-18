using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class GuidTests
	{
		[Fact]
		public async Task Empty_ShouldUseDefaultFormat()
		{
			Guid value = Guid.Empty;
			string expectedResult = "00000000-0000-0000-0000-000000000000";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Is(expectedResult);
			await That(objectResult).Is(expectedResult);
			await That(sb.ToString()).Is(expectedResult);
		}

		[Fact]
		public async Task NullableEmpty_ShouldUseDefaultFormat()
		{
			Guid? value = Guid.Empty;
			string expectedResult = "00000000-0000-0000-0000-000000000000";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Is(expectedResult);
			await That(objectResult).Is(expectedResult);
			await That(sb.ToString()).Is(expectedResult);
		}

		[Fact]
		public async Task NullableShouldUseRoundtripFormat()
		{
			Guid? value = Guid.NewGuid();
			string? expectedResult = value.ToString();
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
			Guid value = Guid.NewGuid();
			string expectedResult = value.ToString();
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
			Guid? value = null;
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
