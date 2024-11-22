#if NET6_0_OR_GREATER
using System.Net.Http;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatHttpResponseMessageShould
{
	/// <summary>
	///     Verifies that the response has a server error status code (5xx)
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThat<HttpResponseMessage?>>
		HaveServerError(
			this IThat<HttpResponseMessage?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode >= 500 && statusCode < 600,
					"have server error (status code 5xx)")),
			source);
}
#endif
