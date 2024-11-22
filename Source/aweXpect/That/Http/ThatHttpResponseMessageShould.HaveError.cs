#if NET6_0_OR_GREATER
using System.Net.Http;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatHttpResponseMessageShould
{
	/// <summary>
	///     Verifies that the response has a client or server error status code (4xx or 5xx)
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThat<HttpResponseMessage?>>
		HaveError(
			this IThat<HttpResponseMessage?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode >= 400 && statusCode < 600,
					"have an error (status code 4xx or 5xx)")),
			source);
}
#endif
