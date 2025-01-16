#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class AsyncEnumerableShould
{
	public sealed class BeEmpty
	{
		public sealed class Tests
		{
			[Fact]
			public async Task CancelledEnumerable_ShouldFail()
			{
				using CancellationTokenSource cts = new();
				cts.Cancel();
				CancellationToken token = cts.Token;
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).Should().BeEmpty().WithCancellation(token);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be empty,
					             but could not evaluate it, because it was already cancelled
					             """);
			}

			[Fact]
			public async Task ConsidersCancellationToken()
			{
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;
				IAsyncEnumerable<int> subject =
					GetCancellingAsyncEnumerable(5, cts, CancellationToken.None);

				async Task Act()
					=> await That(subject).Should().BeEmpty().WithCancellation(token);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be empty,
					             but it was [
					               0,
					               1,
					               2,
					               3,
					               4
					             ]
					             """);
			}

			[Fact]
			public async Task ShouldDisplayUpToTenItems()
			{
				using CancellationTokenSource cts = new();
				IAsyncEnumerable<int> subject =
					GetCancellingAsyncEnumerable(16, cts, CancellationToken.None);

				async Task Act()
					=> await That(subject).Should().BeEmpty();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be empty,
					             but it was [
					               0,
					               1,
					               2,
					               3,
					               4,
					               5,
					               6,
					               7,
					               8,
					               9,
					               …
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsValues_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 2]);

				async Task Act()
					=> await That(subject).Should().BeEmpty();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be empty,
					             but it was [
					               1,
					               1,
					               2
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).Should().BeEmpty();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject!).Should().BeEmpty();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be empty,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
