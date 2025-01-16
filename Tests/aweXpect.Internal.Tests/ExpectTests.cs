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
#endif
}
