#if NET8_0_OR_GREATER
using System.Net.Http;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatHttpResponseMessageShould
{
	/// <summary>
	///     Verifies that the response has a client error status code (4xx)
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThatShould<HttpResponseMessage?>>
		HaveClientError(
			this IThatShould<HttpResponseMessage?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode >= 400 && statusCode < 500,
					"have client error (status code 4xx)")),
			source);
}
#endif
