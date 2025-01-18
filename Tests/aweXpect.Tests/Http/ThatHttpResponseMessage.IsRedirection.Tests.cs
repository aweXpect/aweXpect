#if NET8_0_OR_GREATER
using System.Net;
using System.Net.Http;

namespace aweXpect.Tests;

public sealed partial class ThatHttpResponseMessage
{
	public sealed class IsRedirection
	{
		public sealed class Tests
		{
			[Theory]
			[MemberData(nameof(RedirectStatusCodes), MemberType = typeof(ThatHttpResponseMessage))]
			public async Task WhenStatusCodeIsExpected_ShouldSucceed(HttpStatusCode statusCode)
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(statusCode);

				async Task Act()
					=> await That(subject).IsRedirection();

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[MemberData(nameof(SuccessStatusCodes), MemberType = typeof(ThatHttpResponseMessage))]
			[MemberData(nameof(ClientErrorStatusCodes), MemberType = typeof(ThatHttpResponseMessage))]
			[MemberData(nameof(ServerErrorStatusCodes), MemberType = typeof(ThatHttpResponseMessage))]
			public async Task WhenStatusCodeIsUnexpected_ShouldFail(HttpStatusCode statusCode)
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(statusCode);

				async Task Act()
					=> await That(subject).IsRedirection();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("*be redirection (status code 3xx)*")
					.AsWildcard();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				HttpResponseMessage? subject = null;

				async Task Act()
					=> await That(subject).IsRedirection();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be redirection (status code 3xx),
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
