using System.Collections.Generic;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
#endif

namespace aweXpect.Tests;

internal static class Factory
{
#if NET8_0_OR_GREATER
	/// <summary>
	///     Returns an "infinite" <see cref="IAsyncEnumerable{T}" /> of fibonacci numbers.
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

#if NET8_0_OR_GREATER
	/// <summary>
	///     Returns an "infinite" <see cref="IAsyncEnumerable{T}" /> of mapped fibonacci numbers.
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

#if NET8_0_OR_GREATER
	/// <summary>
	///     Returns an "infinite" <see cref="IAsyncEnumerable{T}" /> of <paramref name="value" />.
	/// </summary>
	public static async IAsyncEnumerable<T> GetConstantValueAsyncEnumerable<T>(T value,
		int maxIterations = int.MaxValue)
	{
		int iterations = 0;
		do
		{
			await Task.Yield();
			yield return value;
		} while (++iterations < maxIterations);
	}
#endif

	/// <summary>
	///     Returns an "infinite" <see cref="IEnumerable{T}" /> of fibonacci numbers.
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

	/// <summary>
	///     Returns an "infinite" <see cref="IEnumerable{T}" /> of fibonacci numbers.
	/// </summary>
	public static IEnumerable<T> GetFibonacciNumbers<T>(Func<int, T> mapper, int maxIterations = int.MaxValue)
	{
		int a = 0, b = 1;

		int iterations = 0;
		do
		{
			yield return mapper(b);
			(a, b) = (b, a + b);
		} while (++iterations < maxIterations);
	}

	/// <summary>
	///     Returns an "infinite" <see cref="IEnumerable{T}" /> of <paramref name="value" />.
	/// </summary>
	public static IEnumerable<T> GetConstantValueEnumerable<T>(T value, int maxIterations = int.MaxValue)
	{
		int iterations = 0;
		do
		{
			yield return value;
		} while (++iterations < maxIterations);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Returns an <see cref="ImmutableArray{T}" /> with <paramref name="count" /> elements of constant
	///     <paramref name="value" />.
	/// </summary>
	public static ImmutableArray<T> GetConstantValueImmutableArray<T>(T value, int count)
		=> [..GetConstantValueEnumerable(value, count),];

	/// <summary>
	///     Returns an <see cref="ImmutableArray{T}" /> with <paramref name="count" /> fibonacci numbers.
	/// </summary>
	public static ImmutableArray<int> GetFibonacciNumbersImmutableArray(int count)
		=> [..GetFibonacciNumbers(count),];

	/// <summary>
	///     Returns an <see cref="ImmutableArray{T}" /> with <paramref name="count" /> fibonacci numbers.
	/// </summary>
	public static ImmutableArray<T> GetFibonacciNumbersImmutableArray<T>(Func<int, T> mapper, int count)
		=> [..GetFibonacciNumbers(mapper, count),];
#endif
}
