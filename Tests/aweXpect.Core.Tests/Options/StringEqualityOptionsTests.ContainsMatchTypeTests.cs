using aweXpect.Options;

namespace aweXpect.Core.Tests.Options;

public sealed partial class StringEqualityOptionsTests
{
	public sealed class ContainsMatchTypeTests
	{
		[Fact]
		public async Task Contains_ShouldReturnSameInstance()
		{
			StringEqualityOptions sut = new();

			StringEqualityOptions result = sut.Contains();

			await That(result).IsSameAs(sut);
		}
	}
}
