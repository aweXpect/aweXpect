#if NET8_0_OR_GREATER
using System.Text.Json;
using aweXpect.Json;

namespace aweXpect.Core.Tests.Options;

public class JsonOptionsTests
{
	[Fact]
	public async Task DocumentOptions_ShouldDefaultToAllowTrailingCommas()
	{
		JsonOptions sut = new();

		await That(sut.DocumentOptions.AllowTrailingCommas).IsTrue();
	}

	[Fact]
	public async Task WithJsonOptions_ShouldSetDocumentOptions()
	{
		int maxDepth = new Random().Next(1, 10);
		JsonOptions sut = new();

		sut.WithJsonOptions(o => o with
		{
			MaxDepth = maxDepth
		});

		await That(sut.DocumentOptions.MaxDepth).Is(maxDepth);
	}

	[Fact]
	public async Task WithJsonOptions_ShouldSupportProvidingFixedOptions()
	{
		int maxDepth = new Random().Next(1, 10);
		JsonDocumentOptions documentOptions = new()
		{
			MaxDepth = maxDepth
		};

		JsonOptions sut = new();

		sut.WithJsonOptions(_ => documentOptions);

		await That(sut.DocumentOptions.MaxDepth).Is(maxDepth);
	}
}
#endif
