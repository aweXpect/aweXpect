#if NET8_0_OR_GREATER
using System.Text.Json;
using aweXpect.Customization;
using aweXpect.Json;

namespace aweXpect.Internal.Tests;

public sealed class CustomizeJsonTests
{
	[Fact]
	public async Task SetDefaultJsonDocumentOptions_ShouldApplyOptionsWithinScope()
	{
		string jsonWithTrailingCommas = "[1, 2,]";
		string jsonWithoutTrailingCommas = "[1, 2]";

		async Task Act()
			=> await That(jsonWithTrailingCommas).Should().Be(jsonWithoutTrailingCommas).AsJson();

		using (IDisposable __ = Customize.aweXpect.Json().DefaultJsonDocumentOptions.Set(new JsonDocumentOptions
		       {
			       // Default options set AllowTrailingCommas to true
			       AllowTrailingCommas = false
		       }))
		{
			await That(Act).Does().ThrowException()
				.WithMessage("""
				             Expected jsonWithTrailingCommas to
				             be JSON equivalent to [1, 2],
				             but could not parse subject: The JSON array contains a trailing comma at the end which is not supported in this mode. Change the reader options. LineNumber: 0 | BytePositionInLine: 6.
				             """);
		}

		await That(Act).Does().NotThrow();
	}

	[Fact]
	public async Task ShouldChangeIndividualProperties()
	{
		await That(Customize.aweXpect.Json().DefaultJsonDocumentOptions.Get().AllowTrailingCommas)
			.IsTrue();
		await That(Customize.aweXpect.Json().DefaultJsonSerializerOptions.Get().AllowTrailingCommas)
			.IsTrue();

		using (Customize.aweXpect.Json().DefaultJsonDocumentOptions.Set(new JsonDocumentOptions
		       {
			       // Default options set AllowTrailingCommas to true
			       AllowTrailingCommas = false
		       }))
		{
			await That(Customize.aweXpect.Json().DefaultJsonDocumentOptions.Get().AllowTrailingCommas)
				.IsFalse();
			await That(Customize.aweXpect.Json().DefaultJsonSerializerOptions.Get().AllowTrailingCommas)
				.IsTrue();
		}

		using (Customize.aweXpect.Json().DefaultJsonSerializerOptions.Set(new JsonSerializerOptions
		       {
			       // Default options set AllowTrailingCommas to true
			       AllowTrailingCommas = false
		       }))
		{
			await That(Customize.aweXpect.Json().DefaultJsonDocumentOptions.Get().AllowTrailingCommas)
				.IsTrue();
			await That(Customize.aweXpect.Json().DefaultJsonSerializerOptions.Get().AllowTrailingCommas)
				.IsFalse();
		}

		await That(Customize.aweXpect.Json().DefaultJsonDocumentOptions.Get().AllowTrailingCommas)
			.IsTrue();
		await That(Customize.aweXpect.Json().DefaultJsonSerializerOptions.Get().AllowTrailingCommas)
			.IsTrue();
	}
}
#endif
