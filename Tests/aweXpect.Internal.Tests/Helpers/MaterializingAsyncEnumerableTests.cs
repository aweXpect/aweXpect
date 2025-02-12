#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Helpers;

namespace aweXpect.Internal.Tests.Helpers;

public class MaterializingAsyncEnumerableTests
{
	[Fact]
	public async Task WhenIterating_ShouldReturnAllValues()
	{
		IAsyncEnumerable<int> enumerable = ToAsyncEnumerable([1, 2, 3]);

		IAsyncEnumerable<int> materialized = MaterializingAsyncEnumerable<int>.Wrap(enumerable);

		await That(materialized).IsEqualTo([1, 2, 3]);
	}

	[Fact]
	public async Task Wrap_Twice_ShouldUseSameInstance()
	{
		IAsyncEnumerable<int> enumerable = ToAsyncEnumerable([1, 2, 3]);

		IAsyncEnumerable<int> materialized1 = MaterializingAsyncEnumerable<int>.Wrap(enumerable);
		IAsyncEnumerable<int> materialized2 = MaterializingAsyncEnumerable<int>.Wrap(materialized1);

		await That(enumerable).IsNotSameAs(materialized1);
		await That(materialized1).IsSameAs(materialized2);
	}


	private static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(T[] items)
	{
		foreach (T item in items)
		{
			await Task.Yield();
			yield return item;
		}
	}
}
#endif
