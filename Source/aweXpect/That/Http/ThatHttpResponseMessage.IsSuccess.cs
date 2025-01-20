#if NET8_0_OR_GREATER
using System.Net.Http;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatHttpResponseMessage
{
	/// <summary>
	///     Verifies that the response has a success status code (2xx)
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThat<HttpResponseMessage?>>
		IsSuccess(this IThat<HttpResponseMessage?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode >= 200 && statusCode < 300,
					"be success (status code 2xx)")),
			source);
}
#endif
