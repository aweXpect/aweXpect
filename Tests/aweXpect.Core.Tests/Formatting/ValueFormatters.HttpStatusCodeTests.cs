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

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Theory]
		[InlineData(HttpStatusCode.OK, "HttpStatusCode 200 OK")]
		[InlineData(HttpStatusCode.BadRequest, "HttpStatusCode 400 BadRequest")]
		[InlineData(null, "<null>")]
		public async Task Nullable_WithType_ShouldIncludeNumberAndDescription(HttpStatusCode? value,
			string expectedResult)
		{
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.WithType);
			string objectResult = Formatter.Format((object?)value, FormattingOptions.WithType);
			Formatter.Format(sb, value, FormattingOptions.WithType);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
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

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task WhenNull_ShouldUseDefaultNullString()
		{
			HttpStatusCode? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(ValueFormatter.NullString);
			await That(objectResult).IsEqualTo(ValueFormatter.NullString);
			await That(sb.ToString()).IsEqualTo(ValueFormatter.NullString);
		}

		[Theory]
		[InlineData(HttpStatusCode.OK, "HttpStatusCode 200 OK")]
		[InlineData(HttpStatusCode.BadRequest, "HttpStatusCode 400 BadRequest")]
		public async Task WithType_ShouldIncludeNumberAndDescription(HttpStatusCode value, string expectedResult)
		{
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.WithType);
			string objectResult = Formatter.Format((object?)value, FormattingOptions.WithType);
			Formatter.Format(sb, value, FormattingOptions.WithType);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}
	}
}
#endif
