#if NET8_0_OR_GREATER
using System.Net;
using System.Net.Http;

namespace aweXpect.Tests.Http;

public sealed partial class HttpResponseMessageShould
{
	public sealed class HaveStatusCode
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenFailing_ShouldIncludeRequestInMessage()
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(HttpStatusCode.BadRequest)
					.WithContent("some content")
					.WithRequest(HttpMethod.Get, "https://example.com")
					.WithRequestContent("request content");

				async Task Act()
					=> await That(subject).Should().HaveStatusCode(HttpStatusCode.OK);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have StatusCode 200 OK,
					             but it was 400 BadRequest:
					               HTTP/1.1 400 BadRequest
					               some content
					               The originating request was:
					                 GET https://example.com/ HTTP 1.1
					                 request content
					             """);
			}

			[Fact]
			public async Task WhenFailing_ShouldIncludeResponseContentAndStatusCodeInMessage()
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(HttpStatusCode.BadRequest)
					.WithContent("some content");

				async Task Act()
					=> await That(subject).Should().HaveStatusCode(HttpStatusCode.OK);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have StatusCode 200 OK,
					             but it was 400 BadRequest:
					               HTTP/1.1 400 BadRequest
					               some content
					               The originating request was <null>
					             """);
			}

			[Fact]
			public async Task WhenStatusCodeDiffersFromExpected_ShouldFail()
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(HttpStatusCode.BadRequest);

				async Task Act()
					=> await That(subject).Should().HaveStatusCode(HttpStatusCode.OK);

				await That(Act).Does().Throw<XunitException>();
			}

			[Theory]
			[MemberData(nameof(SuccessStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			[MemberData(nameof(RedirectStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			[MemberData(nameof(ClientErrorStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			[MemberData(nameof(ServerErrorStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			public async Task WhenStatusCodeIsExpected_ShouldSucceed(HttpStatusCode statusCode)
			{
				HttpStatusCode expected = statusCode;
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(statusCode);

				async Task Act()
					=> await That(subject).Should().HaveStatusCode(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				HttpResponseMessage? subject = null;

				async Task Act()
					=> await That(subject).Should().HaveStatusCode(HttpStatusCode.Accepted);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have StatusCode 202 Accepted,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
