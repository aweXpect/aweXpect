using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace aweXpect.Internal.Tests;

public class ExpectTests
{
#if NET8_0_OR_GREATER
	/// <summary>
	///     Returns an <see cref="IAsyncEnumerable{T}" /> with incrementing numbers, starting with 0, which cancels the
	///     <paramref name="cancellationTokenSource" /> after <paramref name="cancelAfter" /> iteration.
	/// </summary>
	private static async IAsyncEnumerable<int> GetCancellingAsyncEnumerable(
		int cancelAfter,
		CancellationTokenSource cancellationTokenSource,
		[EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		int index = 0;
		while (!cancellationToken.IsCancellationRequested)
		{
			if (index == cancelAfter)
			{
				cancellationTokenSource.Cancel();
			}

			await Task.Yield();
			yield return index++;
		}
	}

	[Fact]
	public async Task ShouldWithExpressionBuilder_ShouldApplyMethods()
	{
		using CancellationTokenSource cts = new();
		CancellationToken token = cts.Token;
		IAsyncEnumerable<int> subject = GetCancellingAsyncEnumerable(6, cts, CancellationToken.None);

		async Task Act()
			=> await That(subject).Should(b => b.WithCancellation(token)).HaveAll(x => x.Satisfy(y => y < 6));

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected subject to
			             have all items satisfy y => y < 6,
			             but could not verify, because it was cancelled early
			             """);
	}
#endif
}
