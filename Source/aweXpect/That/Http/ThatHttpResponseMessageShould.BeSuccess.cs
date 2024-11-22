#if NET6_0_OR_GREATER
using System.Net.Http;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatHttpResponseMessageShould
{
	/// <summary>
	///     Verifies that the response has a success status code (2xx)
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThat<HttpResponseMessage?>>
		BeSuccess(
			this IThat<HttpResponseMessage?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode >= 200 && statusCode < 300,
					"be success (status code 2xx)")),
			source);
}
#endif
