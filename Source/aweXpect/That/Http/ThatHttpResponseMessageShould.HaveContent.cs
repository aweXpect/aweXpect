#if NET6_0_OR_GREATER
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatHttpResponseMessageShould
{
	/// <summary>
	///     Verifies that the string content is equal to <paramref name="expected" />
	/// </summary>
	public static StringMatcherResult<HttpResponseMessage, IThat<HttpResponseMessage?>>
		HaveContent(
			this IThat<HttpResponseMessage?> source,
			StringMatcher expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new HasContentConstraint(it, expected)),
			source,
			expected);

	private readonly struct HasContentConstraint(string it, StringMatcher expected)
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
			if (expected.Matches(message))
			{
				return new ConstraintResult.Success<HttpResponseMessage?>(actual, ToString());
			}

			return new ConstraintResult.Failure<HttpResponseMessage?>(actual, ToString(),
				expected.GetExtendedFailure(it, message));
		}

		public override string ToString()
			=> $"have a string content {expected.GetExpectation(StringMatcher.GrammaticVoice.PassiveVoice)}";
	}
}
#endif
