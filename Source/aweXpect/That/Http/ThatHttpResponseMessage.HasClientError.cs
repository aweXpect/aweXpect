#if NET8_0_OR_GREATER
using System.Net.Http;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatHttpResponseMessage
{
	/// <summary>
	///     Verifies that the response has a client error status code (4xx)
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThat<HttpResponseMessage?>>
		HasClientError(this IThat<HttpResponseMessage?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode >= 400 && statusCode < 500,
					"has client error (status code 4xx)")),
			source);
}
#endif
