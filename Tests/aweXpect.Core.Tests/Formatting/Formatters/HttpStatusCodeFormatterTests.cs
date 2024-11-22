#if NET6_0_OR_GREATER
using System.Net;

namespace aweXpect.Core.Tests.Formatting.Formatters;

public sealed class HttpStatusCodeFormatterTests
{
	[Theory]
	[InlineData(HttpStatusCode.OK, "200 OK")]
	[InlineData(HttpStatusCode.BadRequest, "400 BadRequest")]
	public async Task ShouldIncludeNumberAndDescription(HttpStatusCode value, string expectedResult)
	{
		string result = Formatter.Format(value);

		await That(result).Should().Be(expectedResult);
	}
}
#endif
