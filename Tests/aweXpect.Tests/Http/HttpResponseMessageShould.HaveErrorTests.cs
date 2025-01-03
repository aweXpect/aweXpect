﻿#if NET8_0_OR_GREATER
using System.Net;
using System.Net.Http;

namespace aweXpect.Tests.Http;

public sealed partial class HttpResponseMessageShould
{
	public sealed class HaveError
	{
		public sealed class Tests
		{
			[Theory]
			[MemberData(nameof(ClientErrorStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			[MemberData(nameof(ServerErrorStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			public async Task WhenStatusCodeIsExpected_ShouldSucceed(HttpStatusCode statusCode)
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(statusCode);

				async Task Act()
					=> await That(subject).Should().HaveError();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[MemberData(nameof(SuccessStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			[MemberData(nameof(RedirectStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			public async Task WhenStatusCodeIsUnexpected_ShouldFail(HttpStatusCode statusCode)
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(statusCode);

				async Task Act()
					=> await That(subject).Should().HaveError();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("*have an error (status code 4xx or 5xx)*")
					.AsWildcard();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				HttpResponseMessage? subject = null;

				async Task Act()
					=> await That(subject).Should().HaveError();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have an error (status code 4xx or 5xx),
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
