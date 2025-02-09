﻿#if NET8_0_OR_GREATER
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatHttpResponseMessage
{
	/// <summary>
	///     Verifies that the string content is equal to <paramref name="expected" />
	/// </summary>
	public static StringEqualityTypeResult<HttpResponseMessage, IThat<HttpResponseMessage?>>
		HasContent(this IThat<HttpResponseMessage?> source, string expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<HttpResponseMessage, IThat<HttpResponseMessage?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new HasContentConstraint(it, expected, options)),
			source,
			options);
	}

	private readonly struct HasContentConstraint(string it, string expected, StringEqualityOptions options)
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

			string message = await actual.Content.ReadAsStringAsync(cancellationToken);
			if (options.AreConsideredEqual(message, expected))
			{
				return new ConstraintResult.Success<HttpResponseMessage?>(actual, ToString());
			}

			return new ConstraintResult.Failure<HttpResponseMessage?>(actual, ToString(),
				options.GetExtendedFailure(it, message, expected));
		}

		public override string ToString()
			=> $"has a string content {options.GetExpectation(expected, ExpectationGrammars.None)}";
	}
}
#endif
