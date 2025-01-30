using System.Linq;
using aweXpect.Customization;

namespace aweXpect.Core.Tests.Customization;

public sealed class CustomizeFormattingTests
{
	[Fact]
	public async Task MaximumNumberOfCollectionItems_ShouldBeUsedInFormatter()
	{
		int[] items = Enumerable.Range(1, 6).ToArray();
		using (IDisposable _ = Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Set(3))
		{
			await That(Formatter.Format(items)).IsEqualTo("[1, 2, 3, …]");
		}

		using (IDisposable _ = Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Set(5))
		{
			await That(Formatter.Format(items)).IsEqualTo("[1, 2, 3, 4, 5, …]");
		}

		await That(Formatter.Format(items)).IsEqualTo("[1, 2, 3, 4, 5, 6]");
	}
}
