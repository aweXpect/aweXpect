﻿#if NET8_0_OR_GREATER
using System.Net.Http;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatHttpResponseMessageShould
{
	/// <summary>
	///     Verifies that the response has a server error status code (5xx)
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThatShould<HttpResponseMessage?>>
		HaveServerError(
			this IThatShould<HttpResponseMessage?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode >= 500 && statusCode < 600,
					"have server error (status code 5xx)")),
			source);
}
#endif
