#if NET8_0_OR_GREATER
using System.Net.Http;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatHttpResponseMessage
{
	/// <summary>
	///     Verifies that the response has a redirection status code (3xx)
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IExpectSubject<HttpResponseMessage?>>
		IsRedirection(this IExpectSubject<HttpResponseMessage?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode >= 300 && statusCode < 400,
					"be redirection (status code 3xx)")),
			source);
}
#endif
