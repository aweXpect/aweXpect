using aweXpect.Core.Helpers;

namespace aweXpect.Core.Tests.Core.Helpers;

public class StringExtensionsTests
{
	[Fact]
	public async Task SubstringUntilFirst_WhenNotPresent_ShouldReturnString()
	{
		string input = "foo";

		string result = input.SubstringUntilFirst('X');

		await That(result).Should().Be(input);
	}

	[Fact]
	public async Task SubstringUntilFirst_WhenPresent_ShouldReturnSubstringUntilFirstOccurrence()
	{
		string input = "a,b,c";

		string result = input.SubstringUntilFirst(',');

		await That(result).Should().Be("a");
	}
}
