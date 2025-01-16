#if NET8_0_OR_GREATER
using System.Net;
using System.Net.Http;

namespace aweXpect.Tests.Http;

public sealed partial class HttpResponseMessageShould
{
	public sealed class HaveClientError
	{
		public sealed class Tests
		{
			[Theory]
			[MemberData(nameof(ClientErrorStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			public async Task WhenStatusCodeIsExpected_ShouldSucceed(HttpStatusCode statusCode)
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(statusCode);

				async Task Act()
					=> await That(subject).Should().HaveClientError();

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[MemberData(nameof(SuccessStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			[MemberData(nameof(RedirectStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			[MemberData(nameof(ServerErrorStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			public async Task WhenStatusCodeIsUnexpected_ShouldFail(HttpStatusCode statusCode)
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(statusCode);

				async Task Act()
					=> await That(subject).Should().HaveClientError();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("*have client error (status code 4xx)*")
					.AsWildcard();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				HttpResponseMessage? subject = null;

				async Task Act()
					=> await That(subject).Should().HaveClientError();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have client error (status code 4xx),
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
