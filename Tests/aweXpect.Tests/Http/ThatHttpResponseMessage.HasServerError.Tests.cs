﻿#if NET8_0_OR_GREATER
using System.Net;
using System.Net.Http;

namespace aweXpect.Tests;

public sealed partial class ThatHttpResponseMessage
{
	public sealed class HasServerError
	{
		public sealed class Tests
		{
			[Theory]
			[MemberData(nameof(ServerErrorStatusCodes), MemberType = typeof(ThatHttpResponseMessage))]
			public async Task WhenStatusCodeIsExpected_ShouldSucceed(HttpStatusCode statusCode)
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(statusCode);

				async Task Act()
					=> await That(subject).HasServerError();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[MemberData(nameof(SuccessStatusCodes), MemberType = typeof(ThatHttpResponseMessage))]
			[MemberData(nameof(RedirectStatusCodes), MemberType = typeof(ThatHttpResponseMessage))]
			[MemberData(nameof(ClientErrorStatusCodes), MemberType = typeof(ThatHttpResponseMessage))]
			public async Task WhenStatusCodeIsUnexpected_ShouldFail(HttpStatusCode statusCode)
			{
				HttpResponseMessage subject = ResponseBuilder
					.WithStatusCode(statusCode);

				async Task Act()
					=> await That(subject).HasServerError();

				await That(Act).Throws<XunitException>()
					.WithMessage("*have server error (status code 5xx)*")
					.AsWildcard();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				HttpResponseMessage? subject = null;

				async Task Act()
					=> await That(subject).HasServerError();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             have server error (status code 5xx),
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
