#if NET8_0_OR_GREATER
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatHttpResponseMessage
{
	/// <summary>
	///     Verifies that the response has a status code equal to <paramref name="expected" />
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThat<HttpResponseMessage?>>
		HasStatusCode(this IThat<HttpResponseMessage?> source, HttpStatusCode expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new HasStatusCodeConstraint(it, expected)),
			source);

	/// <summary>
	///     Verifies that the response has a status code different to <paramref name="unexpected" />
	/// </summary>
	public static AndOrResult<HttpResponseMessage, IThat<HttpResponseMessage?>>
		DoesNotHaveStatusCode(this IThat<HttpResponseMessage?> source,
			HttpStatusCode unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new HasStatusCodeRangeConstraint(
					it,
					statusCode => statusCode != (int)unexpected,
					$"have StatusCode different to {Formatter.Format(unexpected)}")),
			source);

	private readonly struct HasStatusCodeConstraint(string it, HttpStatusCode expected)
		: IAsyncConstraint<HttpResponseMessage>
	{
		public async Task<ConstraintResult> IsMetBy(
			HttpResponseMessage? actual,
			CancellationToken cancellationToken)
		{
			if (actual == null)
			{
				return new ConstraintResult.Failure<HttpResponseMessage?>(actual, ToString(),
					$"{it} was <null>");
			}

			if (actual.StatusCode == expected)
			{
				return new ConstraintResult.Success<HttpResponseMessage?>(actual, ToString());
			}

			string formattedResponse =
				await HttpResponseMessageFormatter.Format(actual, "  ", cancellationToken);
			return new ConstraintResult.Failure<HttpResponseMessage?>(actual, ToString(),
				$"{it} was {Formatter.Format(actual.StatusCode)}:{Environment.NewLine}{formattedResponse}");
		}

		public override string ToString()
			=> $"have StatusCode {Formatter.Format(expected)}";
	}
}
#endif
