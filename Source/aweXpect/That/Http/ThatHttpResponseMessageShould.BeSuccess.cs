#if NET8_0_OR_GREATER
using System.Net.Http;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatHttpResponseMessageShould
{
	/// <summary>
	///     Verifies that the response has a success status code (2xx)
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThatShould<HttpResponseMessage?>>
		BeSuccess(
			this IThatShould<HttpResponseMessage?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode >= 200 && statusCode < 300,
					"be success (status code 2xx)")),
			source);
}
#endif
