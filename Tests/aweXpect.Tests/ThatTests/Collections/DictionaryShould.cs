using System.Collections.Generic;

namespace aweXpect.Tests.ThatTests.Collections;

public partial class DictionaryShould
{
	public static IDictionary<int, T> ToDictionary<T>(T[] items)
	{
		Dictionary<int, T> result = new();
		int index = 0;
		foreach (T item in items)
		{
			result.Add(index++, item);
		}

		return result;
	}

	public static IDictionary<int, TMember> ToDictionary<TSource, TMember>(TSource[] items,
		Func<TSource, TMember> mapper)
	{
		Dictionary<int, TMember> result = new();
		int index = 0;
		foreach (TSource item in items)
		{
			result.Add(index++, mapper(item));
		}

		return result;
	}

	public class MyClass(int value = 0)
	{
		public InnerClass? Inner { get; set; }
		public int Value { get; set; } = value;
	}

	public class InnerClass
	{
		public IEnumerable<string>? Collection { get; set; }

		public InnerClass? Inner { get; set; }
		public string? Value { get; set; }
	}
}
