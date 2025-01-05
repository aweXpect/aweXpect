﻿#if NET8_0_OR_GREATER
using System.Text.Json;
using aweXpect.Customization;

namespace aweXpect.Core.Tests.Customization;

public sealed class CustomizeJsonTests
{
	[Fact]
	public async Task SetDefaultJsonDocumentOptions_ShouldApplyOptionsWithinScope()
	{
		string jsonWithTrailingCommas = "[1, 2,]";
		string jsonWithoutTrailingCommas = "[1, 2]";

		async Task Act()
			=> await That(jsonWithTrailingCommas).Should().Be(jsonWithoutTrailingCommas).AsJson();

		using (IDisposable __ = Customize.Json.SetDefaultJsonDocumentOptions(new JsonDocumentOptions
		       {
			       // Default options set AllowTrailingCommas to true
			       AllowTrailingCommas = false
		       }))
		{
			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected jsonWithTrailingCommas to
				             be JSON equivalent to [1, 2],
				             but could not parse subject: The JSON array contains a trailing comma at the end which is not supported in this mode. Change the reader options. LineNumber: 0 | BytePositionInLine: 6.
				             """);
		}

		await That(Act).Should().NotThrow();
	}
}
#endif
