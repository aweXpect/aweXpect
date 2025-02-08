#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed class IsNull
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(null)]
			[InlineData(new int[0])]
			public async Task IsNull_ShouldBeChainableWithIsEmpty(int[]? values)
			{
				IAsyncEnumerable<int>? subject = values == null ? null : ToAsyncEnumerable(values);

				async Task Act()
					=> await That(subject)
						.IsNull().Or.IsEmpty();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task IsNull_WhenNotNull_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).IsNull();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is null,
					             but it was*
					             """).AsWildcard();
			}

			[Fact]
			public async Task IsNull_WhenNull_ShouldSucceed()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).IsNull();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
