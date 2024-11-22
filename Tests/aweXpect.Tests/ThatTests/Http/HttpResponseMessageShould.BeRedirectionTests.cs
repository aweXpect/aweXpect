#if NET6_0_OR_GREATER
using System.Net;
using System.Net.Http;

namespace Testably.Expectations.Tests.ThatTests.Http;

public sealed partial class HttpResponseMessageShould
{
	public sealed class BeRedirectionTests
	{
		[Theory]
		[MemberData(nameof(RedirectStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
		public async Task WhenStatusCodeIsExpected_ShouldSucceed(HttpStatusCode statusCode)
		{
			HttpResponseMessage subject = ResponseBuilder
				.WithStatusCode(statusCode);

			async Task Act()
				=> await That(subject).Should().BeRedirection();

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[MemberData(nameof(SuccessStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
		[MemberData(nameof(ClientErrorStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
		[MemberData(nameof(ServerErrorStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
		public async Task WhenStatusCodeIsUnexpected_ShouldFail(HttpStatusCode statusCode)
		{
			HttpResponseMessage subject = ResponseBuilder
				.WithStatusCode(statusCode);

			async Task Act()
				=> await That(subject).Should().BeRedirection();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("*be redirection (status code 3xx)*")
				.AsWildcard();
		}
	}
}
#endif
