using System.Collections.Generic;
using System.Linq;

namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class CollectionFormatterTests
{
	[Fact]
	public async Task ShouldFormatItems()
	{
		string expectedResult = "[\"1\", \"2\", \"3\", \"4\"]";
		IEnumerable<string> value = Enumerable.Range(1, 4).Select(x => x.ToString());

		string result = Formatter.Format(value);

		await That(result).Should().Be(expectedResult);
	}

	[Fact]
	public async Task ShouldLimitTo10Items()
	{
		string expectedResult = "[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, …]";
		IEnumerable<int> value = Enumerable.Range(1, 20);

		string result = Formatter.Format(value);

		await That(result).Should().Be(expectedResult);
	}
}
