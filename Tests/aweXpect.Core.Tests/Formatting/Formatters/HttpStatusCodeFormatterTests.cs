#if NET6_0_OR_GREATER
using System.Net;
using System.Text;

namespace aweXpect.Core.Tests.Formatting.Formatters;

public sealed class HttpStatusCodeFormatterTests
{
	[Theory]
	[InlineData(HttpStatusCode.OK, "200 OK")]
	[InlineData(HttpStatusCode.BadRequest, "400 BadRequest")]
	public async Task ShouldIncludeNumberAndDescription(HttpStatusCode value, string expectedResult)
	{
		StringBuilder sb = new();
		
		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}
}
#endif
