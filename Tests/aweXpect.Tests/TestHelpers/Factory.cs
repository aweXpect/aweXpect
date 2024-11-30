using System.Collections.Generic;
#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
using System.Threading;
#endif

namespace aweXpect.Tests.TestHelpers;

internal static class Factory
{
#if NET6_0_OR_GREATER
	/// <summary>
	///     Returns an infinite <see cref="IAsyncEnumerable{T}" /> of fibonacci numbers.
	/// </summary>
	public static async IAsyncEnumerable<int> GetAsyncFibonacciNumbers(
		int maxIterations = int.MaxValue,
		[EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		int a = 0, b = 1;

		int iterations = 0;
		do
		{
			await Task.Yield();
			yield return b;
			(a, b) = (b, a + b);
		} while (++iterations < maxIterations && !cancellationToken.IsCancellationRequested);
	}
#endif

#if NET6_0_OR_GREATER
	/// <summary>
	///     Returns an infinite <see cref="IAsyncEnumerable{T}" /> of mapped fibonacci numbers.
	/// </summary>
	public static async IAsyncEnumerable<T> GetAsyncFibonacciNumbers<T>(
		Func<int, T> mapper,
		int maxIterations = int.MaxValue,
		[EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		int a = 0, b = 1;

		int iterations = 0;
		do
		{
			await Task.Yield();
			yield return mapper(b);
			(a, b) = (b, a + b);
		} while (++iterations < maxIterations && !cancellationToken.IsCancellationRequested);
	}
#endif

	/// <summary>
	///     Returns an infinite <see cref="IEnumerable{T}" /> of fibonacci numbers.
	/// </summary>
	public static IEnumerable<int> GetFibonacciNumbers(int maxIterations = int.MaxValue)
	{
		int a = 0, b = 1;

		int iterations = 0;
		do
		{
			yield return b;
			(a, b) = (b, a + b);
		} while (++iterations < maxIterations);
	}
}
