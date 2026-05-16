#if NET8_0_OR_GREATER
using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class All
	{
		public sealed partial class ComplyWith
		{
			public sealed class StringTests
			{
				[Fact]
				public async Task WhenAllItemsMatchExpectation_ShouldSucceed()
				{
					IAsyncEnumerable<string?> subject = ToAsyncEnumerable<string?>("apple", "ant", "avocado");

					async Task Act()
						=> await That(subject).All().ComplyWith(it => it.StartsWith("a"));

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenExactlyOneItemMatchesExpectation_ShouldSucceed()
				{
					IAsyncEnumerable<string?> subject = ToAsyncEnumerable<string?>("apple", "banana", "cherry");

					async Task Act()
						=> await That(subject).Exactly(1).ComplyWith(it => it.StartsWith("b"));

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenNotAllItemsMatch_ShouldFail()
				{
					IAsyncEnumerable<string?> subject = ToAsyncEnumerable<string?>("apple", "banana", "avocado");

					async Task Act()
						=> await That(subject).All().ComplyWith(it => it.StartsWith("a"));

					await That(Act).Throws<XunitException>();
				}
			}
		}
	}
}
#endif
