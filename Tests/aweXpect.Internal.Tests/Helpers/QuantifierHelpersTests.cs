using aweXpect.Helpers;
using aweXpect.Options;

namespace aweXpect.Internal.Tests.Helpers;

public sealed class QuantifierHelpersTests
{
	[Fact]
	public async Task WhenIndentationIsEmpty_ShouldReturnInput()
	{
		Quantifier quantifier = Quantifier.Never();

		string result1 = quantifier.ToNegatedString();
		string result2 = quantifier.ToNegatedString();

		await That(result1).IsEqualTo(result2);
	}
}
