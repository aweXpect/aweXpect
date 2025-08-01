using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect.Core.Tests.Results;

public sealed class ExecutesInResultTests
{
	[Fact]
	public async Task ShouldBeOptionsProvider_ForTimeSpanEqualityOptions()
	{
		TimeSpanEqualityOptions options = new();
		ExecutesInResult<int[]> sut = CreateSut(Array.Empty<int>(), options);

		await That(sut).Is<IOptionsProvider<TimeSpanEqualityOptions>>()
			.Whose(x => x.Options, it => it.IsSameAs(options));
	}

	private static ExecutesInResult<T> CreateSut<T>(T subject, TimeSpanEqualityOptions options)
		=> new(subject, options);
}
