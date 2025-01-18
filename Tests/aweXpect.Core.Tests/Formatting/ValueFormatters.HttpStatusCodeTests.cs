#if NET8_0_OR_GREATER
using System.Net;
using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class HttpStatusCodeTests
	{
		[Theory]
		[InlineData(HttpStatusCode.OK, "200 OK")]
		[InlineData(HttpStatusCode.BadRequest, "400 BadRequest")]
		[InlineData(null, "<null>")]
		public async Task Nullable_ShouldIncludeNumberAndDescription(HttpStatusCode? value, string expectedResult)
		{
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Is(expectedResult);
			await That(objectResult).Is(expectedResult);
			await That(sb.ToString()).Is(expectedResult);
		}

		[Theory]
		[InlineData(HttpStatusCode.OK, "200 OK")]
		[InlineData(HttpStatusCode.BadRequest, "400 BadRequest")]
		public async Task ShouldIncludeNumberAndDescription(HttpStatusCode value, string expectedResult)
		{
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
			HttpStatusCode? value = null;
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
