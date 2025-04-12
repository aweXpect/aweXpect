using System.Collections.Generic;
using aweXpect.Helpers;

namespace aweXpect.Internal.Tests.Helpers;

public class ExceptionHelpersTests
{
	[Fact]
	public async Task GetInnerExceptions_WhenNull_ShouldBeEmpty()
	{
		Exception? exception = null;

		IEnumerable<Exception> result = exception.GetInnerExceptions();

		await That(result).IsEmpty();
	}
}
