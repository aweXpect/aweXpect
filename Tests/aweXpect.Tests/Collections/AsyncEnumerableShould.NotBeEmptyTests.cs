#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class AsyncEnumerableShould
{
	public sealed class NotBeEmpty
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenEnumerableContainsValues_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 2]);

				async Task Act()
					=> await That(subject).Should().NotBeEmpty();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).Should().NotBeEmpty();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be empty,
					             but it was
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotBeEmpty();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be empty,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
