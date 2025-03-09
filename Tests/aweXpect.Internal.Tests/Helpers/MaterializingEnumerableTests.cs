using System.Collections.Generic;
using System.Linq;
using aweXpect.Helpers;

namespace aweXpect.Internal.Tests.Helpers;

public class MaterializingEnumerableTests
{
	[Fact]
	public async Task WhenIterating_ShouldReturnAllValues()
	{
		IEnumerable<int> enumerable = ToEnumerable([1, 2, 3,]);

		IEnumerable<int> materialized = MaterializingEnumerable<int>.Wrap(enumerable);

		List<int> result = materialized.ToList();

		await That(result).IsEqualTo([1, 2, 3,]);
	}

	[Fact]
	public async Task Wrap_ForCollection_ShouldUseCollection()
	{
		List<int> collection = new();

		IEnumerable<int> enumerable = MaterializingEnumerable<int>.Wrap(collection);

		await That(enumerable).IsSameAs(collection);
	}

	[Fact]
	public async Task Wrap_Twice_ShouldUseSameInstance()
	{
		IEnumerable<int> enumerable = ToEnumerable([1, 2, 3,]);

		IEnumerable<int> materialized1 = MaterializingEnumerable<int>.Wrap(enumerable);
		IEnumerable<int> materialized2 = MaterializingEnumerable<int>.Wrap(materialized1);

		await That(enumerable).IsNotSameAs(materialized1);
		await That(materialized1).IsSameAs(materialized2);
	}


	private static IEnumerable<T> ToEnumerable<T>(T[] items)
	{
		foreach (T item in items)
		{
			yield return item;
		}
	}
}
