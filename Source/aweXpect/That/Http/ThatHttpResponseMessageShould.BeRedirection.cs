#if NET6_0_OR_GREATER
using System.Net.Http;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatHttpResponseMessageShould
{
	/// <summary>
	///     Verifies that the response has a redirection status code (3xx)
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThat<HttpResponseMessage?>>
		BeRedirection(
			this IThat<HttpResponseMessage?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode >= 300 && statusCode < 400,
					"be redirection (status code 3xx)")),
			source);
}
#endif
