﻿#if NET8_0_OR_GREATER
using System.Net;
using System.Net.Http;

namespace aweXpect.Tests.Http;

public sealed partial class HttpResponseMessageShould
{
	public sealed class NotHaveStatusCode
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenStatusCodeDiffersFromExpected_ShouldSucceed()
			{
				HttpStatusCode unexpected = HttpStatusCode.OK;
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(HttpStatusCode.BadRequest);

				async Task Act()
					=> await That(subject).Should().NotHaveStatusCode(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[MemberData(nameof(SuccessStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			[MemberData(nameof(RedirectStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			[MemberData(nameof(ClientErrorStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			[MemberData(nameof(ServerErrorStatusCodes), MemberType = typeof(HttpResponseMessageShould))]
			public async Task WhenStatusCodeIsUnexpected_ShouldFail(HttpStatusCode statusCode)
			{
				HttpStatusCode unexpected = statusCode;
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(statusCode);

				async Task Act()
					=> await That(subject).Should().NotHaveStatusCode(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("*StatusCode different to*")
					.AsWildcard();
			}
		}
	}
}
#endif
