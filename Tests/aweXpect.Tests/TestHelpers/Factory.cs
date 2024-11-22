using System.Collections.Generic;
#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
using System.Threading;
#endif

namespace Testably.Expectations.Tests.TestHelpers;

internal static class Factory
{
#if NET6_0_OR_GREATER
	/// <summary>
	///     Returns an infinite <see cref="IAsyncEnumerable{T}" /> of fibonacci numbers.
	/// </summary>
	public static async IAsyncEnumerable<int> GetAsyncFibonacciNumbers(
		[EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		int a = 0, b = 1;

		do
		{
			await Task.Yield();
			yield return b;
			(a, b) = (b, a + b);
		} while (!cancellationToken.IsCancellationRequested);
	}
#endif
	/// <summary>
	///     Returns an infinite <see cref="IEnumerable{T}" /> of fibonacci numbers.
	/// </summary>
	public static IEnumerable<int> GetFibonacciNumbers()
	{
		int a = 0, b = 1;

		do
		{
			yield return b;
			(a, b) = (b, a + b);
		} while (true);
	}
}
